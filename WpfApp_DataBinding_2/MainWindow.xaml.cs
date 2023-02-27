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

namespace WpfApp_DataBinding_2
{
    // The DataContext property is the default source of your bindings, unless you specifically declare another source,
    // like with the ElementName property.
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            Binding binding1 = new Binding("Text");
            binding1.Source = textBox2;
            textBlock1.SetBinding(TextBlock.TextProperty, binding1);
        }
    }
}
