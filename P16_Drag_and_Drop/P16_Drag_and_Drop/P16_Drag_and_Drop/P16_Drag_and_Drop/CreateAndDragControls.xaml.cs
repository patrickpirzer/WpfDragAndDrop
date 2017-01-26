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
    /// Class for designing masks for P16-tests.
    /// </summary>
    public partial class CreateAndDragControls : Window
    {
        /// <summary>
        /// Field for an object of the class <see cref="CreateAndDragControlsViewModel"/> class.
        /// </summary>
        private CreateAndDragControlsViewModel vm = null;

        /// <summary>
        /// The constructor.
        /// </summary>
        public CreateAndDragControls()
        {
            InitializeComponent();

            // The viewmodel is created and gets the designerpanel.
            vm = new CreateAndDragControlsViewModel(designerpanel, this);
        }

        /// <summary>
        /// Starts the adding of a new label control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_label_Click(object sender, RoutedEventArgs e)
        {
            vm.StartAddingLabel();
        }

        /// <summary>
        /// Starts the adding of a new textbox control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_textbox_Click(object sender, RoutedEventArgs e)
        {
            vm.StartAddingTextbox();
        }

        /// <summary>
        /// Aborts the adding action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_abort_Click(object sender, RoutedEventArgs e)
        {
            vm.AbortAddingAction();
        }

        /// <summary>
        /// The mouse cursor enters the designerpanel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void designerpanel_MouseEnter(object sender, MouseEventArgs e)
        {
            vm.designerpanelMouseEnter();
        }

        /// <summary>
        /// The mouse cursor leaves the designerpanel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void designerpanel_MouseLeave(object sender, MouseEventArgs e)
        {
            vm.designerpanelMouseLeave();
        }

        /// <summary>
        /// A mouse-button is clicked while the cursor is in the designerpanel.
        /// Now the control chosen for adding shall be added to the designerpanel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void designerpanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            vm.designerpanelMouseDown();
        }

        /// <summary>
        /// The cursor is moved inside the designerpanel.
        /// Actually not in use.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void designerpanel_MouseMove(object sender, MouseEventArgs e)
        {
        }

    }
}
