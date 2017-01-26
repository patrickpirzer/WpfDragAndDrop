using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using P16_Drag_and_Drop.ViewModels;

namespace P16_Drag_and_Drop
{
    /// <summary>
    /// Class for setting and changing the properties of elements.
    /// </summary>
    public partial class ChangeElementProperties : Window
    {
        /// <summary>
        /// Field for an object of the <see cref="ChangeElementPropertiesViewModel"/> class.
        /// </summary>
        private ChangeElementPropertiesViewModel vm;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="element">The element.</param>
        public ChangeElementProperties(FrameworkElement element)
        {
            InitializeComponent();
            vm = new ChangeElementPropertiesViewModel(element);
            DataContext = vm;
        }

        /// <summary>
        /// Tries to set the properties of the element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_set_properties_Click(object sender, RoutedEventArgs e)
        {
            vm.SetElementProperties();
            Close();
        }
    }
}
