using System.Windows.Controls;

namespace ReactNative.Views.Web
{
    /// <summary>
    /// A collection of <see cref="WebBrowser"/> extensions.
    /// </summary>
    public static class WebBrowserExtensions
    {
        /// <summary>
        /// Gets the document title from the page loaded in the web browser.
        /// </summary>
        /// <param name="webBrowser">The object.</param>
        /// <param name="fallback">The value to return if no title can be extracted.</param>
        /// <returns></returns>
        public static string GetDocumentTitle(this WebBrowser webBrowser, string fallback = "")
        {
            try
            {
                if (webBrowser != null)
                {
                    return ((dynamic)webBrowser.Document).Title;
                }
            }
            catch
            {
                // Ignored
            }

            return fallback;
        }
    }
}
