using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.UserFeature.Models;

namespace Przychodnia.Features.Entities.UserFeature.Messages;

public class UserAddedMessage(User value)
    : ValueChangedMessage<User>(value)
{
}
