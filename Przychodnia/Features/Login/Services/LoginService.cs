using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Shared.Services.CurrentUserService;
using Przychodnia.Shared.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Login.Services;

public class LoginService(IUserLookupService userLookupService, IServiceProvider serviceProvider, IMapper mapper) 
    : ILoginService
{
    private readonly IMapper _mapper = mapper;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IUserLookupService _userLookupService = userLookupService;

    public async Task<bool> Authenticate(string login, SecureString inputPassword)
    {
        var user = await _userLookupService.GetByLogin(login);

        if (user is not null && VerifyPassword(user.PasswordHash, inputPassword))
        {
            var dto = _mapper.Map<UserDTO>(user);
            _serviceProvider.GetRequiredService<ICurrentUserService>().SetUser(dto);
            return true;
        }
        else 
            return false;
    }

    private static bool VerifyPassword(string passwordHash, SecureString inputPassword)
    {
        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = Marshal.SecureStringToBSTR(inputPassword);
            string plainPassword = Marshal.PtrToStringBSTR(ptr);

            bool isValid = plainPassword == passwordHash;
            return isValid;
        }
        finally
        {
            if(ptr != IntPtr.Zero)
                Marshal.ZeroFreeBSTR(ptr);
        }
    }
}
