using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Service.Interface;

namespace Przychodnia.ViewModel.Model;

class UserInputModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public Laboratory? laboratory { get; set; }

    public bool? isActive { get; set; }
    
}