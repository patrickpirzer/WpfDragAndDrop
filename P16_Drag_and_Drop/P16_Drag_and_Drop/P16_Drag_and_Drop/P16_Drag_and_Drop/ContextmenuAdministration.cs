using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace P16_Drag_and_Drop
{
    /// <summary>
    /// Class for the adminstration of contextmenus to controls.
    /// </summary>
    public class ContextmenuAdministration
    {
        private ChangeElementProperties changeElementProperties;

        /// <summary>
        /// Creates the contextmenu and adds it to the element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ElementAddContextMenu(FrameworkElement element)
        {
            // Creates the contextmenu.
            ContextMenu menu = new ContextMenu();
            menu.Items.Add(CreateEditItemForContextmenu(element));
            menu.Items.Add(CreateRemoveItemForContextmenu(element));
            element.ContextMenu = menu;
        }

        private MenuItem CreateEditItemForContextmenu(FrameworkElement element)
        {
            MenuItem menuitem = new MenuItem();
            menuitem.Header = "Edit";
            menuitem.DataContext = element;
            menuitem.Click += EditElementProperties;
            return menuitem;
        }

        private MenuItem CreateRemoveItemForContextmenu(FrameworkElement element)
        {
            MenuItem menuitem = new MenuItem();
            menuitem.Header = "Remove";
            menuitem.DataContext = element;
            menuitem.Click += RemoveElement;
            return menuitem;
        }

        /// <summary>
        /// Opens the change-window and adjusts it's settings (text, buttons) according to the properties of the element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        private void EditElementProperties(object sender, RoutedEventArgs e)
        {
            var element = ((MenuItem)sender).DataContext;

            if (changeElementProperties == null)
            {
                // The window for changing the properties of the element is opened in modal mode.
                // After the window has closed, the field is resetted.
                changeElementProperties = new ChangeElementProperties(element as FrameworkElement);
                changeElementProperties.ShowDialog();
                changeElementProperties = null;
            }
        }

        /// <summary>
        /// TODO: Tries to remove the selected element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventarguments.</param>
        private void RemoveElement(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)((MenuItem)sender).DataContext;
            if (element.GetType().GetProperty("Parent") != null && element.GetType().GetProperty("Parent").GetValue(element) != null)
            {
                var parent = element.GetType().GetProperty("Parent").GetValue(element);
                if (parent != null)
                {
                    // Get the children-collection by reflection and remove the element from it.
                    if (parent.GetType().GetProperty("Children") != null && parent.GetType().GetProperty("Children").GetValue(parent) != null)
                    {
                        var children = parent.GetType().GetProperty("Children").GetValue(parent);

                        // Get the Remove-Method by refelction and invoke it with parameters.
                        MethodInfo removeMethod = children.GetType().GetMethod("Remove");

                        if (removeMethod != null)
                        {
                            object[] parameters = new object[] { element };
                            removeMethod.Invoke(children, parameters);

                            ////if (element.GetType().GetProperty("DatabaseFieldName") != null && element.GetType().GetProperty("DatabaseFieldName").GetValue(element) != null)
                            ////{
                            ////    // Remove data to the element from the database.
                            ////    int input_field_id = int.Parse(element.GetType().GetProperty("InputFieldId").GetValue(element).ToString());

                            ////    // Removes the formula field attributes.
                            ////    if (element.GetType().GetProperty("SelectedFormulas") != null && element.GetType().GetProperty("SelectedFormulas").GetValue(element) != null)
                            ////    {
                            ////        var selectedFormulas = (SelectedFormulaCollection)element.GetType().GetProperty("SelectedFormulas").GetValue(element);
                            ////        if (selectedFormulas != null && selectedFormulas.Count > 0)
                            ////        {
                            ////            int formula_id = (new DAO_Formulas()).GetFormulaIdByFormulaName(selectedFormulas[0].ToString());
                            ////            (new DAO_Formula_field_attributes()).RemoveFormulaFieldAttributes(input_field_id);
                            ////        }
                            ////    }

                            ////    // Removes the entry from Input_fields.
                            ////    new DAO_Input_fields().RemoveInputField(input_field_id);

                            ////    // Writes the XML file.
                            ////    if (AppSettings.FormularGrid != null)
                            ////    {
                            ////        string filePath = System.Configuration.ConfigurationManager.AppSettings["SerializedXmlFolderPath"].ToString() + "\\" + AppSettings.MaskName + ".xml";
                            ////        new CommonMethods().SerializeToXmlAndWriteXmlFile(filePath, AppSettings.FormularGrid);
                            ////    }
                            ////}

                        }

                    }
                }
            }
        }

    }
}
