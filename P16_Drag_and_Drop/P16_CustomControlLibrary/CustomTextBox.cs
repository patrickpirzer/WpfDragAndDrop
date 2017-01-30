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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P16_CustomControlLibrary
{
    /// <summary>
    /// Class for a CustomControl which inherits from TextBox and adds additional properties.
    /// </summary>
    public class CustomTextBox : TextBox
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        static CustomTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBox), new FrameworkPropertyMetadata(typeof(CustomTextBox)));
        }

        /// <summary>
        /// DependencyProperty for external access to the property TableName.
        /// </summary>
        public static readonly DependencyProperty TableNameProperty =
            DependencyProperty.Register("TableName", typeof(string), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty for external access to the IsRequired-property.
        /// </summary>
        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register("IsRequired", typeof(bool), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty with which we can check, if the control is visible on the input form.
        /// </summary>
        public static readonly DependencyProperty IsVisibleInInputFormProperty =
            DependencyProperty.Register("IsVisibleInInputForm", typeof(bool), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty with which we can check, if the control is visible on the testprotocol.
        /// </summary>
        public static readonly DependencyProperty IsVisibleInTestprotocolProperty =
            DependencyProperty.Register("IsVisibleInTestprotocol", typeof(bool), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty for external access to the CanBeEdited-property.
        /// </summary>
        public static readonly DependencyProperty CanBeEditedProperty =
            DependencyProperty.Register("CanBeEdited", typeof(bool), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty for external access to the DatabaseFieldName-property.
        /// </summary>
        public static readonly DependencyProperty DatabaseFieldNameProperty =
            DependencyProperty.Register("DatabaseFieldName", typeof(string), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty for external access to the InputFieldId-property.
        /// </summary>
        public static readonly DependencyProperty InputFieldIdProperty =
            DependencyProperty.Register("InputFieldId", typeof(int), typeof(CustomTextBox));

        /// <summary>
        /// DependencyProperty for external access to the DescriptionText-property.
        /// </summary>
        public static readonly DependencyProperty DescriptionTextProperty =
            DependencyProperty.Register("DescriptionText", typeof(string), typeof(CustomTextBox));

        /// <summary>
        /// Gets or sets the name of the datatable which is the datasource.
        /// </summary>
        public string TableName
        {
            get { return (string)GetValue(TableNameProperty); }
            set { SetValue(TableNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets if input in the control is required.
        /// </summary>
        public bool IsRequired
        {
            get { return bool.Parse(GetValue(IsRequiredProperty).ToString()); }
            set { SetValue(IsRequiredProperty, value); }
        }

        /// <summary>
        /// Gets or sets if input in the control is visible on the input form.
        /// </summary>
        public bool IsVisibleInInputForm
        {
            get { return bool.Parse(GetValue(IsVisibleInInputFormProperty).ToString()); }
            set { SetValue(IsVisibleInInputFormProperty, value); }
        }

        /// <summary>
        /// Gets or sets if input in the control is visible on the testprotocol.
        /// </summary>
        public bool IsVisibleInTestprotocol
        {
            get { return bool.Parse(GetValue(IsVisibleInTestprotocolProperty).ToString()); }
            set { SetValue(IsVisibleInTestprotocolProperty, value); }
        }

        /// <summary>
        /// Gets or sets if input in the control can be edited.
        /// </summary>
        public bool CanBeEdited
        {
            get { return bool.Parse(GetValue(CanBeEditedProperty).ToString()); }
            set { SetValue(CanBeEditedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the database fieldname.
        /// </summary>
        public string DatabaseFieldName
        {
            get { return (string)GetValue(DatabaseFieldNameProperty); }
            set { SetValue(DatabaseFieldNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input_field_id.
        /// </summary>
        public int InputFieldId
        {
            get { return int.Parse(GetValue(InputFieldIdProperty).ToString()); }
            set { SetValue(InputFieldIdProperty, value); }
        }

        /// <summary>
        /// Gets or sets the description text.
        /// </summary>
        public string DescriptionText
        {
            get { return (string)GetValue(DescriptionTextProperty); }
            set { SetValue(DescriptionTextProperty, value); }
        }
    }
}
