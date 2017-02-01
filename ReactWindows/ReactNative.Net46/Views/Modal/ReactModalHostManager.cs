using ReactNative.UIManager;
using ReactNative.UIManager.Annotations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ReactNative.Views.Modal
{
    public class ReactModalHostManager : ViewParentManager<Canvas, ModalHostShadowNode>
    {
        private const string REACT_CLASS = "RCTModalHostView";
        public override string Name => REACT_CLASS;
        public string getName() => REACT_CLASS;

        protected override Canvas CreateViewInstance (ThemedReactContext reactContext)
        {
            return new Canvas();
        }

        public override ModalHostShadowNode CreateShadowNodeInstance ()
        {
            return new ModalHostShadowNode();
        }

        public override void AddView(Canvas parent, DependencyObject child, int index)
        {
            parent.Children.Insert(index, (UIElement)child);
        }

        public override int GetChildCount(Canvas parent)
        {
            return parent.Children.Count;
        }

        public override DependencyObject GetChildAt(Canvas parent, int index)
        {
            return (FrameworkElement)parent.Children[index];
        }

        public override void RemoveChildAt(Canvas parent, int index)
        {
            parent.Children.RemoveAt(index);
        }

        public override void RemoveAllChildren(Canvas parent)
        {
            parent.Children.Clear();
        }

        [ReactProp("animationType")]
        public void setAnimationType(Canvas view, string animationType)
        {
            throw new NotImplementedException();
        }
    }
}
