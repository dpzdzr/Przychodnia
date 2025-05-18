using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Login.Services;

public interface IViewModelFactory
{
    INavigationService CreateFor(int UserTypeId);
}
