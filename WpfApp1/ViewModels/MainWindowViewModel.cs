using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value,
            [CallerMemberName]string propertyName = null) //<- this is an optional parameter so we dont have to pass a value to use the method
        {
            if (!EqualityComparer<T>.Default.Equals(field, value)) //if using custom classes need to implement equals
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Text;
        public string Text
        {
            get => _Text;
            set => SetProperty(ref _Text, value);            
        }

        private Visibility _FieldVisibility;

        public Visibility FieldVisibility
        {
            get => _FieldVisibility;
            set => SetProperty(ref _FieldVisibility, value);
        }

        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>(); //notifys if data is changed automatically

        public ICommand ChangeNameCommand { get; } //command referenced by command binding in xaml file

        public ICommand AddPersonCommand { get; } //command referenced by command binding in xaml file

        public MainWindowViewModel() //constructor
        {
            Text = "Container List"; //default when this Window loads
            ChangeNameCommand = new Command(OnChangeName);
            AddPersonCommand = new Command(OnAddPerson);
            FieldVisibility = Visibility.Visible;
        }

        private void OnAddPerson()
        {            
            //get new data from view and add to ObservableCollection            
        }

        private void OnChangeName()
        {
            //set up to change a simple textbox, largely irrelevent for project
            Text = "Kevin";
            VisibilityToggle();
        }

        private void VisibilityToggle()
        {
            if(FieldVisibility == Visibility.Hidden)
            {
                FieldVisibility = Visibility.Visible;
            } else
            {
                FieldVisibility = Visibility.Hidden;
            }
        }
    }
}
