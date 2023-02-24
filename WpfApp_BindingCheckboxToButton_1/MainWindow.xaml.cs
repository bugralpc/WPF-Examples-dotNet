using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp_BindingCheckboxToButton_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        public string Deneme { get; set; }

        public string AreItemsChecked
        {
            get
            {
                foreach(CheckBox checkBox in checkBoxes)
                {
                    if ((bool)checkBox.IsChecked)
                    {
                        return "asdasd";
                    }
                }
                return "dddddd";
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            string[] checkBoxNames = { "checkBox1", "checkBox2", "checkBox3" };

            
            foreach(string checkBoxName in checkBoxNames)
            {
                CheckBox cb = new CheckBox();
                cb.Content = checkBoxName;
                cb.IsChecked = false;

                stackPanel2.Children.Add(cb);

                checkBoxes.Add(cb);
            }

            Deneme = "asd";

            A.DataContext = Deneme;

            string debug = "debug";

        }        
    }
}
