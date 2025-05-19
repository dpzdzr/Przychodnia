using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Shared.Messages;

namespace Przychodnia.Features.Entities.PatientFeature.Messages;

public class PatientChangedMessage(EntityChangedPayload value) : BaseEntityChangedMessage(value) { }
