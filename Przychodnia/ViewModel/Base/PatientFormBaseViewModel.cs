using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;

namespace Przychodnia.ViewModel.Base;

public class PatientFormBaseViewModel<TForm>(IPostalCodeService postalCodeService, IDialogService dialogService, IMapper mapper) : BaseViewModel
    where TForm : PatientFormDataBase, new()
{
    private readonly IPostalCodeService _postalCodeService = postalCodeService;
    protected readonly IDialogService _dialogService = dialogService;
    protected readonly IMapper _mapper = mapper;
    public TForm FormData { get; set; } = new();

    private ObservableCollection<PostalCode> _postalCodes;
    private ObservableCollection<PostalCode> _cities;

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
    public ObservableCollection<PostalCode> PostalCodes
    {
        get => _postalCodes;
        set => SetProperty(ref _postalCodes, value);
    }

    public ObservableCollection<PostalCode> Cities
    {
        get => _cities;
        set => SetProperty(ref _cities, value);
    }

    public async Task FilterCodes()
    {
        PostalCodes = [.. await _postalCodeService.GetDistinctCodes(EnteredCode)];
        Cities = [.. await _postalCodeService.GetAllMatchingByCode(EnteredCode)];
    }

    protected async Task InitializeFormDataAsync()
    {
        PostalCodes = [.. await _postalCodeService.GetDistinctCodes(EnteredCode)];
        Cities = [.. await _postalCodeService.GetAllAsync()];
    }

    public async override Task OnNavigatedBack()
    {
        await InitializeFormDataAsync();
        await FilterCodes();
    }
}
