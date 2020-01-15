using SAUSALibrary.Models.Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SAUSALibrary.DataProcessing;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.IO;
using SAUSALibrary.Defaults;

namespace WpfApp1.ViewModels 
{
    public class NewStackViewModel : INotifyPropertyChanged
    {
        #region fields

        public event PropertyChangedEventHandler? PropertyChanged;

        private string[]? _ProjectDBFile;

        private string? _FullProjectDBFilePath;

        public string? NameField { get; set; } = "XXX";

        public RelayCommand AddFieldToList { get; }

        public RelayCommand WriteFieldsToDatabase { get; }

        public RelayCommand DeleteFieldFromList { get; }

        public List<IndividualDatabaseFieldModel>? DropDownFields { get; set; }

        public List<IndividualDatabaseFieldModel>? DefaultFields { get; set; }

        public ObservableCollection<IndividualDatabaseFieldModel>? NewDBFields { get; set; } = new ObservableCollection<IndividualDatabaseFieldModel>();

        private IndividualDatabaseFieldModel? _Model;

        public IndividualDatabaseFieldModel? Model
        {
            get => _Model;
            set => SetProperty(ref _Model, value);
        }

    #endregion

        public NewStackViewModel()
        {
            _ProjectDBFile = GetProjectDB();
            DefaultFields = GetDefaultFields();
            DropDownFields = GetFieldsFromEnum();
            Model = new IndividualDatabaseFieldModel() {FieldName = "BLANK", FieldType="BLANK" };
            AddFieldToList = new RelayCommand(OnAddField);
            WriteFieldsToDatabase = new RelayCommand(OnWriteFields);
            DeleteFieldFromList = new RelayCommand(OnDeleteField);
        }

        private void OnAddField()
        {
            if (!NameField.Contains("XXX") || !string.IsNullOrEmpty(NameField))
            {
                Model.FieldName = NameField;
                NewDBFields.Add(new IndividualDatabaseFieldModel() {FieldName = Model.FieldName, FieldType = Model.FieldType });
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewDBFields)));
            }
            else
            {
                //TODO throw error dialog that name field can't be empty
            }
        }

        private void OnWriteFields()
        {
            //TODO write all new database fields in the optional field list to the project database
        }

        /// <summary>
        /// Delete the selected field out of the custom field list.
        /// </summary>
        private void OnDeleteField()
        {
            NewDBFields.Remove(Model);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewDBFields)));
        }

        #region helper_methods

        private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value)) //if using custom classes need to implement equals
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }               

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string[] GetProjectDB()
        {
            var scratchFolder = FilePathDefaults.ScratchFolder;

            return Directory.GetFiles(scratchFolder, "*.sqlite");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<IndividualDatabaseFieldModel> GetFieldsFromEnum()
        {
            return GetEnums.GetFieldsList();
        }

        /// <summary>
        /// Since SQLite fields are not type bound, this list is manually created for the user's sake.
        /// </summary>
        /// <returns></returns>
        private List<IndividualDatabaseFieldModel> GetDefaultFields()
        {
            List<IndividualDatabaseFieldModel> list = new List<IndividualDatabaseFieldModel>();
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "FIELD NAME", FieldType = "TYPE" });
            list.Add(new IndividualDatabaseFieldModel() {FieldName = "ID", FieldType = "Integer"} );
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Xpos", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Ypos", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Zpos", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Length", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Width", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Height", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Weight", FieldType = "Float" });
            list.Add(new IndividualDatabaseFieldModel() { FieldName = "Name", FieldType = "VarChar" });            
            return list;
        }

        #endregion
    }
}
