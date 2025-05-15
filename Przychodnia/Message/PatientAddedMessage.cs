using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Model;

namespace Przychodnia.Message;

public class PatientAddedMessage : ValueChangedMessage<Patient>
{
    public PatientAddedMessage(Patient value) : base(value) { }
}
