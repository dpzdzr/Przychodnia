using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.PatientFeature.Messages;

public class PatientAddedMessage(Patient value) : ValueChangedMessage<Patient>(value)
{
}
