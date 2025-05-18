using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Service.Implementation.Entity;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Base;

public abstract partial class ExaminationFormBaseViewModel<TForm> : BaseViewModel
    where TForm : ExaminationBaseFormData, new()
{
    protected readonly IMapper _mapper;
    protected readonly IMessenger _messenger;
    protected readonly IPatientService _patientService;
    protected readonly ILaboratoryService _laboratoryService;

    [ObservableProperty] private ObservableCollection<PatientWrapper> patients = [];
    [ObservableProperty] private ObservableCollection<LaboratoryWrapper> laboratories = [];

    public ExaminationFormBaseViewModel(IDialogService dialogService,
        IMapper mapper, IMessenger messenger, IPatientService patientService, ILaboratoryService laboratoryService)
        : base(dialogService)
    {
        _mapper = mapper;
        _messenger = messenger;
        _patientService = patientService;   
        _laboratoryService = laboratoryService;
        SubmitCommand = new AsyncRelayCommand(Submit);
    }
    public TForm FormData { get; } = new();
    public IAsyncRelayCommand SubmitCommand { get; }
    protected abstract Task Submit();

    public async Task InitializeFormDataAsync()
    {
        var patients = await _patientService.GetAllAsync();
        Patients = [.. patients.Select(p => new PatientWrapper(p))];
        var laboratories = await _laboratoryService.GetAllAsync();
        Laboratories = [.. laboratories.Select(l => new LaboratoryWrapper(l))];
    }
}
