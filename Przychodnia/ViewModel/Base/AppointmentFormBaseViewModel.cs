using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Base;

public abstract partial class AppointmentFormBaseViewModel<TForm> : BaseViewModel
    where TForm : AppointmentBaseFormData, new()
{
    private readonly IUserService _userService;
    protected readonly IAppointmentService _appointmentService;
    protected readonly IPatientService _patientService;
    protected readonly IMapper _mapper;

    [ObservableProperty] private ObservableCollection<UserWrapper> doctors = [];
    [ObservableProperty] private ObservableCollection<PatientWrapper> patients = [];
    [ObservableProperty] private ObservableCollection<TimeSpan> availableHours = [];
    [ObservableProperty] private TimeSpan? selectedHour;
    [ObservableProperty] private DateTime? selectedDate;
    
    [ObservableProperty] private UserWrapper? selectedDoctor;
    [ObservableProperty] private string enteredPatientPesel = string.Empty;
    protected DateTime? FullDate => SelectedDate.Value.Date + SelectedHour;
    public AppointmentFormBaseViewModel(IDialogService dialogService, IUserService userService, 
        IPatientService patientService, IMapper mapper, IAppointmentService appointmentService) 
        : base(dialogService)
    {
        _userService = userService;
        _patientService = patientService;
        _mapper = mapper;
        _appointmentService = appointmentService;

        SubmitCommand = new AsyncRelayCommand(Submit);
    }

    public IAsyncRelayCommand SubmitCommand { get; }

    public TForm FormData { get; } = new();

    public async Task InitializeFormDataAsync()
    {
        var doctors = (await _userService.GetAllWithDetailsAsync())
            .Where(u => u.UserTypeId == (int)UserTypeEnum.Lekarz);
        Doctors = [.. doctors.Select(u => new UserWrapper(u)).ToList()];
        SelectedDoctor = Doctors.First();

        var patients = await _patientService.GetAllWithDetailsAsync();
        Patients = [.. patients.Select(u => new PatientWrapper(u)).ToList()];

        UpdateAvailableHours();
    }
    protected abstract Task Submit();

    
    private async void UpdateAvailableHours()
    {
        if (SelectedDoctor?.Id is not int doctorId || SelectedDate is null)
        {
            AvailableHours.Clear();
            return;
        }

        var appointments = await _appointmentService.GetAppointmentsForDoctorOnDateAsync(doctorId, SelectedDate.Value.Date);

        var booked = appointments.Select(a => a.Date.Value.TimeOfDay).ToHashSet();

        var allSlots = Enumerable.Range(0, (int)(17 - 8) * 2)
            .Select(i => TimeSpan.FromHours(8) + TimeSpan.FromMinutes(i * 30));

        var available = allSlots.Where(h => !booked.Contains(h));

        AvailableHours = [.. available];

        SelectedHour = AvailableHours.FirstOrDefault();
    }

    partial void OnSelectedDoctorChanged(UserWrapper? value) => UpdateAvailableHours();
    partial void OnSelectedDateChanged(DateTime? value) => UpdateAvailableHours();

}
