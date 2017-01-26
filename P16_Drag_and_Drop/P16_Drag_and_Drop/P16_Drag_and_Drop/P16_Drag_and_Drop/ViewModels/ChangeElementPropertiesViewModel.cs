using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace P16_Drag_and_Drop.ViewModels
{
    /// <summary>
    /// Viewmodel for changing the properties of the element.
    /// </summary>
    public class ChangeElementPropertiesViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Field for the element whose properties shall be changed.
        /// </summary>
        private FrameworkElement fwElement;

        /// <summary>
        /// Field for the text/content of the element.
        /// </summary>
        private string strText = "";

        /// <summary>
        /// Eventhandler for signalising that a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="element">The element.</param>
        public ChangeElementPropertiesViewModel(FrameworkElement element)
        {
            fwElement = element;

            GetElementProperties();
        }

        /// <summary>
        /// Tries to set the püroperties of the element.
        /// </summary>
        public void SetElementProperties()
        {
            if (fwElement.GetType().GetProperty("Content") != null)
            {
                fwElement.GetType().GetProperty("Content").SetValue(fwElement, StrText);
            }

            if (fwElement.GetType().GetProperty("Text") != null)
            {
                fwElement.GetType().GetProperty("Text").SetValue(fwElement, StrText);
            }
        }

        /// <summary>
        /// Gets or sets the text/content of the element.
        /// </summary>
        public string StrText
        {
            get { return strText; }
            set
            {
                strText = value;
                RaisePropertyChanged("StrText");
            }
        }

        /// <summary>
        /// Informs the target which is bound to a property, that it's source was changed and that it shall update.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Tries to get the properties and its data from the element.
        /// </summary>
        private void GetElementProperties()
        {
            if (fwElement == null)
            {
                return;
            }

            GetElementText();
        }

        /// <summary>
        /// Tries to get the text/content of the element.
        /// </summary>
        private void GetElementText()
        {
            if (fwElement.GetType().GetProperty("Content") != null && fwElement.GetType().GetProperty("Content").GetValue(fwElement) != null)
            {
                StrText = fwElement.GetType().GetProperty("Content").GetValue(fwElement).ToString();
            }

            if (fwElement.GetType().GetProperty("Text") != null && fwElement.GetType().GetProperty("Text").GetValue(fwElement) != null)
            {
                StrText = fwElement.GetType().GetProperty("Text").GetValue(fwElement).ToString();
            }
        }

    }
}
