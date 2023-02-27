using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApp_DataBinding_3
{
    // URLs
    // https://wpf-tutorial.com/data-binding/the-update-source-trigger-property/

    public partial class MainWindow : Window
    {
        private List<User> users = new List<User>();

        //private ObservableCollection<User> users = new ObservableCollection<User>();
        public MainWindow()
        {
            InitializeComponent();

            users.Add(new User() { Name = "Sauron"});
            users.Add(new User() { Name = "Galadriel" });

            listBox1.ItemsSource = users;

        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New User" });
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            if(listBox1 != null)
            {
                (listBox1.SelectedItem as User).Name = "Random Name";
            }
        }

        private void Button3Click(object sender, RoutedEventArgs e)
        {
            if(listBox1 != null)
            {
                users.Remove(listBox1.SelectedItem as User);
            }
        }
    }

    public class User : INotifyPropertyChanged
    {
        private string name;
        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }



}
