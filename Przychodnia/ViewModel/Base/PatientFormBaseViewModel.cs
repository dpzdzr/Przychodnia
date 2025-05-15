using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Base;

public abstract partial class PatientFormBaseViewModel<TForm> : BaseViewModel
    where TForm : PatientBaseFormData, new()
{
    protected readonly IPatientService _patientService;
    protected readonly IMapper _mapper;
    protected readonly IMessenger _messenger;
    private readonly IPostalCodeService _postalCodeService;

    private List<PostalCodeWrapper> _allPostalCodes = [];
    [ObservableProperty]
    private PostalCodeWrapper dummyPostalCode = new(null, createDummy: true) { City = "brak", Code = "brak" };
    [ObservableProperty] private string enteredCode = string.Empty;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> cities = [];
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> postalCodes = [];

    public PatientFormBaseViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
        IMapper mapper, IMessenger messenger, IPatientService patientService)
        : base(dialogService)
    {
        _mapper = mapper;
        _postalCodeService = postalCodeService;
        _messenger = messenger;
        _patientService = patientService;

        SubmitCommand = new AsyncRelayCommand(Submit);

        _messenger.Register<PostalCodeAltered>(this, HandlePostalCodeMessage);
        _patientService = patientService;
    }

    public IAsyncRelayCommand SubmitCommand { get; }

    public TForm FormData { get; set; } = new();
    public Dictionary<Sex, string> SexDispDict { get; } = new()
    {
        {Model.Sex.Male, "Mężczyzna" },
        {Model.Sex.Female, "Kobieta" }
    };

    public void HandlePostalCodeMessage(object recipient, PostalCodeAltered message)
    {
        var wrapper = message.Value;
        var existing = _allPostalCodes.FirstOrDefault(pc => pc.Id == wrapper.Id);

        if (existing is not null)
            _mapper.Map(wrapper, existing);
        else
            _allPostalCodes.Add(wrapper);

        FilterCodes();
    }

    protected async Task InitializeFormDataAsync()
    {
        _allPostalCodes = [.. (await _postalCodeService.GetAllAsync()).Select(pc => new PostalCodeWrapper(pc))];
        FilterCodes();
    }
    protected abstract Task Submit();

    private void FilterCodes()
    {
        var filteredCities = _allPostalCodes
            .Where(pc => string.IsNullOrEmpty(EnteredCode) || pc.Code.StartsWith(EnteredCode))
            .OrderBy(k => k.Code).ToList();
        filteredCities.Insert(0, DummyPostalCode);
        Cities = [.. filteredCities];

        var distinctCodes = filteredCities.GroupBy(x => x.Code).Select(x => x.First()).ToList();
        distinctCodes.RemoveAll(pc => pc.Code == "brak");
        distinctCodes.Insert(0, DummyPostalCode);
        PostalCodes = [.. distinctCodes];
    }

    partial void OnEnteredCodeChanged(string value) => FilterCodes();
}
