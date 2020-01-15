using SAUSALibrary.FileHandling.XML.Writing;
using SAUSALibrary.FileHandling.Database;
using SAUSALibrary.Defaults;
using SAUSALibrary.Models;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace WpfApp1.ViewModels
{
    public class NewRoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ProjectStorageDimensionsModel? _StorageModel;

        public ProjectStorageDimensionsModel StorageModel
        {
            get => _StorageModel;
            set => SetProperty(ref _StorageModel, value);
        }

        public RelayCommand ApplyRoomDimensions { get; }

        public NewRoomViewModel()
        {
            ApplyRoomDimensions = new RelayCommand(OnApplyRoomDimensions);
        }

        private void OnApplyRoomDimensions()
        {

        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value)) //if using custom classes need to implement equals
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
