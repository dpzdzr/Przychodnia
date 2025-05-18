using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Messages;

public class PostalCodeAltered(PostalCodeWrapper value)
        : ValueChangedMessage<PostalCodeWrapper>(value)
{ }
