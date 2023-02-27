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

namespace WpfApp_DataBinding_1
{

    // Data-binding, the management of data is entirely separated from the way data.
    // When a binding is established and the data or your business model changes, then it reflects the updates automatically to the UI or vice verca.

    // One-way data binding, data is bound from its source (that is the object that holds the data) to its target (that is the object
    // that displays the data)

    public partial class MainWindow : Window
    {
        Person person1 = new Person { Name = "Sauron", Age = "Unknown" };
        Person person2 = new Person { Name = "Aragorn", Age = "87" };

        public MainWindow()
        {
            InitializeComponent();

            // The text properties of both the textboxes bind to Name and Age which are class variables of Person class
            nameTextBox1.DataContext = person1;
            ageTextBox1.DataContext = person1;

            nameTextBox2.DataContext = person2;
            ageTextBox2.DataContext = person2;



        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            string message = person1.Name + " is " + person1.Age;
            MessageBox.Show(message);
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            string message = person2.Name + " is " + person2.Age;
            MessageBox.Show(message);
        }
    }

    public class Person
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
    }
}
