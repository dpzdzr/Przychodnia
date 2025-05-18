using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Implementation.Entity;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Shared;

public class ExaminationAddViewModel(IDialogService dialogService, IExaminationService examinationService, IMapper mapper, IMessenger messenger, IPatientService patientService, ILaboratoryService laboratoryService)
    : ExaminationFormBaseViewModel<ExaminationAddFormData>(dialogService, mapper, messenger, patientService, laboratoryService)
{
    private readonly IExaminationService _examinationService = examinationService;
    public static string HeaderText => "Dodaj badanie";
    public static string SubmitButtonText => "Dodaj";

    protected override async Task Submit()
    {
        await TryExecuteAsync(async () =>
        {
            var dto = _mapper.Map<ExaminationDTO>(FormData);
            var entity = await _examinationService.CreateAsync(dto);
            //NotifyExaminationAdded(entity);
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego pacjenta");
            ClearForm();
        });
    }
    public async Task InitializeAsync() => await base.InitializeFormDataAsync();

    //private void NotifyExaminationAdded(Examination entity)
    //    => _messenger.Send(new ExaminationAddedMessage(entity));
    private void ClearForm()
    {
        FormData.ClearForm();
    }
}
