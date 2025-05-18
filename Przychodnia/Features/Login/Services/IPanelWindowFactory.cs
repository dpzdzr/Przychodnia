using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Przychodnia.Features.Login.Services;

public interface IPanelWindowFactory
{
    Window CreateFor(int userTypeId);
}
