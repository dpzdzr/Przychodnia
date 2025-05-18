using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;

public partial class AppointmentEditFormData : AppointmentBaseFormData
{
    [ObservableProperty] private int id;
}
