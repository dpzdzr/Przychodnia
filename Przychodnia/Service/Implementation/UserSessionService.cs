using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

internal class UserSessionService : IUserSessionService
{
    public User? _currentUser;
    public User CurrentUser => _currentUser;

    public bool IsLoggedIn => _currentUser != null;

    public void Login(User user)
    {
        _currentUser = user;
    }

    public void Logout()
    {
        _currentUser = null;
    }
}
