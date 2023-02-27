using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp_INotify_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person person = new Person("Sauron", "1000");

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
        }

        private void ShowClassClick(object sender, RoutedEventArgs e)
        {
            string message = person.Name + " is " + person.Age;
            MessageBox.Show(message);
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string? name; // field
        private string? age; // field
        public string Name // Property
        { 
            get
            {
                return name;
            } 
            set
            {
                name = value;
                this.NotifyPropertyChanged("Name");
            }
        }
        public string Age // Property
        { 
            get
            {
                return age;
            }
            set
            {
                age = value;
                this.NotifyPropertyChanged("Age");
            }
        } 

        public Person(string name, string age)
        {
            this.name = name;
            this.age = age;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string str)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(str));
        }

    }


}
