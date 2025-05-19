using Przychodnia.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.AppointmentFeature.Messages;

public class AppointmentChangedMessage(EntityChangedPayload value) : BaseEntityChangedMessage(value) { }
