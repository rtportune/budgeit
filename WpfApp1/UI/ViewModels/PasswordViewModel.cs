using BudgeIt.Interface.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgeIt.UI.ViewModels
{
    public class PasswordViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseEventArgs> CloseRequested;

        private RelayCommand _cmdOK;
        private RelayCommand _cmdCancel;
        private string _windowTitle;

        public void ConfigureForDisplay()
        {
        }

        public string Password { get; set; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }

        public ICommand CmdOK => _cmdOK ?? (_cmdOK = new RelayCommand(
            f =>
            {
                CloseRequested?.Invoke(this, new DialogCloseEventArgs(true));
            },
            o => true));

        public ICommand CmdCancel => _cmdCancel ?? (_cmdCancel = new RelayCommand(
           f =>
           {
               CloseRequested?.Invoke(this, new DialogCloseEventArgs(null));
           },
           o => true));
    }
}
