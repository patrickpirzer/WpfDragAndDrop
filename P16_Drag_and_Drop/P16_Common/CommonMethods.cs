using System.Windows;
using System.Windows.Input;

namespace P16_Common
{
    /// <summary>
    /// Class for common methods.
    /// </summary>
    public class CommonMethods
    {
        /// <summary>
        /// Displays the given cursor on the given window.
        /// </summary>
        /// <param name="window">The window on which the cursor shall be displayed.</param>
        /// <param name="displayCursor">The cursor to be displayed.</param>
        public static void DisplayCursor(Window window, Cursor displayCursor)
        {
            window.Cursor = displayCursor;
            Mouse.OverrideCursor = displayCursor;
        }

    }
}
