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
        private Person person = new Person() { Name = "Sauron", Age = "1000" };

        public MainWindow()
        {
            InitializeComponent();

            textBox1.DataContext = person;
            textBox2.DataContext = person;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            person.Name = textBox3.Text;
            person.Age = textBox4.Text;

            string message = person.Name + " is " + person.Age;

            MessageBox.Show(message);
        }

        private void ShowClick(object sender, RoutedEventArgs e)
        {
            string message = person.Name + " is " + person.Age;

            MessageBox.Show(message);
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string name;
        public string? Name
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
        public string? Age { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
