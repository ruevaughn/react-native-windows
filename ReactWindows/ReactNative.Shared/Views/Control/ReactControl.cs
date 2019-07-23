// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#if WINDOWS_UWP
using ReactNative.Accessibility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

#endif

namespace ReactNative.Views.ControlView
{
    /// <summary>
    /// A native control with a single Canvas child.
    /// </summary>
#if WINDOWS_UWP
    public class ReactControl : UserControl, IAccessible
#else
    public class ReactControl : UserControl
#endif
    {
        private readonly Canvas _canvas;

        /// <summary>
        /// Instantiates the <see cref="ReactControl"/>. 
        /// </summary>
        public ReactControl()
        {
            Content = _canvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            AutomationControlType = AutomationControlType.Custom;

#if WINDOWS_UWP
            UseSystemFocusVisuals = true;
#endif
        }

        /// <summary>
        /// The view children.
        /// </summary>
        public UIElementCollection Children
        {
            get
            {
                return _canvas.Children;
            }
        }

        /// <summary>
        /// Keys that should be handled during <see cref="UIElement.KeyDownEvent"/>. 
        /// </summary>
        public int[] HandledKeyDownKeys
        {
            get;
            set;
        }

        /// <summary>
        /// Keys that should be handled during <see cref="UIElement.KeyUpEvent"/>. 
        /// </summary>
        public int[] HandledKeyUpKeys
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets automation control type
        /// </summary>
        public AutomationControlType AutomationControlType { get; private set; }

        /// <inheritdoc />                                              
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ReactControlAutomationPeer(this);
        }

        /// <summary>
        /// Used to set automation control type
        /// </summary>
        /// <param name="type">automation control type</param>
        public void SetAutomationControlType(AutomationControlType type)
        {
            AutomationControlType = type;
        }

#if WINDOWS_UWP
        /// <inheritdoc />                                              
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new DynamicAutomationPeer<ReactControl>(this);
        }

        // TODO: implement runtime change raising event to screen reader #1562
        /// <inheritdoc />                                                    
        public AccessibilityTrait[] AccessibilityTraits { get; set; }
#endif
    }

    /// <summary>
    /// Custom peer class deriving from UserControlAutomationPeer
    /// </summary>
    public class ReactControlAutomationPeer : UserControlAutomationPeer
    {
        private readonly AutomationControlType _ownerAutomationControlType;

        /// <summary>
        /// Modified ReactControl with interactive role.
        /// </summary>
        /// <param name="owner">The ReactControl instance.</param>
        public ReactControlAutomationPeer(ReactControl owner) : base(owner)
        {
            _ownerAutomationControlType = owner.AutomationControlType;
        }

        /// <summary>
        /// Override for GetAutomationControlTypeCore
        /// </summary>
        /// <returns>AutomationControlType</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return _ownerAutomationControlType;
        }

        /// <summary>
        /// Interactive role in the user interface
        /// </summary>
        /// <returns> Boolean </returns>
        protected override bool IsControlElementCore()
        {
            return true;
        }
    }

}
