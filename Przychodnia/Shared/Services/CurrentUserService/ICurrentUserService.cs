using Przychodnia.Features.Entities.UserFeature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Shared.Services.CurrentUserService;

public interface ICurrentUserService
{
    UserDTO? GetUser();
    void SetUser(UserDTO dto);
    void Clear();
}
