using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Shared.Messages;

public class BaseEntityChangedMessage(EntityChangedPayload value) : ValueChangedMessage<EntityChangedPayload>(value) { }

public record EntityChangedPayload(int Id, EntityChangedAction Action);

public enum EntityChangedAction { Added, Edited }
