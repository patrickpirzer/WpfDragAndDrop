﻿using P16_CustomControlLibrary;
using System.Windows;
using System.Windows.Controls;

namespace P16_Drag_and_Drop
{
    /// <summary>
    /// Class for the creating controls to the mask designer.
    /// </summary>
    public class ControlCreator
    {
        /// <summary>
        /// Creates a control according to the given element and returns it.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// If successfull the method returns the control as an object of type FrameworkElement.
        /// Otherwise it returns null.
        /// </returns>
        public FrameworkElement GetControl(FrameworkElement element)
        {
            FrameworkElement newControl = null;

            if (element.GetType() == typeof(Label))
            {
                newControl = new Label();
                newControl.GetType().GetProperty("Content").SetValue(newControl, "Label");
                newControl.GetType().GetProperty("Background").SetValue(newControl, new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.AliceBlue));
                newControl.GetType().GetProperty("Width").SetValue(newControl, double.NaN);
                Label lbl = new Label();
            }
            else if (element.GetType() == typeof(TextBox))
            {
                newControl = new TextBox();
                newControl.GetType().GetProperty("Width").SetValue(newControl, 50);
            }
            else if (element.GetType() == typeof(CustomTextBox))
            {
                newControl = new CustomTextBox();
                newControl.GetType().GetProperty("Width").SetValue(newControl, 50);
            }

            return newControl;
        }
    }
}
