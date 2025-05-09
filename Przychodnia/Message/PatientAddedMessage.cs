using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Message;

public class PatientAddedMessage : ValueChangedMessage<Patient>
{
    public PatientAddedMessage(Patient value) : base(value) { }
}
