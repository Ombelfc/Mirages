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

namespace Mirages.Controls
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
