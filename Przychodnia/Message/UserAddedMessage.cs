using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Model;

namespace Przychodnia.Message;

public class UserAddedMessage(User value)
    : ValueChangedMessage<User>(value)
{
}
