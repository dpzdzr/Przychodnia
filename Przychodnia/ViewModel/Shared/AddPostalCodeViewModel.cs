using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Shared;

public class AddPostalCodeViewModel : ViewModelBase
{
    private readonly IPostalCodeService _postalCodeService;
    private readonly IDialogService _dialogService;

    private string _postalCode;
    private string _city;

    public string PostalCode
    {
        get => _postalCode;
        set => SetProperty(ref _postalCode, value);
    }
    public string City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }

    public IAsyncRelayCommand SaveButton { get; }

    public AddPostalCodeViewModel(IPostalCodeService postalCodeService, IDialogService dialogService)
    {
        _postalCodeService = postalCodeService;
        _dialogService = dialogService;

        SaveButton = new AsyncRelayCommand(SavePostalCode);
        _dialogService = dialogService;
    }

    private async Task SavePostalCode()
    {
        await _postalCodeService.CreateAsync(CreatePostalCodeDTO());
        _dialogService.Show("Sukces", "Pomyślnie dodano nowy kod pocztowy.");
    }

    private PostalCodeInputDTO CreatePostalCodeDTO()
     => new() { City = _city, PostalCode = _postalCode };

}
