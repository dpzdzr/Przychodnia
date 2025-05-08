using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public class PostalCodeListViewModel : BaseViewModel
{
    private readonly IPostalCodeService _postalCodeService;
    private readonly IDialogService _dialogService;

    private string _postalCode;
    private string _city;
    private PostalCodeWrapper? _selectedPostalCode;
    private ObservableCollection<PostalCodeWrapper> _postalCodes;
    public ObservableCollection<PostalCodeWrapper> PostalCodes
    {
        get => _postalCodes;
        set => SetProperty(ref _postalCodes, value);
    }
    public PostalCodeWrapper? SelectedPostalCode
    {
        get => _selectedPostalCode;
        set
        {
            if (SetProperty(ref _selectedPostalCode, value))
            {
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(ActionButtonText));
                OnPropertyChanged(nameof(FormHeader));
                (DeleteCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();

                if (value != null)
                {
                    PostalCode = value.Code;
                    City = value.City;
                }
                else
                {
                    ClearForm();
                }
            }
        }
    }
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

    public IAsyncRelayCommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public IAsyncRelayCommand DeleteCommand { get; }

    public bool IsEditMode
        => SelectedPostalCode != null;
    public string ActionButtonText
        => IsEditMode ? "Edytuj" : "Zapisz";
    public string FormHeader
        => IsEditMode ? "Edytuj wybrany kod pocztowy" : "Dodaj nowy kod pocztowy";

    public PostalCodeListViewModel(IPostalCodeService postalCodeService, IDialogService dialogService)
    {
        _postalCodeService = postalCodeService;
        _dialogService = dialogService;

        SaveCommand = new AsyncRelayCommand(SubmitPostalCodeAsync);
        CancelCommand = new RelayCommand(ClearForm);
        DeleteCommand = new AsyncRelayCommand(DeletePostalCode, () => IsEditMode);
    }

    private PostalCodeInputDTO CreatePostalCodeDTO()
     => new() { City = _city, PostalCode = _postalCode };

    public async Task InitializeAsync()
    {
        var items = await _postalCodeService.GetAllAsync();
        PostalCodes = [.. items.Select(p => new PostalCodeWrapper(p))];
    }

    public async Task SubmitPostalCodeAsync()
    {
        if (IsEditMode)
        {
            SelectedPostalCode.Code = PostalCode;
            SelectedPostalCode.City = City;
            await _postalCodeService.UpdateAsync(SelectedPostalCode.ToEntity());
            _dialogService.Show("Sukces", "Pomyślnie zaktualizowano kod pocztowy");
        }
        else
        {
            var entity = await _postalCodeService.CreateAsync(CreatePostalCodeDTO());
            PostalCodes.Add(new PostalCodeWrapper(entity));
            _dialogService.Show("Sukces", "Pomyślnie dodano kod pocztowy");
        }

        //await InitializeAsync();
        ClearForm();
    }

    public async Task DeletePostalCode()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybrany kod pocztowy?"))
        {
            await _postalCodeService.RemoveAsync(SelectedPostalCode.ToEntity());
            PostalCodes.Remove(SelectedPostalCode);
        }
    }

    private void ClearForm()
    {
        City = string.Empty;
        PostalCode = string.Empty;
        SelectedPostalCode = null;
    }
}
