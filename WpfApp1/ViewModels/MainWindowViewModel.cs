using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {        

        private string _Text;
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);            
        }

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => Set(ref _selectedPerson, value);
        }

        private Visibility _FieldVisibility;

        public Visibility FieldVisibility
        {
            get => _FieldVisibility;
            set => Set(ref _FieldVisibility, value);
        }

        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>(); //notifys if data is changed automatically

        public RelayCommand ChangeNameCommand { get; } //command referenced by command binding in xaml file

        public RelayCommand AddPersonCommand { get; } //command referenced by command binding in xaml file

        private Func<DateTime> GetNow { get; }

        private bool CanExecute { get; set; }

        public MainWindowViewModel() //constructor
        {
            Text = "Container List"; //default when this Window loads
            ChangeNameCommand = new RelayCommand(OnChangeName, () => CanExecute );
            AddPersonCommand = new RelayCommand(OnAddPerson);
            

            FieldVisibility = Visibility.Visible;
        }

        private void OnAddPerson()
        {
            var dob = GetNow().Subtract(TimeSpan.FromDays(30 * 365));
            People.Add(new Person("Foo", $"Bar { People.Count}", dob));
            CanExecute = true;
            ChangeNameCommand.RaiseCanExecuteChanged();
        }

        private void OnChangeName()
        {
            //set up to change a simple textbox, largely irrelevent for project
            Text = "Kevin";
            //VisibilityToggle();
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
