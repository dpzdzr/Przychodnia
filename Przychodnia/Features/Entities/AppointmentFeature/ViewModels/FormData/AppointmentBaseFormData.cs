using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.PatientFeature.Wrappers;
using Przychodnia.Features.Entities.UserFeature.Wrappers;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;

public abstract partial class AppointmentBaseFormData : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Godzina jest wymagana")]
    private TimeSpan? selectedHour;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Data jest wymagana")]
    private DateTime? selectedDate;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Pesel jest wymagany")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL musi zawierać dokładnie 11 cyfr")]
    private string? enteredPatientPesel;

    [ObservableProperty]
    private UserWrapper? scheduledBy;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Doktor jest wymagany")]
    private UserWrapper? selectedDoctor;

    [ObservableProperty]
    private bool? completed = false;

    public PatientWrapper? SelectedPatient { get; set; }
    public DateTime? FullDate =>
       SelectedDate is not null && SelectedHour is not null ? SelectedDate.Value.Date + SelectedHour.Value : null;

    public bool IsValid
    {
        get
        {
            ValidateAllProperties();
            return !HasErrors;
        }
    }

    public void ClearAllErrors()
    {
        var errorPropertyNames = GetErrors()
            .SelectMany(e => e.MemberNames)
            .Distinct()
            .ToList();

        foreach (var propertyName in errorPropertyNames)
        {
            ClearErrors(propertyName);
        }
    }
}
