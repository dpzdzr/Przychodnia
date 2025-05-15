using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Wrapper;
public partial class ExaminationWrapper : BaseWrapper
{
    [ObservableProperty] private ExaminationType? examinationType;
    [ObservableProperty] private PatientWrapper? patient;
    [ObservableProperty] private UserWrapper? orderedBy;
    [ObservableProperty] private UserWrapper? performingDoctor;
    [ObservableProperty] private LaboratoryWrapper? performingLaboratory;
    public ExaminationWrapper(Examination exam)
    {
        ExaminationType = exam.ExaminationType;
        Patient = WrapIfNotNull(exam.Patient, p => new PatientWrapper(p));
        OrderedBy = WrapIfNotNull(exam.OrderedBy, p => new UserWrapper(p));
        PerformingDoctor = WrapIfNotNull(exam.PerformingDoctor, p => new UserWrapper(p));
        PerformingLaboratory = WrapIfNotNull(exam.PerformingLaboratory, p => new LaboratoryWrapper(p));
    }
}

