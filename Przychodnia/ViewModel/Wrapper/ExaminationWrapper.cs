using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;
public partial class ExaminationWrapper : BaseWrapper
{
    [ObservableProperty] private ExaminationType? examinationType;
    [ObservableProperty] private PatientWrapper? patient;
    [ObservableProperty] private UserWrapper? orderedBy;
    [ObservableProperty] private UserWrapper? performingDoctor;
    [ObservableProperty] private LaboratoryWrapper? performingLaboratory;
    public ExaminationWrapper(Examination exam) {
        ExaminationType = exam.ExaminationType;
        Patient = WrapIfNotNull(exam.Patient, p => new PatientWrapper(p));

    }
}

