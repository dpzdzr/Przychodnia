using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Base;

public class PatientFormBaseViewModel<TForm>(IPostalCodeService postalCodeService, IDialogService dialogService, IMapper mapper) : BaseViewModel
    where TForm : PatientFormDataBase, new()
{
    private readonly IPostalCodeService _postalCodeService = postalCodeService;
    protected readonly IDialogService _dialogService = dialogService;
    protected readonly IMapper _mapper = mapper;
    public TForm FormData { get; set; } = new();

    private ObservableCollection<PostalCodeWrapper> _postalCodes;
    private ObservableCollection<PostalCodeWrapper> _cities;

    private string _enteredCode;

    public Dictionary<Sex, string> SexDispDict { get; } = new()
    {
        {Model.Sex.Male, "Mężczyzna" },
        {Model.Sex.Female, "Kobieta" }
    };

    public string EnteredCode
    {
        get => _enteredCode;
        set
        {
            if (_enteredCode != value)
            {
                _enteredCode = value;
                OnPropertyChanged(nameof(EnteredCode));
                _ = FilterCodes();
            }
        }
    }
    public ObservableCollection<PostalCodeWrapper> PostalCodes
    {
        get => _postalCodes;
        set => SetProperty(ref _postalCodes, value);
    }

    public ObservableCollection<PostalCodeWrapper> Cities
    {
        get => _cities;
        set => SetProperty(ref _cities, value);
    }

    public async Task FilterCodes()
    {
        var distinctCodes = await _postalCodeService.GetDistinctCodes(EnteredCode);
        PostalCodes = [.. distinctCodes.Select(pc => new PostalCodeWrapper(pc))];

        var citiesMatchingByCode = await _postalCodeService.GetAllMatchingByCode(EnteredCode);
        Cities = [.. citiesMatchingByCode.Select(pc => new PostalCodeWrapper(pc))];
    }

    protected async Task InitializeFormDataAsync()
    {
        var distinctCodes = await _postalCodeService.GetDistinctCodes(EnteredCode);
        PostalCodes = [.. distinctCodes.Select(pc => new PostalCodeWrapper(pc))];

        var allCities = await _postalCodeService.GetAllAsync();
        Cities = [.. allCities.Select(pc => new PostalCodeWrapper(pc))];
    }

    public async override Task OnNavigatedBack()
    {
        await InitializeFormDataAsync();
        await FilterCodes();
    }
}
