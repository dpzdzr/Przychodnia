using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Shared.Messages;

public class BaseEntityChangedMessage(EntityChangedPayload value) : ValueChangedMessage<EntityChangedPayload>(value) { }

public record EntityChangedPayload(IEntity Entity, EntityChangedAction Action);

public enum EntityChangedAction { Added, Edited }
