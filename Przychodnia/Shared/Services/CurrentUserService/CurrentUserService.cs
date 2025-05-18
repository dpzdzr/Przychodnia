using Przychodnia.Features.Entities.UserFeature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Shared.Services.CurrentUserService;

public class CurrentUserService : ICurrentUserService
{
    public UserDTO? CurrentUser { get; private set; }
    public void Clear()
    {
        CurrentUser = null;
    }

    public UserDTO? GetUser()
    {
        return CurrentUser;
    }

    public void SetUser(UserDTO dto)
    {
        CurrentUser = dto;
    }
}
