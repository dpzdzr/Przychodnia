using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.ViewModel.Model;

namespace Przychodnia.Service.Interface;

public interface IUserCreationService
{
    void CreateUser(UserInputModel model);
}
