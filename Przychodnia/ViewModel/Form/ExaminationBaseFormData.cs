using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Form;

public abstract partial class ExaminationBaseFormData : ObservableObject
{
    [ObservableProperty] private ExaminationType? examinationType;
    [ObservableProperty] private PatientWrapper? patient;
    [ObservableProperty] private UserWrapper? orderedBy;
    [ObservableProperty] private UserWrapper? performingDoctor;
    [ObservableProperty] private LaboratoryWrapper? performingLaboratory;
}
