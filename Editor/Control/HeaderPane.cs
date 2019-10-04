
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Editor.Control
{
    public class HeaderPane : INotifyPropertyChanged
    {
        private object header;
        private ICommand _CloseCommand;
        public Action Click;
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new RelayCommand(
                        param => Click?.Invoke(),
                        param => this.CanClose()
                    );
                }
                return _CloseCommand;
            }
        }
        private Visibility change=Visibility.Hidden;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Visibility Change { get => change; set { change = value; OnPropertyChanged("Change"); } }
        private bool CanClose()
        {
            // Verify command can be executed here
            return true;
        }

        private void CloseObject()
        {
            // Close command execution logic
        }
        public object Header { get => header; set { header = value; OnPropertyChanged("Header"); } }
        
    }
}
