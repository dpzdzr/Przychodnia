using CommunityToolkit.Mvvm.Messaging.Messages;
using Przychodnia.Model;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Message;

public class PostalCodeAddedOrEditedMessage(PostalCodeWrapper value)
        : ValueChangedMessage<PostalCodeWrapper>(value)
{ }
