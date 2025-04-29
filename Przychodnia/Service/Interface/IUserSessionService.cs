using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;

namespace Przychodnia.Service.Interface;

interface IUserSessionService
{
    User CurrentUser { get; }
    bool IsLoggedIn { get; }
    void Login(User user);
    void Logout ();
}
