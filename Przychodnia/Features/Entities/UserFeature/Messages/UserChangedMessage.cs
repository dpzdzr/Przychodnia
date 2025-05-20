using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Shared.Messages;

namespace Przychodnia.Features.Entities.UserFeature.Messages;

public class UserChangedMessage(EntityChangedPayload value) 
    : BaseEntityChangedMessage(value)
{
}
