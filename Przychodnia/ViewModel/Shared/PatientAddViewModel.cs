using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public class PatientAddViewModel : PatientFormBaseViewModel<PatientAddFormData>
{
    private readonly IPatientService _patientService;

    public PatientAddViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
        IPatientService patientService, IMapper mapper, IMessenger messenger)
        : base(postalCodeService, dialogService, mapper, messenger)
    {
        _patientService = patientService;
        ActionButtonCommand = new AsyncRelayCommand(AddPatient);
    }

    public static string HeaderText => "Dodaj pacjenta";
    public static string ActionButtonText => "Dodaj";

    public ICommand ActionButtonCommand { get; }

    public async Task InitializeAsync() => await base.InitializeFormDataAsync();

    private async Task AddPatient()
    {
        try
        {
            var dto = _mapper.Map<PatientDTO>(FormData);
            var entity = await _patientService.CreateAsync(dto);
            NotifyPatientAdded(entity);
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego pacjenta");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.Message}\n{ex.InnerException?.Message ?? string.Empty}");
        }
        ClearForm();
    }

    private void NotifyPatientAdded(Patient entity)
        => _messenger.Send(new PatientAddedMessage(entity));

    private void ClearForm()
    {
        EnteredCode = string.Empty;
        FormData.ClearForm();
    }
}
