using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.UserFeature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.UserFeature.Messages;

public class UserAddedMessage(User value) 
    : ValueChangedMessage<User>(value)
{
}
