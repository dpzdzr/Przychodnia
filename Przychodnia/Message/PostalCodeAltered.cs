using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.Message;

public class PostalCodeAltered(PostalCodeWrapper value)
        : ValueChangedMessage<PostalCodeWrapper>(value)
{ }
