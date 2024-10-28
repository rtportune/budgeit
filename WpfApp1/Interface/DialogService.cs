using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgeIt.Interface.UI
{
    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
        void Show();
        event EventHandler Closed;
    }

    /// <summary>
    /// Interface for the Dialog Service itself
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Register the given mapping of a particular ViewModel to its associated View type.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel type</typeparam>
        /// <typeparam name="TView">View type</typeparam>
        void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                           where TView : IDialog;

        /// <summary>
        /// Shows the appropriate Dialog View based on the referenced ViewModel type. If no
        /// ViewModel type has been Registered, will return false.
        /// </summary>
        /// <typeparam name="TViewModel">Given ViewModel type</typeparam>
        /// <param name="viewModel">Concrete ViewModel instance</param>
        /// <param name="modal">Whether the shown dialog should be modal</param>
        /// <param name="unique">Whether the shown dialog is unique. Unique dialogs cannot have multiple instances.</param>
        /// <returns>The result of the showing the dialog. Modeless dialogs will always return null immediately.</returns>
        bool? ShowDialog<TViewModel>(TViewModel viewModel, bool modal, bool unique) where TViewModel : IDialogRequestClose;
    }

    /// <summary>
    /// Interface that must be implemneted by a Dialog's ViewModel component
    /// </summary>
    public interface IDialogRequestClose
    {
        /// <summary>
        /// Performs any necessary operations that need to occur prior to the Dialog being shown.
        /// </summary>
        void ConfigureForDisplay();
        /// <summary>
        /// Event which is fired if the Dialog's ViewModel needs its associated View to close.
        /// </summary>
        event EventHandler<DialogCloseEventArgs> CloseRequested;
    }

    /// <summary>
    /// Event Arguments associated with a Dialog closing.
    /// </summary>
    public class DialogCloseEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the result of the shown dialog.
        /// </summary>
        public bool? DialogResult { get; }

        /// <summary>
        /// Creates a new DialogCloseEventArgs
        /// </summary>
        /// <param name="dialogResult">Result of the dialog closing</param>
        public DialogCloseEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }

}
