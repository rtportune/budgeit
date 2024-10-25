using BudgeIt.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Reflection;

namespace BudgeIt.UI.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private DBManager _dbManager;
        private RelayCommand _cmdOpenDatabase;

        public ICommand CmdOpenDatabase => _cmdOpenDatabase ?? (_cmdOpenDatabase = new RelayCommand(
            f =>
            {
                try
                {
                    var dialog = new OpenFileDialog()
                    {
                        DefaultDirectory = Assembly.GetExecutingAssembly().Location
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        _dbManager.OpenDatabase(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open database" + ex.Message);           
                }
            },
            o => true));
    }
}
