using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mirages.Tabs
{
    /// <summary>
    /// Interaction logic for BinarizationTab.xaml
    /// </summary>
    public partial class BinarizationTab : UserControl
    {
        public BinarizationTab()
        {
            InitializeComponent();
        }

        private void threshold_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var text = (sender as TextBox).Text.Insert(s.SelectionStart, e.Text);

            // If parsing is successful, set Handled to false
            e.Handled = !Double.TryParse(text, out double d);
        }
    }
}
