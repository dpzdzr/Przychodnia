using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;

public partial class UserEditFormData : UserBaseFormData
{
    [ObservableProperty] private int id;
}
