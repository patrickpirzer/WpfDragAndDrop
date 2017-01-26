using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P16_Drag_and_Drop
{
    [Obsolete]
    /// <summary>
    /// Class for dragging existing elements inside a panel.
    /// This class is not used anymore.
    /// </summary>
    public class DragElementActions
    {
        /// <summary>
        /// Field for the x-position of the cursor on an existing control while mouse left button down.
        /// This field is set when the user starts to change its position on the designerpanel via dragging.
        /// </summary>
        private double firstXPos;

        /// <summary>
        /// Field for the y-position of the cursor on an existing control while mouse left button down.
        /// This field is set when the user starts to change its position on the designerpanel via dragging.
        /// </summary>
        private double firstYPos;

        /// <summary>
        /// Field for the x-position of the cursor when the user starts to change the position of an existing control via dragging.
        /// </summary>
        private double firstCursorXPos;

        /// <summary>
        /// Field for the y-position of the cursor when the user starts to change the position of an existing control via dragging.
        /// </summary>
        private double firstCursorYPos;

        /// <summary>
        /// Field for an existing control whose position on the designerpanel is changed by the user via dragging.
        /// </summary>
        private FrameworkElement movingObject;

        /// <summary>
        /// An existing control is clicked with the left mouse button.
        /// This starts dragging of the control on the designerpanel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //In this event, we get current mouse position on the control to use it in the MouseMove event.
            firstXPos = e.GetPosition(sender as Control).X;
            firstYPos = e.GetPosition(sender as Control).Y;
            firstCursorXPos = e.GetPosition((sender as Control).Parent as Control).X - firstXPos;
            firstCursorYPos = e.GetPosition((sender as Control).Parent as Control).Y - firstYPos;

            movingObject = sender as FrameworkElement;
        }

        /// <summary>
        /// While the control is dragged within the designerpanel, its position is recalculated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlPreviewMouseMove(object sender, MouseEventArgs e)
        {
            // If movingObject is not set we leave the method here.
            if (movingObject == null)
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Checks if the position of the control is outside the panel and corrects it.
                double xpos = Canvas.GetLeft(movingObject);
                if (xpos < 0)
                {
                    Canvas.SetLeft(movingObject, 0);
                    return;
                }

                double ypos = Canvas.GetTop(movingObject);
                if (ypos < 0)
                {
                    Canvas.SetTop(movingObject, 0);
                    return;
                }

                // Calculates the position of the control while dragging.
                movingObject.SetValue(Canvas.LeftProperty,
                     e.GetPosition(movingObject.Parent as FrameworkElement).X - firstXPos);

                movingObject.SetValue(Canvas.TopProperty,
                     e.GetPosition(movingObject.Parent as FrameworkElement).Y - firstYPos);
            }
        }

        /// <summary>
        /// When the left mouse button is released, the control is dropped on the designerpanel and the dragging action finished.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // If movingObject is not set we leave the method here.
            if (movingObject == null)
            {
                return;
            }

            // Gets the actual position of the control and recalculates the coordinates.
            double xpos = Canvas.GetLeft(movingObject);
            double ypos = Canvas.GetTop(movingObject);
            xpos = Math.Round(xpos / 10) * 10;
            ypos = Math.Round(ypos / 10) * 10;

            // Checks if the position of the control is outside the panel and corrects it.
            if (xpos < 0) { xpos = 0; }
            if (ypos < 0) { ypos = 0; }

            // The control gets the final left and top position.
            Canvas.SetLeft(movingObject, xpos);
            Canvas.SetTop(movingObject, ypos);

            // The field movingObject is resetted.
            movingObject = null;
        }

    }
}
