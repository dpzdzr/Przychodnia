using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

public class DialogService : IDialogService
{
    public bool Confirm(string title, string message)
    {
        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return result == MessageBoxResult.Yes;
    }

    public void Show(string title, string message)
    {
        MessageBox.Show(message, title);
    }
}
