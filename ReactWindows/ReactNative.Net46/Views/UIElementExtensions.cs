using System.Windows;
using System.Windows.Input;

namespace ReactNative.Views.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="UIElement"/>
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Move focus away from the specified <see cref="UIElement"/>
        /// </summary>
        /// <param name="uiElement">The <see cref="UIElement"/> to blur</param>
        public static void Blur(this UIElement uiElement)
        {
            if (uiElement == null)
            {
                return;
            }

            var focusScope = FocusManager.GetFocusScope(uiElement);

            var inputElement = focusScope as IInputElement;

            if (inputElement != null && !inputElement.Equals(uiElement))
            {
                FocusManager.SetFocusedElement(focusScope, inputElement);
            }
            else
            {
                FocusManager.SetFocusedElement(focusScope, null);
            }
        }
    }

}
