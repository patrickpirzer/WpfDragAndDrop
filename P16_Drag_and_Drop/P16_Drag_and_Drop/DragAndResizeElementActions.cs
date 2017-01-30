using P16_Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P16_Drag_and_Drop
{
    /// <summary>
    /// Class for changing the position and size of elements by drag-and-resize.
    /// </summary>
    public class DragAndResizeElementActions
    {
        /// <summary>
        /// Constant for the gap between the edge and the interior of an element.
        /// This constant is used to find out, if an element shall be moved or resized.
        /// </summary>
        private const double GAP = 10;

        /// <summary>
        /// The part of the control the mouse is over.
        /// </summary>
        private enum HitType
        {
            None,
            Body,
            BottomLeft,
            BottomRight,
            TopRight,
            TopLeft,
            Left,
            Right,
            Top,
            Bottom
        };

        /// <summary>
        /// The part of the control under the mouse.
        /// </summary>
        private HitType mouseHitType = HitType.None;

        /// <summary>
        /// True if a drag is in progress.
        /// </summary>
        private bool dragInProgress;

        /// <summary>
        /// Field for the drag's last point.
        /// </summary>
        private Point lastPoint;

        /// <summary>
        /// Field for the default mouse cursor.
        /// </summary>
        private Cursor defaultCursor = Cursors.Arrow;

        /// <summary>
        /// Field for an object of the designerwindow (= CreateAndDragControls.xaml).
        /// </summary>
        private Window designerwindow;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="windowForMaskDesign">The window for designing masks.</param>
        public DragAndResizeElementActions(Window windowForMaskDesign)
        {
            designerwindow = windowForMaskDesign;
        }

        /// <summary>
        /// The MouseDown-event starts either the dragging or the resizing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        public void OnElementMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Converts the sender to type FrameworkElement.
            FrameworkElement element = (FrameworkElement)sender;

            // Finds out the hit type of the mouse click.
            mouseHitType = SetHitType(element, Mouse.GetPosition(element.Parent as FrameworkElement));

            // Changes the look of the mouse cursor.
            SetMouseCursor(element);

            if (mouseHitType == HitType.None)
                return;

            // Reminds the actual mouse position.
            lastPoint = Mouse.GetPosition(element.Parent as FrameworkElement);

            dragInProgress = true;
        }

        /// <summary>
        /// While the drag-and-resize operation the element is either moved or resized. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        public void OnElementMouseMove(object sender, MouseEventArgs e)
        {
            // Converts the sender to type FrameworkElement.
            FrameworkElement element = (FrameworkElement)sender;

            if (dragInProgress)
            {
                // Checks how much the mouse has moved.
                Point mousePosition = Mouse.GetPosition(element.Parent as FrameworkElement);
                double offsetX = mousePosition.X - lastPoint.X;
                double offsetY = mousePosition.Y - lastPoint.Y;

                // Gets the element's current position.
                double newX = Canvas.GetLeft(element);
                double newY = Canvas.GetTop(element);
                double newWidth = Double.Parse(element.GetType().GetProperty("ActualWidth").GetValue(element).ToString());
                double newHeight = Double.Parse(element.GetType().GetProperty("ActualHeight").GetValue(element).ToString());

                // Updates the element's position and size.
                switch (mouseHitType)
                {
                    case HitType.Body:
                        newX += offsetX;
                        newY += offsetY;
                        break;
                    case HitType.TopLeft:
                        newX += offsetX;
                        newY += offsetY;
                        newWidth -= offsetX;
                        newHeight -= offsetY;
                        break;
                    case HitType.TopRight:
                        newY += offsetY;
                        newWidth += offsetX;
                        newHeight -= offsetY;
                        break;
                    case HitType.BottomRight:
                        newWidth += offsetX;
                        newHeight += offsetY;
                        break;
                    case HitType.BottomLeft:
                        newX += offsetX;
                        newWidth -= offsetX;
                        newHeight += offsetY;
                        break;
                    case HitType.Left:
                        newX += offsetX;
                        newWidth -= offsetX;
                        break;
                    case HitType.Right:
                        newWidth += offsetX;
                        break;
                    case HitType.Bottom:
                        newHeight += offsetY;
                        break;
                    case HitType.Top:
                        newY += offsetY;
                        newHeight -= offsetY;
                        break;
                }

                // Avoids negative width or height.
                if (newWidth > (GAP * 2) && newHeight > (GAP * 2))
                {
                    // Updates the element.
                    Canvas.SetLeft(element, newX);
                    Canvas.SetTop(element, newY);
                    element.GetType().GetProperty("Width").SetValue(element, newWidth);
                    element.GetType().GetProperty("Height").SetValue(element, newHeight);

                    // Reminds the mouse's new location.
                    lastPoint = mousePosition;
                }
            }
            else
            {
                // Finds out the mouse hit type and sets the mouse cursor.
                mouseHitType = SetHitType(element, Mouse.GetPosition(element.Parent as FrameworkElement));
                SetMouseCursor(element);
            }
        }

        /// <summary>
        /// Stops the drag-and-resize operation and resets the mouse cursor.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Dragging is finished and the default mouse cursor restored.
            dragInProgress = false;
            CommonMethods.DisplayCursor(designerwindow, defaultCursor);

            // Gets the actual position of the control and recalculates the coordinates.
            FrameworkElement movingObject = (FrameworkElement)sender;
            double xpos = Canvas.GetLeft(movingObject);
            double ypos = Canvas.GetTop(movingObject);
            xpos = Math.Round(xpos / 10) * 10;
            ypos = Math.Round(ypos / 10) * 10;

            // The control gets the final left and top position.
            Canvas.SetLeft(movingObject, xpos);
            Canvas.SetTop(movingObject, ypos);
        }

        /// <summary>
        /// Reminds the mouse cursor layout when entering an element on the designer mask.
        /// While the MouseLeave-event the cursor will be restored.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        public void OnMouseEnter(object sender, MouseEventArgs e)
        {
            defaultCursor = designerwindow.Cursor;
        }

        /// <summary>
        /// Stops the darg-and-resize operation and resets the mouse cursor.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        public void OnMouseLeave(object sender, MouseEventArgs e)
        {
            // Dragging is finished and the default mouse cursor restored.
            dragInProgress = false;
            CommonMethods.DisplayCursor(designerwindow, defaultCursor);
        }

        /// <summary>
        /// Returns a HitType value to indicate what is at the point.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="mousePosition">The actual mouse-position on the canvas.</param>
        /// <returns>
        /// Returns an object of type HitType.
        /// </returns>
        private HitType SetHitType(FrameworkElement element, Point mousePosition)
        {
            // Finds the actual coordinates of the element on the Canvas.
            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);

            // Calculates the actual size of the element.
            double right = left + Double.Parse(element.GetType().GetProperty("ActualWidth").GetValue(element).ToString());
            double bottom = top + Double.Parse(element.GetType().GetProperty("ActualHeight").GetValue(element).ToString());

            if (mousePosition.X < left) return HitType.None;
            if (mousePosition.X > right) return HitType.None;
            if (mousePosition.Y < top) return HitType.None;
            if (mousePosition.Y > bottom) return HitType.None;

            if (mousePosition.X - left < GAP)
            {
                // Left edge.
                if (mousePosition.Y - top < GAP) return HitType.TopLeft;
                if (bottom - mousePosition.Y < GAP) return HitType.BottomLeft;
                return HitType.Left;
            }
            else if (right - mousePosition.X < GAP)
            {
                // Right edge.
                if (mousePosition.Y - top < GAP) return HitType.TopRight;
                if (bottom - mousePosition.Y < GAP) return HitType.BottomRight;
                return HitType.Right;
            }

            if (mousePosition.Y - top < GAP) return HitType.Top;
            if (bottom - mousePosition.Y < GAP) return HitType.Bottom;

            return HitType.Body;
        }

        /// <summary>
        /// Sets a mouse cursor appropriate for the current hit type.
        /// </summary>
        private void SetMouseCursor(FrameworkElement element)
        {
            // Sees what cursor we should display.
            // Default is the default mouse cursor.
            Cursor desiredCursor = defaultCursor;

            switch (mouseHitType)
            {
                case HitType.None:
                    desiredCursor = Cursors.Arrow;
                    break;
                case HitType.Body:
                    desiredCursor = Cursors.ScrollAll;
                    break;
                case HitType.BottomLeft:
                case HitType.TopRight:
                    desiredCursor = Cursors.SizeNESW;
                    break;
                case HitType.TopLeft:
                case HitType.BottomRight:
                    desiredCursor = Cursors.SizeNWSE;
                    break;
                case HitType.Top:
                case HitType.Bottom:
                    desiredCursor = Cursors.SizeNS;
                    break;
                case HitType.Left:
                case HitType.Right:
                    desiredCursor = Cursors.SizeWE;
                    break;
            }

            // Displays the desired cursor.
            if (designerwindow.Cursor != desiredCursor)
            {
                CommonMethods.DisplayCursor(designerwindow, desiredCursor);
            }

            //// Especially in case of TextBox (and other controls) this code displays the desired cursor.
            //if (element.GetType().GetProperty("Cursor") != null &&
            //    (Cursor)element.GetType().GetProperty("Cursor").GetValue(element) != desiredCursor)
            //{
            //    element.GetType().GetProperty("Cursor").SetValue(element, desiredCursor);
            //    // Needed for displaying the desired cursor when the mouse is over the center of the CustomTextBox p.e.
            //    Mouse.OverrideCursor = desiredCursor;
            //}
        }

    }
}
