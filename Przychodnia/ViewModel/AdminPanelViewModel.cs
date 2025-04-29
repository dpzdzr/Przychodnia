using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Repository.Interface;

namespace Przychodnia.ViewModel;

internal class AdminPanelViewModel(IUserRepository userRepository) : ViewModelBase
{
    private readonly IUserRepository _userRepository;
}
