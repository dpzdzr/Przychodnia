using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PostalCodeFeature.Messages;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels;

public abstract partial class PatientFormBaseViewModel<TForm> : BaseViewModel
    where TForm : PatientBaseFormData, new()
{
    protected readonly IPatientService _patientService;
    protected readonly IMapper _mapper;
    private readonly IPostalCodeService _postalCodeService;
    private readonly PostalCodeWrapper DummyPostalCode = new(null, createDummy: true) { City = "brak", Code = "brak" };

    private List<PostalCodeWrapper> _allPostalCodes = [];
    [ObservableProperty] private string enteredCode = string.Empty;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> cities = [];
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> postalCodes = [];

    public PatientFormBaseViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
        IMapper mapper, IMessenger messenger, IPatientService patientService)
        : base(dialogService, messenger)
    {
        _mapper = mapper;
        _postalCodeService = postalCodeService;
        _patientService = patientService;

        SubmitCommand = new AsyncRelayCommand(Submit);

        _messenger.Register<PostalCodeChanged>(this, (r, m) => HandlePostalCodeMessage(m));
    }

    public IAsyncRelayCommand SubmitCommand { get; }

    public TForm FormData { get; set; } = new();
    public Dictionary<Sex, string> SexDispDict { get; } = new()
    {
        {Sex.Male, "Mężczyzna" },
        {Sex.Female, "Kobieta" }
    };

    protected async Task InitializeFormDataAsync()
    {
        _allPostalCodes = [.. (await _postalCodeService.GetAllAsync()).Select(pc => new PostalCodeWrapper(pc))];
        FilterCodes();
    }
    protected abstract Task Submit();
    protected void ValidateFormData()
    {
        if (!FormData.IsValid)
            throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");
    }

    private void HandlePostalCodeMessage(BaseEntityChangedMessage message)
    {
        switch (message.Value.Action)
        {
            case EntityChangedAction.Added:
                HandleAdded((PostalCode)message.Value.Entity);
                break;
            case EntityChangedAction.Edited:
                HandleEdited((PostalCode)message.Value.Entity);
                break;
        }
        FilterCodes();
    }
    private void HandleEdited(PostalCode entity)
    {
        var current = _allPostalCodes.Find(pc => pc.Id == entity.Id);
        _mapper.Map(entity, current);
    }
    private void HandleAdded(PostalCode entity)
    {
        _allPostalCodes.Add(new(entity));
    }
    private void FilterCodes()
    {
        var filteredCities = _allPostalCodes
            .Where(pc => string.IsNullOrEmpty(EnteredCode) || pc.Code!.StartsWith(EnteredCode))
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
