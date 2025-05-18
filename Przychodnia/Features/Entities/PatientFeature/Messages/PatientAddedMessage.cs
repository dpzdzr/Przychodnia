using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;

namespace Przychodnia.Features.Entities.PatientFeature.Messages;

public class PatientAddedMessage(Patient value) : ValueChangedMessage<Patient>(value)
{
}
