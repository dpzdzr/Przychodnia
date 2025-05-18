using Przychodnia.Features.Entities.UserFeature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Login.Services;

public interface ILoginService
{
    Task<bool> Authenticate(string login, SecureString inputPassword);
}
