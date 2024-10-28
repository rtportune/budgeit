using BudgeIt.Interface.UI;
using BudgeIt.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgeIt.UI
{
    public class DialogService : IDialogService
    {
        private Dictionary<Type, Type> _mappingDictionary;
        private List<IDialog> _activeDialogs;

        private DialogService()
        {
            _mappingDictionary = new Dictionary<Type, Type>();
            _activeDialogs = new List<IDialog>();
        }

        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            //If mapping exists, return
            if (_mappingDictionary.ContainsKey(typeof(TViewModel)))
                return;

            _mappingDictionary.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel, bool modal, bool unique) where TViewModel : IDialogRequestClose
        {
            if (!_mappingDictionary.ContainsKey(typeof(TViewModel)))
                throw new ArgumentException("Unable to display dialog, type not registered");

            var dialogType = _mappingDictionary[typeof(TViewModel)];

            //If the requested dialog is unique, check to see if it already has an active dialog.
            if (unique)
            {
                foreach (var dlg in _activeDialogs)
                    if (dlg.GetType() == dialogType)
                        return false;
            }

            try
            {
                //Create Dialog view itself
                var dialog = (IDialog)Activator.CreateInstance(dialogType);

                //Subscribe to close requested event
                EventHandler<DialogCloseEventArgs> handler = null;

                handler = (sender, e) =>
                {
                    viewModel.CloseRequested -= handler;

                    if (e.DialogResult.HasValue)
                    {
                        dialog.DialogResult = e.DialogResult;
                    }
                    else
                        dialog.Close();
                };

                viewModel.CloseRequested += handler;
                viewModel.ConfigureForDisplay();
                dialog.DataContext = viewModel;

                //If the dialog should be unique, subscribe to its Closed event
                if (unique)
                {
                    EventHandler closedHandler = null;

                    closedHandler = (sender, e) =>
                    {
                        dialog.Closed -= closedHandler;
                        _activeDialogs.Remove(dialog);
                    };

                    dialog.Closed += closedHandler;

                    _activeDialogs.Add(dialog);
                }

                if (modal)
                    return dialog.ShowDialog();
                else
                {
                    dialog.Show();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to show the requested Dialog", ex);
            }
        }

        public static DialogService Instance => Singleton<DialogService>.Instance;
    }
}
