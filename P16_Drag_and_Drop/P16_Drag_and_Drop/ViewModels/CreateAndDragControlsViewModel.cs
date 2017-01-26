using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P16_Drag_and_Drop.ViewModels
{
    /// <summary>
    /// ViewModel for creating and dragging controls on the mask designer window.
    /// </summary>
    public class CreateAndDragControlsViewModel
    {
        /// <summary>
        /// Field for the control which is created on the designer window.
        /// </summary>
        private FrameworkElement myControl = null;

        /// <summary>
        /// Field for an object of the designerpanel.
        /// On that panel the control are created, dragged and resized.
        /// </summary>
        private Canvas designerpanel;

        /// <summary>
        /// Field for an object of the designerwindow (= CreateAndDragControls.xaml).
        /// </summary>
        private Window designerwindow;

        /////// <summary>
        /////// Field for an object of the <see cref="DragElementActions"/> class.
        /////// </summary>
        ////private DragElementActions dragElementActions;

        /// <summary>
        /// Field for an object of the <see cref="DragAndResizeElementActions"/> class.
        /// </summary>
        private DragAndResizeElementActions dragAndResizeElementActions;

        /// <summary>
        /// Field for an object of the <see cref="ContextmenuAdministration"/> class.
        /// </summary>
        private ContextmenuAdministration contextmenuAdministration;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="panelForMaskDesign">The panel for designing a mask.</param>
        /// <param name="windowForMaskDesign">The window for designing a mask.</param>
        public CreateAndDragControlsViewModel(Canvas panelForMaskDesign, Window windowForMaskDesign)
        {
            designerpanel = panelForMaskDesign;
            designerwindow = windowForMaskDesign;

            // First version for dragging elements.
            ////dragElementActions = new DragElementActions();

            // Second alternative version for dragging and resizing elements.
            dragAndResizeElementActions = new DragAndResizeElementActions(designerwindow);

            contextmenuAdministration = new ContextmenuAdministration();
        }

        /// <summary>
        /// Starts the adding of a new label control.
        /// </summary>
        public void StartAddingLabel()
        {
            myControl = new Label();
            designerwindow.Cursor = Cursors.No;
        }

        /// <summary>
        /// Starts the adding of a new textbox control.
        /// </summary>
        public void StartAddingTextbox()
        {
            myControl = new TextBox();
            designerwindow.Cursor = Cursors.No;
        }

        /// <summary>
        /// Aborts the adding action.
        /// </summary>
        public void AbortAddingAction()
        {
            myControl = null;
            designerwindow.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// The mouse cursor enters the designerpanel.
        /// </summary>
        public void designerpanelMouseEnter()
        {
            if (myControl == null)
            {
                return;
            }

            // If myControl is set the cursor has to become a cross.
            designerwindow.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// The mouse cursor leaves the designerpanel.
        /// </summary>
        public void designerpanelMouseLeave()
        {
            if (myControl == null)
            {
                return;
            }

            // If myControl is set the cursor has to become the forbidden sign.
            designerwindow.Cursor = Cursors.No;
        }

        /// <summary>
        /// A mouse-button is clicked while the cursor is in the designerpanel.
        /// Now the control chosen for adding shall be added to the designerpanel.
        /// </summary>
        public void designerpanelMouseDown()
        {
            FrameworkElement newControl = null;

            if (myControl == null)
            {
                return;
            }

            newControl = (new ControlCreator()).GetControl(myControl);

            // If newControl is set, the new control will be added to the designerpanel.
            if (newControl != null)
            {
                // Gets the actual position of the cursor and recalculates the coordinates.
                double xpos = Mouse.GetPosition(designerpanel).X;
                double ypos = Mouse.GetPosition(designerpanel).Y;
                xpos = Math.Round(xpos / 10) * 10;
                ypos = Math.Round(ypos / 10) * 10;

                // The new control gets the final left and top position.
                Canvas.SetLeft(newControl, xpos);
                Canvas.SetTop(newControl, ypos);

                // Adds eventhandlers to the new control for drag and drop.
                ////newControl.PreviewMouseLeftButtonDown += dragElementActions.ControlPreviewMouseLeftButtonDown;
                ////newControl.PreviewMouseMove += dragElementActions.ControlPreviewMouseMove;
                ////newControl.PreviewMouseLeftButtonUp += dragElementActions.ControlPreviewMouseLeftButtonUp;

                // Adds eventhandlers to the new control for drag, drop and resize.
                newControl.PreviewMouseLeftButtonDown += dragAndResizeElementActions.OnElementMouseDown;
                newControl.PreviewMouseMove += dragAndResizeElementActions.OnElementMouseMove;
                newControl.PreviewMouseLeftButtonUp += dragAndResizeElementActions.OnMouseUp;
                newControl.MouseEnter += dragAndResizeElementActions.OnMouseEnter;
                newControl.MouseLeave += dragAndResizeElementActions.OnMouseLeave;

                // Creates the context menu and adds it to the new control.
                contextmenuAdministration.ElementAddContextMenu(newControl);

                // Adds the new control to the designerpanel.
                designerpanel.Children.Add(newControl);
            }

            myControl = null;
            designerwindow.Cursor = Cursors.Arrow;
        }

    }
}
