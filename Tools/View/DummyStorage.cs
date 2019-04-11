using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Tools.View
{

    public class DummyStorage : ViewModelBase
    {
        private ObservableCollection<StorageFile> storageFiles;
        private ICommand deleteCommand;
        private string folderName;

        public ObservableCollection<StorageFile> StorageFiles
        {
            get
            {
                if (this.storageFiles == null)
                {
                    this.storageFiles = new ObservableCollection<StorageFile>();
                    this.storageFiles.Add(new StorageFile("dummy file.txt"));
                    this.storageFiles.Add(new StorageFile("dummy file 2.txt"));
                }
                return this.storageFiles;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new DelegateCommand(this.DeleteCommandHandler);
                }
                return this.deleteCommand;
            }
        }

        public string FolderName
        {
            get
            {
                if (string.IsNullOrEmpty(this.folderName))
                {
                    this.folderName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                return this.folderName;
            }
            set
            {
                if (this.folderName != value)
                {
                    this.folderName = value;
                    this.OnPropertyChanged("FolderName");
                }
            }
        }

        private void DeleteCommandHandler(object param)
        {
            var deletedFile = param as StorageFile;
            if (deletedFile != null)
            {
                this.StorageFiles.Remove(deletedFile);
            }
        }
    }
}