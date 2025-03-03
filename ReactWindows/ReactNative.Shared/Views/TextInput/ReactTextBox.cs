// Copyright (c) Microsoft Corporation. All rights reserved.
// Portions derived from React Native:
// Copyright (c) 2015-present, Facebook, Inc.
// Licensed under the MIT License.

using ReactNative.UIManager;
using System;
using System.Threading;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
#endif

namespace ReactNative.Views.TextInput
{
    internal class ReactTextBox : TextBox
    {
        private int _eventCount;
        private bool _selectionChangedSubscribed;
        private bool _sizeChangedSubscribed;
        private ClearButtonModeType _clearButtonMode = ClearButtonModeType.Default;
        private Button _deleteButton;

        /// <summary>
        /// This flag determines whether focus was requested before the state of the component was set to IsLoaded
        /// </summary>
        private bool _isFocusRequestedBeforeLoad;

        public ReactTextBox()
        {
            this.Loaded += OnLoaded;
        }

        // This ensures that default XAML behavior is used if 'clearButtonMode' prop is unaltered/unused.
        // If 'clearButtonMode' is changed to anything but default and is changed back to default, then it will use the WhileEditing behavior
        // because default behavior becomes distorted after manually manipulating DeleteButton visibility.
        private bool _isClearButtonModeAltered = false;

        public ClearButtonModeType ClearButtonMode
        {
            get
            {
                return _clearButtonMode;
            }
            set
            {
                if (_clearButtonMode != value)
                {
                    _clearButtonMode = value;

                    if (!_isClearButtonModeAltered && value != ClearButtonModeType.Default)
                    {
                        _isClearButtonModeAltered = true;
                    }

                    UpdateDeleteButtonVisibility();
                }
            }
        }

        private long? DeleteButtonVisibilityToken
        {
            get;
            set;
        }

        public int CurrentEventCount => _eventCount;

        public bool ClearTextOnFocus
        {
            get;
            set;
        }

        public bool SelectTextOnFocus
        {
            get;
            set;
        }

        public bool OnSelectionChange
        {
            get
            {
                return _selectionChangedSubscribed;
            }
            set
            {
                if (value != _selectionChangedSubscribed)
                {
                    _selectionChangedSubscribed = value;
                    if (_selectionChangedSubscribed)
                    {
                        SelectionChanged += OnSelectionChanged;
                    }
                    else
                    {
                        SelectionChanged -= OnSelectionChanged;
                    }
                }
            }
        }

        public bool OnContentSizeChange
        {
            get
            {
                return _sizeChangedSubscribed;
            }
            set
            {
                if (value != _sizeChangedSubscribed)
                {
                    _sizeChangedSubscribed = value;
                    if (_sizeChangedSubscribed)
                    {
                        SizeChanged += OnSizeChanged;
                    }
                    else
                    {
                        SizeChanged -= OnSizeChanged;
                    }
                }
            }
        }

        public bool AutoGrow
        {
            get;
            set;
        }

        public bool DimensionsUpdated
        {
            get;
            set;
        }

        public int IncrementEventCount()
        {
            return Interlocked.Increment(ref _eventCount);
        }

        /// <summary>
        /// New Focus method introduced to handle calls while IsLoaded == false.
        /// </summary>
        /// <returns>operation result.</returns>
        public new bool Focus()
        {
            if (!this.IsLoaded)
            {
                this._isFocusRequestedBeforeLoad = true;
                return false;
            }
            else
            {
                return base.Focus();
            }
        }

#if WINDOWS_UWP
        protected override void OnApplyTemplate()
#else
        public override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();

            if (_deleteButton != null)
            {
                if (DeleteButtonVisibilityToken.HasValue)
                {
#if WINDOWS_UWP
                    _deleteButton.UnregisterPropertyChangedCallback(Button.VisibilityProperty, (long)DeleteButtonVisibilityToken);
#endif
                }
            }

            _deleteButton = (Button)GetTemplateChild("DeleteButton");
#if WINDOWS_UWP
            DeleteButtonVisibilityToken = _deleteButton.RegisterPropertyChangedCallback(Button.VisibilityProperty, (DependencyObject d, DependencyProperty dp) => UpdateDeleteButtonVisibility());
#endif
            TextChanged += OnTextChanged;
            UpdateDeleteButtonVisibility();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isFocusRequestedBeforeLoad)
            {
                base.Focus();
            }

            this.Loaded -= OnLoaded;
        }

        private void UpdateDeleteButtonVisibility()
        {
            if (_deleteButton != null && _isClearButtonModeAltered)
            {
                switch (ClearButtonMode)
                {
                    case ClearButtonModeType.Default:
                    case ClearButtonModeType.WhileEditing:
                        _deleteButton.Visibility = HasFocus() && !string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case ClearButtonModeType.UnlessEditing:
                        _deleteButton.Visibility = !HasFocus() && !string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case ClearButtonModeType.Never:
                        _deleteButton.Visibility = Visibility.Collapsed;
                        break;
                    case ClearButtonModeType.Always:
                        _deleteButton.Visibility = Visibility.Visible;
                        break;
                    default:
                        throw new NotSupportedException($"'{ClearButtonMode}' is not a mode supported by ClearButtonMode property.");
                }
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (ClearTextOnFocus)
            {
                Text = "";
            }

            if (SelectTextOnFocus)
            {
                SelectionStart = 0;
                SelectionLength = Text.Length;
            }

            UpdateDeleteButtonVisibility();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            UpdateDeleteButtonVisibility();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDeleteButtonVisibility();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DimensionsUpdated)
            {
                DimensionsUpdated = false;
                return;
            }

            this.GetReactContext()
                .GetNativeModule<UIManagerModule>()
                .EventDispatcher
                .DispatchEvent(
                    new ReactTextInputContentSizeChangedEvent(
                        this.GetTag(),
                        e.NewSize.Width,
                        e.NewSize.Height));
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var start = this.SelectionStart;
            var length = this.SelectionLength;
            this.GetReactContext()
                .GetNativeModule<UIManagerModule>()
                .EventDispatcher
                .DispatchEvent(
                    new ReactTextInputSelectionEvent(
                        this.GetTag(),
                        start,
                        start + length));
        }

        private bool HasFocus()
        {
#if WINDOWS_UWP
            return FocusState != FocusState.Unfocused;
#else
            return this.IsKeyboardFocused;
#endif
        }
    }
}
