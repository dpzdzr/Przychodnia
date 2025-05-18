using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.Wrappers;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Wrappers;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Shared.Services;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public abstract partial class AppointmentFormBaseViewModel<TForm> : BaseViewModel
    where TForm : AppointmentBaseFormData, new()
{
    private readonly IUserService _userService;
    protected readonly IAppointmentService _appointmentService;
    protected readonly IPatientService _patientService;
    protected readonly IMapper _mapper;

    public TForm FormData { get; set; } = new();
    [ObservableProperty] private ObservableCollection<UserWrapper> doctors = [];
    [ObservableProperty] private ObservableCollection<PatientWrapper> patients = [];
    [ObservableProperty] private ObservableCollection<TimeSpan> availableHours = [];
    

    public AppointmentFormBaseViewModel(IDialogService dialogService, IUserService userService, 
        IPatientService patientService, IMapper mapper, IAppointmentService appointmentService) 
        : base(dialogService)
    {
        _userService = userService;
        _patientService = patientService;
        _mapper = mapper;
        _appointmentService = appointmentService;

        SubmitCommand = new AsyncRelayCommand(Submit);

        FormData.PropertyChanged += OnFormDataPropertyChanged;
    }

    public IAsyncRelayCommand SubmitCommand { get; }

    public async Task InitializeFormDataAsync()
    {
        var doctors = (await _userService.GetAllWithDetailsAsync())
            .Where(u => u.UserTypeId == (int)UserTypeEnum.Lekarz);
        Doctors = [.. doctors.Select(u => new UserWrapper(u)).ToList()];
        FormData.SelectedDoctor = Doctors.First();

        var patients = await _patientService.GetAllWithDetailsAsync();
        Patients = [.. patients.Select(u => new PatientWrapper(u)).ToList()];

        await UpdateAvailableHours();
    }

    protected abstract Task Submit();

    protected async Task UpdateAvailableHours()
    {
        if (FormData.SelectedDoctor?.Id is not int doctorId || FormData.SelectedDate is null)
        {
            AvailableHours.Clear();
            return;
        }

        var appointments = 
            await _appointmentService.GetAppointmentsForDoctorOnDateAsync(doctorId, FormData.SelectedDate.Value.Date);

        var booked = appointments.Select(a => a.Date.Value.TimeOfDay).ToHashSet();

        var allSlots = Enumerable.Range(0, (17 - 8) * 2)
            .Select(i => TimeSpan.FromHours(8) + TimeSpan.FromMinutes(i * 30));

        var available = allSlots.Where(h => !booked.Contains(h));

        AvailableHours = [.. available];

        FormData.SelectedHour = AvailableHours.FirstOrDefault();
    }
    private void OnFormDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(FormData.SelectedDoctor) || e.PropertyName == nameof(FormData.SelectedDate))
            _ = UpdateAvailableHours();
    }
    protected void SortAvailableHours()
    {
        var sorted = AvailableHours.OrderBy(ts => ts).ToList();
        AvailableHours = [.. sorted];
    }
}
