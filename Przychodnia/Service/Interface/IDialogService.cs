using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Service.Interface;

public interface IDialogService
{
    bool Confirm(string title, string message);
    void Show(string title, string message);

    void Error(string title, string message);
}
