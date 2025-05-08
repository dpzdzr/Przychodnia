using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Shared;

public class PatientAddViewModel : PatientFormBaseViewModel<PatientAddFormData>
{
    private readonly IPatientService _patientService;
    public static string HeaderText => "Dodaj pacjenta";
    public static string ActionButtonText => "Dodaj";

    public ICommand ActionButtonCommand { get; }

    public PatientAddViewModel(IPostalCodeService postalCodeService, IDialogService dialogService, IPatientService patientService) : base(postalCodeService, dialogService)
    {
        _patientService = patientService;
        ActionButtonCommand = new AsyncRelayCommand(AddPatient);
    }

    private async Task AddPatient()
    {
        try
        {
            await _patientService.AddAsync(CreatePatientInputDTO());
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego pacjenta");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.InnerException.Message}");
        }
        ClearForm();
    }

    private PatientInputDTO CreatePatientInputDTO()
    {
        return new PatientInputDTO
        {
            FirstName = FormData.FirstName,
            LastName = FormData.LastName,
            Pesel = FormData.Pesel,
            PostalCode = FormData.PostalCode,
            Street = FormData.Street,
            HouseNumber = FormData.HouseNumber,
            ApartmentNumber = FormData.ApartmentNumber,
            Sex = FormData.Sex
        };
    }

    private void ClearForm()
    {
        EnteredCode = string.Empty;
        FormData.ClearForm();
    }

}
