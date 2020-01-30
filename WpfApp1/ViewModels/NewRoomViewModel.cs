using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using SAUSALibrary.FileHandling.XML.Writing;
using SAUSALibrary.Defaults;

namespace WpfApp1.ViewModels
{
    public class NewRoomViewModel : ViewModelBase
    {
        
        private string? _XStorageDimension;
        public string? XStorageDimension
        {
            get => _XStorageDimension;
            set => Set(ref _XStorageDimension, value);
        }

        private string? _YStorageDimension;
        public string? YStorageDimension
        {
            get => _YStorageDimension;
            set => Set(ref _YStorageDimension, value);
        }
        private string? _ZStorageDimension;
        public string? ZStorageDimension
        {
            get => _ZStorageDimension;
            set => Set(ref _ZStorageDimension, value);
        }
        private string? _WeightStorageMax;
        public string? WeightStorageMax
        {
            get => _WeightStorageMax;
            set => Set(ref _WeightStorageMax, value);
        }

        public RelayCommand ApplyRoomDimensions { get; }

        public NewRoomViewModel()
        {            
            ApplyRoomDimensions = new RelayCommand(OnApplyRoomDimensions);
            InitStorageFields();
        }

        private void OnApplyRoomDimensions()
        {
            if(RoomDimensionFieldValidator())
            {
                string[] dimensions = new string[4];
                string[] NewRoomDimensions = {XStorageDimension,YStorageDimension,ZStorageDimension,WeightStorageMax };
                dimensions[0] = XStorageDimension;
                dimensions[1] = YStorageDimension;
                dimensions[2] = ZStorageDimension;
                dimensions[3] = WeightStorageMax;

                //send array to the write method
                WriteXML.SaveDimensions(FilePathDefaults.ScratchFolder, "test.xml", NewRoomDimensions);

                //set view state appropriate to the current new project work flow state
            }
            else
            {
                //TODO launch room dimension error dialog
                
            }
        }
        
        private bool RoomDimensionFieldValidator()
        {
            if (string.IsNullOrEmpty(XStorageDimension) || XStorageDimension is "0")
                return false;
            if (string.IsNullOrEmpty(YStorageDimension) || YStorageDimension is "0")
                return false;
            if (string.IsNullOrEmpty(ZStorageDimension) || ZStorageDimension is "0")
                return false;
            if (string.IsNullOrEmpty(WeightStorageMax) || WeightStorageMax is "0")
                return false;
            return true;
        }

        private void InitStorageFields()
        {
            XStorageDimension = "0";
            YStorageDimension = "0";
            ZStorageDimension = "0";
            WeightStorageMax = "0";
        }
    }
}
