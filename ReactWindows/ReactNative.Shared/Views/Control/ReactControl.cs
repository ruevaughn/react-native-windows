// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#if WINDOWS_UWP
using ReactNative.Accessibility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
#else
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
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

        private ReactControlAutomationPeer peer;

        /// <summary>
        /// This flag determines whether focus was requested before the state of the component was set to IsLoaded
        /// </summary>
        private bool _isFocusRequestedBeforeLoad;

        /// <summary>
        /// This flag determines whether alert was requested before the state of the component was set to IsLoaded
        /// </summary>
        private bool _isAlertRequestedBeforeLoad;

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

            this.Loaded += OnLoaded;

            AutomationControlType = AutomationControlType.Custom;

#if WINDOWS_UWP
            UseSystemFocusVisuals = true;
#endif
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isFocusRequestedBeforeLoad)
            {
                base.Focus();
            }

            if (_isAlertRequestedBeforeLoad)
            {
                this.RaiseSystemAlertInternal();
            }

            this.Loaded -= OnLoaded;
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
            this.peer = new ReactControlAutomationPeer(this);
            return this.peer;
        }

        /// <summary>
        /// Raises a system alert.
        /// </summary>
        /// <returns>Operation result.</returns>
        public bool RaiseSystemAlert()
        {
            if (!this.IsLoaded)
            {
                this._isAlertRequestedBeforeLoad = true;
                return false;
            }
            else
            {
                this.RaiseSystemAlertInternal();
                return true;
            }
        }

        private void RaiseSystemAlertInternal()
        {
            if (this.peer != null)
            {
                // Have the AutomationPeer for this element raise the UIA event.
                peer.RaiseSystemAlertEvent();
            }
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

        ReactControl reactControl;

        IRawElementProviderSimple provider;

        /// <summary>
        /// Modified ReactControl with interactive role.
        /// </summary>
        /// <param name="owner">The ReactControl instance.</param>
        public ReactControlAutomationPeer(ReactControl owner) : base(owner)
        {
            this.reactControl = owner;
            _ownerAutomationControlType = owner.AutomationControlType;
        }

        /// <summary>
        /// Raises System alert
        /// </summary>
        public void RaiseSystemAlertEvent()
        {
            // Get the IRawElementProviderSimple for this AutomationPeer.
            if (this.provider == null)
            {
                AutomationPeer peer = FrameworkElementAutomationPeer.FromElement(this.reactControl);

                if (peer != null)
                {
                    provider = ProviderFromPeer(peer);
                }
            }

            if (this.provider != null)

            {
                // Call the native UiaRaiseAutomationEvent to raise the event.
                UiaRaiseAutomationEvent(this.provider, 20023 /*UIA_SystemAlertEventId*/);
            }
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

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaRaiseAutomationEvent")]

        private static extern int UiaRaiseAutomationEvent(IRawElementProviderSimple element, int eventId);
    }

}
