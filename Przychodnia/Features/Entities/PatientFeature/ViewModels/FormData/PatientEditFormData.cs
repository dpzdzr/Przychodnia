using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;

public partial class PatientEditFormData : PatientBaseFormData
{
    [ObservableProperty] private int id;
}
