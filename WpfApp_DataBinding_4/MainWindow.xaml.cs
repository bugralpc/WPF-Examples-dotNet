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

using System.ComponentModel;

namespace WpfApp_DataBinding_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person person = new Person("Sauron", "1000");

        public MainWindow()
        {
            InitializeComponent();

            Binding binding1 = new Binding("Name");
            binding1.Source = person;
            textBox1.SetBinding(TextBox.TextProperty, binding1);

            Binding binding2 = new Binding("Age");
            binding2.Source = person;
            textBox2.SetBinding(TextBox.TextProperty, binding2);

        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            person.Name = textBox3.Text;
            person.Age = textBox4.Text;

            string message = person.Name + " is " + person.Age;

            MessageBox.Show(message);

            string debug = "debug";
        }

        private void ShowClick(object sender, RoutedEventArgs e)
        {
            string message = person.Name + " is " + person.Age;

            MessageBox.Show(message);

            string debug = "debug";
        }
    }

    public class Person
    {
        private string? name;
        private string? age;
        public string? Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string? Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public Person(string name, string age)
        {
            this.name = name;
            this.age = age;
        }
    }
}
