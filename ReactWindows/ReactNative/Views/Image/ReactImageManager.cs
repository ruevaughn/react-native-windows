// Copyright (c) Microsoft Corporation. All rights reserved.
// Portions derived from React Native:
// Copyright (c) 2015-present, Facebook, Inc.
// Licensed under the MIT License.

using ImagePipeline.Core;
using Newtonsoft.Json.Linq;
using ReactNative.Collections;
using ReactNative.Modules.Image;
using ReactNative.UIManager;
using ReactNative.UIManager.Annotations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ReactNative.Views.Image
{
    /// <summary>
    /// The view manager responsible for rendering native images.
    /// </summary>
    public class ReactImageManager : BaseViewManager<Border, ReactImageShadowNode>
    {
        private readonly ViewKeyedDictionary<Border, List<KeyValuePair<string, double>>> _imageSources =
            new ViewKeyedDictionary<Border, List<KeyValuePair<string, double>>>();

        private readonly ViewKeyedDictionary<Border, CornerRadiusManager> _borderToRadii =
            new ViewKeyedDictionary<Border, CornerRadiusManager>();

        private readonly ViewKeyedDictionary<Border, ThicknessManager> _borderToThickness =
            new ViewKeyedDictionary<Border, ThicknessManager>();

        private readonly ThreadLocal<ScaleTransform> _rtlScaleTransform = new ThreadLocal<ScaleTransform>(() => new ScaleTransform
        {
            CenterX = 0.5,
            ScaleX = -1
        });

        private readonly Dictionary<int, Color?> _tintColors =
            new Dictionary<int, Color?>();

        private readonly Dictionary<int, Color?> _backgroundColors =
            new Dictionary<int, Color?>();

        /// <summary>
        /// The view manager name.
        /// </summary>
        public override string Name
        {
            get
            {
                return "RCTImageView";
            }
        }

        /// <summary>
        /// This method should return the <see cref="ReactImageShadowNode"/>
        /// which will be then used for measuring the position and size of the
        /// view. 
        /// </summary>
        /// <returns>The shadow node instance.</returns>
        public override ReactImageShadowNode CreateShadowNodeInstance()
        {
            return new ReactImageShadowNode();
        }

        /// <summary>
        /// Implement this method to receive optional extra data enqueued from
        /// the corresponding instance of <see cref="ReactShadowNode"/> in
        /// <see cref="ReactShadowNode.OnCollectExtraUpdates"/>.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="extraData">The extra data.</param>
        public override void UpdateExtraData(Border view, object extraData)
        {
            Tuple<JArray, Color?, Color?> imageExtraData = (Tuple<JArray, Color?, Color?>)extraData;
            var imageBrush = (ImageBrush)view.Background;

            OnImageStatusUpdate(view, ImageLoadStatus.OnLoadStart, new ImageMetadata());

            var sources = imageExtraData.Item1;
            var tintColor = imageExtraData.Item2;
            var backgroundColor = imageExtraData.Item3;
            var tag = view.GetTag();

            var count = sources.Count;

            // There is no image source
            if (count == 0)
            {
                throw new ArgumentException("Sources must not be empty.", nameof(sources));
            }
            // Optimize for the case where we have just one uri, case in which we don't need the sizes
            else if (count == 1)
            {
                var uri = ((JObject)sources[0]).Value<string>("uri");
                var width = ((JObject)sources[0]).Value<double>("width");
                var height = ((JObject)sources[0]).Value<double>("height");

                var viewSources = new List<KeyValuePair<string, double>>(1);
                viewSources.Add(
                    new KeyValuePair<string, double>(
                        uri,
                        width * height));
                _imageSources.Add(tag, viewSources);
                _tintColors.Add(tag, tintColor);
                _backgroundColors.Add(tag, backgroundColor);
                SetUriFromSingleSource(view, uri, tintColor, backgroundColor);
            }
            else
            {
                var viewSources = default(List<KeyValuePair<string, double>>);

                if (_imageSources.TryGetValue(tag, out viewSources))
                {
                    viewSources.Clear();
                }
                else
                {
                    viewSources = new List<KeyValuePair<string, double>>(count);
                    _imageSources.Add(tag, viewSources);
                }

                foreach (var source in sources)
                {
                    var sourceData = (JObject)source;
                    viewSources.Add(
                        new KeyValuePair<string, double>(
                            sourceData.Value<string>("uri"),
                            sourceData.Value<double>("width") * sourceData.Value<double>("height")));
                }

                _tintColors.Add(tag, tintColor);
                _backgroundColors.Add(tag, backgroundColor);

                viewSources.Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

                if (double.IsNaN(view.Width) || double.IsNaN(view.Height))
                {
                    // If we need to choose from multiple URIs but the size is not yet set, wait for layout pass
                    return;
                }

                var uriToLoad = ChooseUriFromMultipleSources(view);
                if (uriToLoad != null)
                    SetUriFromSingleSource(view, uriToLoad, tintColor, backgroundColor);
            }
        }
        /// <summary>
        /// The view manager event constants.
        /// </summary>
        public override JObject CustomDirectEventTypeConstants
        {
            get
            {
                return new JObject
                {
                    {
                        "topLoadStart",
                        new JObject
                        {
                            { "registrationName", "onLoadStart" }
                        }
                    },
                    {
                        "topLoad",
                        new JObject
                        {
                            { "registrationName", "onLoad" }
                        }
                    },
                    {
                        "topLoadEnd",
                        new JObject
                        {
                            { "registrationName", "onLoadEnd" }
                        }
                    },
                    {
                        "topError",
                        new JObject
                        {
                            { "registrationName", "onError" }
                        }
                    },
                };
            }
        }

        /// <summary>
        /// Set the scaling mode of the image.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        /// <param name="resizeMode">The scaling mode.</param>
        [ReactProp(ViewProps.ResizeMode)]
        public void SetResizeMode(Border view, string resizeMode)
        {
            if (resizeMode != null)
            {
                var imageBrush = (ImageBrush)view.Background;

                if (resizeMode.Equals("cover"))
                {
                    imageBrush.Stretch = Stretch.UniformToFill;
                }
                else if (resizeMode.Equals("contain"))
                {
                    imageBrush.Stretch = Stretch.Uniform;
                }
                else
                {
                    imageBrush.Stretch = Stretch.Fill;
                }
            }
        }

        /// <summary>
        /// Set the source URI of the image.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        /// <param name="sources">The source URI.</param>
        [ReactProp("src")]
        public void SetSource(Border view, JArray sources)
        {
            var count = sources.Count;

            // There is no image source
            if (count == 0)
            {
                throw new ArgumentException("Sources must not be empty.", nameof(sources));
            }
            // Optimize for the case where we have just one uri, case in which we don't need the sizes
            else if (count == 1)
            {
                var uri = ((JObject)sources[0]).Value<string>("uri");
                SetUriFromSingleSource(view, uri);
            }
            else
            {
                if (_imageSources.TryGetValue(view, out var viewSources))
                {
                    viewSources.Clear();
                }
                else
                {
                    viewSources = new List<KeyValuePair<string, double>>(count);
                    _imageSources.AddOrUpdate(view, viewSources);
                }

                foreach (var source in sources)
                {
                    var sourceData = (JObject)source;
                    viewSources.Add(
                        new KeyValuePair<string, double>(
                            sourceData.Value<string>("uri"),
                            sourceData.Value<double>("width") * sourceData.Value<double>("height")));
                }

                viewSources.Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

                if (double.IsNaN(view.Width) || double.IsNaN(view.Height))
                {
                    // If we need to choose from multiple URIs but the size is not yet set, wait for layout pass
                    return;
                }

                SetUriFromMultipleSources(view);
            }
        }

        /// <summary>
        /// Enum values correspond to positions of prop names in ReactPropGroup attribute
        /// applied to <see cref="SetBorderRadius(Border, int, double?)"/>
        /// </summary>
        private enum Radius
        {
            All,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }

        /// <summary>
        /// The border radius of the <see cref="ReactRootView"/>.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        /// <param name="index">The prop index.</param>
        /// <param name="radius">The border radius value.</param>
        [ReactPropGroup(
            ViewProps.BorderRadius,
            ViewProps.BorderTopLeftRadius,
            ViewProps.BorderTopRightRadius,
            ViewProps.BorderBottomLeftRadius,
            ViewProps.BorderBottomRightRadius)]
        public void SetBorderRadius(Border view, int index, double? radius)
        {
            if (!_borderToRadii.TryGetValue(view, out var cornerRadiusManager))
            {
                cornerRadiusManager = new CornerRadiusManager();
                _borderToRadii.AddOrUpdate(view, cornerRadiusManager);
            }

            switch ((Radius)index)
            {
                case Radius.All:
                    cornerRadiusManager.Set(CornerRadiusManager.All, radius);
                    break;
                case Radius.TopLeft:
                    cornerRadiusManager.Set(CornerRadiusManager.TopLeft, radius);
                    break;
                case Radius.TopRight:
                    cornerRadiusManager.Set(CornerRadiusManager.TopRight, radius);
                    break;
                case Radius.BottomLeft:
                    cornerRadiusManager.Set(CornerRadiusManager.BottomLeft, radius);
                    break;
                case Radius.BottomRight:
                    cornerRadiusManager.Set(CornerRadiusManager.BottomRight, radius);
                    break;
            }
            view.CornerRadius = cornerRadiusManager.AsCornerRadius();
        }

        /// <summary>
        /// Set the border color of the image view.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        /// <param name="color">The masked color value.</param>
        [ReactProp(ViewProps.BorderColor, CustomType = "Color")]
        public void SetBorderColor(Border view, uint? color)
        {
            view.BorderBrush = color.HasValue
                ? new SolidColorBrush(ColorHelpers.Parse(color.Value))
                : null;
        }

        /// <summary>
        /// Enum values correspond to positions of prop names in ReactPropGroup attribute
        /// applied to <see cref="SetBorderWidth(Border, int, double?)"/>
        /// </summary>
        private enum Width
        {
            All,
            Left,
            Right,
            Top,
            Bottom,
        }

        /// <summary>
        /// Sets the border thickness of the image view.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        /// <param name="index">The prop index.</param>
        /// <param name="width">The border width in pixels.</param>
        [ReactPropGroup(
            ViewProps.BorderWidth,
            ViewProps.BorderLeftWidth,
            ViewProps.BorderRightWidth,
            ViewProps.BorderTopWidth,
            ViewProps.BorderBottomWidth)]
        public void SetBorderWidth(Border view, int index, double? width)
        {
            if (!_borderToThickness.TryGetValue(view, out var thicknessManager))
            {
                thicknessManager = new ThicknessManager();
                _borderToThickness.AddOrUpdate(view, thicknessManager);
            }

            switch ((Width)index)
            {
                case Width.All:
                    thicknessManager.Set(ThicknessManager.All, width);
                    break;
                case Width.Left:
                    thicknessManager.Set(ThicknessManager.Left, width);
                    break;
                case Width.Right:
                    thicknessManager.Set(ThicknessManager.Right, width);
                    break;
                case Width.Top:
                    thicknessManager.Set(ThicknessManager.Top, width);
                    break;
                case Width.Bottom:
                    thicknessManager.Set(ThicknessManager.Bottom, width);
                    break;
            }
            view.BorderThickness = thicknessManager.AsThickness();
        }

        /// <summary>
        /// Called when view is detached from view hierarchy and allows for
        /// additional cleanup.
        /// </summary>
        /// <param name="reactContext">The React context.</param>
        /// <param name="view">The view.</param>
        public override void OnDropViewInstance(ThemedReactContext reactContext, Border view)
        {
            base.OnDropViewInstance(reactContext, view);

            _imageSources.Remove(view);
            _borderToRadii.Remove(view);
            _borderToThickness.Remove(view);
        }

        /// <summary>
        /// Creates the image view instance.
        /// </summary>
        /// <param name="reactContext">The React context.</param>
        /// <returns>The image view instance.</returns>
        protected override Border CreateViewInstance(ThemedReactContext reactContext)
        {
            var border = new Border
            {
                Background = new ImageBrush
                {
                    Stretch = Stretch.UniformToFill
                },
            };

            border.Loaded += OnLoaded;

            return border;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Using a Border instead of a native Image has its advantages (round corner support, etc.), but
            // we have to take into account the automatic flipping that happens in RTL mode. We use a transform
            // to negate that flipping.
            var border = (Border)sender;

            border.RegisterPropertyChangedCallback(FrameworkElement.FlowDirectionProperty, FlowDirectionChanged);
            FlowDirectionChanged(border, null);
        }

        private void FlowDirectionChanged(DependencyObject sender, DependencyProperty dp)
        {
            var border = (Border)sender;
            if (border.FlowDirection == FlowDirection.RightToLeft)
            {
                border.Background.RelativeTransform = _rtlScaleTransform.Value;
            }
            else
            {
                border.Background.ClearValue(Brush.RelativeTransformProperty);
            }
        }

        /// <summary>
        /// Sets the dimensions of the view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="dimensions">The output buffer.</param>
        ///
        public override void SetDimensions(Border view, Dimensions dimensions)
        {
            base.SetDimensions(view, dimensions);
            var uriToLoad = ChooseUriFromMultipleSources(view);
            Color? tintColor = null, backgroundColor = null;
            _tintColors.TryGetValue(view.GetTag(), out tintColor);
            _backgroundColors.TryGetValue(view.GetTag(), out backgroundColor);
            if (uriToLoad != null)
                SetUriFromSingleSource(view, uriToLoad, tintColor, backgroundColor);
        }

        private void OnImageFailed(Border view, Exception e)
        {
            if (!view.HasTag())
            {
                // View may have been unmounted, ignore.
                return;
            }

            var eventDispatcher = view.GetReactContext()
                .GetNativeModule<UIManagerModule>()
                .EventDispatcher;

            eventDispatcher.DispatchEvent(
                new ReactImageLoadEvent(
                    view.GetTag(),
                    e.Message));

            eventDispatcher.DispatchEvent(
                new ReactImageLoadEvent(
                    view.GetTag(),
                    ReactImageLoadEvent.OnLoadEnd));

        }

        private void OnImageStatusUpdate(Border view, ImageLoadStatus status, ImageMetadata metadata)
        {
            if (!view.HasTag())
            {
                // View may have been unmounted, ignore.
                return;
            }

            var eventDispatcher = view.GetReactContext()
                .GetNativeModule<UIManagerModule>()
                .EventDispatcher;

            eventDispatcher.DispatchEvent(
                new ReactImageLoadEvent(
                    view.GetTag(),
                    (int)status,
                    metadata.Uri,
                    metadata.Width,
                    metadata.Height));
        }

        /// <summary>
        /// Set the source URI of the image.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        /// <param name="source">The source URI.</param>
        /// <param name="tintColor">The tint color</param>
        /// <param name="backgroundColor">The background color</param>
        private async void SetUriFromSingleSource(Border view, string source, Color? tintColor, Color? backgroundColor)
        {
            var imageBrush = (ImageBrush)view.Background;
            OnImageStatusUpdate(view, ImageLoadStatus.OnLoadStart, default(ImageMetadata));
            try
            {
                var imagePipeline = ImagePipelineFactory.Instance.GetImagePipeline();
                var dispatcher = CoreApplication.GetCurrentView().Dispatcher;
                var image = await imagePipeline.FetchEncodedBitmapImageAsync(new Uri(source), default(CancellationToken), dispatcher);
                var metadata = new ImageMetadata(source, image.PixelWidth, image.PixelHeight);

                OnImageStatusUpdate(view, ImageLoadStatus.OnLoad, metadata);
                imageBrush.ImageSource = image;
                OnImageStatusUpdate(view, ImageLoadStatus.OnLoadEnd, metadata);
            }
            catch (Exception e)
            {
                OnImageFailed(view, e);
            }
        }

        private static async Task<IRandomAccessStream> CreateStreamFromHttpUri(string source)
        {
            /* We could use RandomAccessStreamReference.CreateFromUri(source).OpenReadAsync()
             *  if it didn't mysteriously occasionally fail with dev server.
             *  Therefore we have to climb the tree with feet first.
             */
            var response = await System.Net.HttpWebRequest.CreateHttp(source).GetResponseAsync();
            using (var responseStream = response.GetResponseStream())
            {
                var memStream = new MemoryStream();
                await responseStream.CopyToAsync(memStream);
                return memStream.AsRandomAccessStream();
            }
        }

        private static async Task<IRandomAccessStream> CreateStreamFromAppUri(string source)
        {
            return await RandomAccessStreamReference.CreateFromUri(new Uri(source)).OpenReadAsync();
        }

        // Applies tintcolor and backgroundcolor for an image.
        // Cannot use the stored BitmapImage since there is no way to get raw data.
        private static async Task<WriteableBitmap> ColorizeBitmap(IRandomAccessStream stream, Color? tintColor, Color? backgroundColor)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream);

            var pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format 
                BitmapAlphaMode.Premultiplied,
                new BitmapTransform(),
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage
            );

            return await BitmapImageHelpers.ColorizePixelData(decoder.PixelWidth, decoder.PixelHeight, pixelData.DetachPixelData(), tintColor, backgroundColor);
        }

        private void Bi_ImageOpened(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chooses the uri with the size closest to the target image size. Must be called only after the
        /// layout pass when the sizes of the target image have been computed, and when there are at least
        /// two sources to choose from.
        /// </summary>
        /// <param name="view">The image view instance.</param>
        private string ChooseUriFromMultipleSources(Border view)
        {
            if (_imageSources.TryGetValue(view, out var sources))
            {
                var targetImageSize = view.Width * view.Height;
                var bestResult = sources.LocalMin((s) => Math.Abs(s.Value - targetImageSize));
                return bestResult.Key;
            }
            return null;
        }
    }
}
