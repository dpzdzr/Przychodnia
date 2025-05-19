using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using Przychodnia.Shared.Messages;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Messages;

public class PostalCodeChanged(EntityChangedPayload value) : BaseEntityChangedMessage(value) { }
