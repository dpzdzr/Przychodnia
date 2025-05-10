using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Base;

public abstract partial class PatientFormBaseViewModel<TForm> : BaseViewModel
    where TForm : PatientFormDataBase, new()
{
    protected readonly IMapper _mapper;
    protected readonly IDialogService _dialogService;
    private readonly IPostalCodeService _postalCodeService;

    private List<PostalCodeWrapper> _allPostalCodes = [];
    [ObservableProperty] private string enteredCode;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> cities;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> postalCodes;

    public PatientFormBaseViewModel(IPostalCodeService postalCodeService, IDialogService dialogService, IMapper mapper)
    {
        _mapper = mapper;
        _dialogService = dialogService;
        _postalCodeService = postalCodeService;

        WeakReferenceMessenger.Default.Register<PostalCodeAddedOrEditedMessage>(this, (r, m) =>
        {
            PostalCodeWrapper wrapper = m.Value;

            var existing = _allPostalCodes.FirstOrDefault(pc => pc.Id == wrapper.Id);

            if (existing is not null)
            {
                existing.Code = wrapper.Code;
                existing.City = wrapper.City;
            }
            else
            {
                _allPostalCodes.Add(wrapper);
            }
            FilterCodes();
        });
    }

    public TForm FormData { get; set; } = new();
    public Dictionary<Sex, string> SexDispDict { get; } = new()
    {
        {Model.Sex.Male, "Mężczyzna" },
        {Model.Sex.Female, "Kobieta" }
    };

    private void FilterCodes()
    {
        var filteredCities = _allPostalCodes
            .Where(pc => string.IsNullOrEmpty(EnteredCode) || pc.Code.StartsWith(EnteredCode))
            .OrderBy(k => k.Code).ToList();
        Cities = [.. filteredCities];

        var distinctCodes = filteredCities.GroupBy(x => x.Code).Select(x => x.First());
        PostalCodes = [.. distinctCodes];
    }
    protected async Task InitializeFormDataAsync()
    {
        _allPostalCodes = [.. (await _postalCodeService.GetAllAsync()).Select(pc => new PostalCodeWrapper(pc))];
        FilterCodes();
    }
    partial void OnEnteredCodeChanged(string value) => FilterCodes();
}
