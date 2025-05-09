using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
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
    private readonly IMapper _mapper;

    private PostalCodeWrapper? _selectedPostalCode;
    private PostalCodeWrapper _editPostalCode;
    private ObservableCollection<PostalCodeWrapper> _postalCodes;
    private bool _isEditMode;

    public PostalCodeListViewModel(IPostalCodeService postalCodeService, IDialogService dialogService, IMapper mapper)
    {
        _postalCodeService = postalCodeService;
        _dialogService = dialogService;
        _mapper = mapper;

        SaveCommand = new AsyncRelayCommand(SubmitPostalCodeAsync);
        CancelCommand = new RelayCommand(ClearForm);
        DeleteCommand = new AsyncRelayCommand(DeletePostalCode, () => IsEditMode);

        EditPostalCode = new(new PostalCode());
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetProperty(ref _isEditMode, value);
    }
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
                IsEditMode = value != null && value.Id != 0;
                EditPostalCode = value?.Clone() ?? new PostalCodeWrapper(new PostalCode());
                OnPropertyChanged(nameof(ActionButtonText));
                OnPropertyChanged(nameof(FormHeader));
                (DeleteCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
            }
        }
    }
    public PostalCodeWrapper EditPostalCode
    {
        get => _editPostalCode;
        set => SetProperty(ref _editPostalCode, value);
    }
    public string ActionButtonText => IsEditMode ? "Edytuj" : "Zapisz";
    public string FormHeader => IsEditMode ? "Edytuj wybrany kod pocztowy" : "Dodaj nowy kod pocztowy";

    public IAsyncRelayCommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public IAsyncRelayCommand DeleteCommand { get; }

    public async Task InitializeAsync()
    {
        var items = await _postalCodeService.GetAllAsync();
        PostalCodes = [.. items.Select(p => new PostalCodeWrapper(p))];
    }

    private async Task DeletePostalCode()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybrany kod pocztowy?"))
        {
            await _postalCodeService.RemoveAsync(SelectedPostalCode.Id);
            PostalCodes.Remove(SelectedPostalCode);
        }
    }
    private async Task SubmitPostalCodeAsync()
    {
        if (IsEditMode)
        {
            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            await _postalCodeService.UpdateAsync(EditPostalCode.Id, dto);
            SelectedPostalCode.Code = EditPostalCode.Code;
            SelectedPostalCode.City = EditPostalCode.City;
            _dialogService.Show("Sukces", "Pomyślnie zaktualizowano kod pocztowy");
        }
        else
        {
            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            var entity = await _postalCodeService.CreateAsync(dto);
            PostalCodes.Add(new PostalCodeWrapper(entity));
            _dialogService.Show("Sukces", "Pomyślnie dodano kod pocztowy");
            ClearForm();
        }
    }
    private void ClearForm()
    {
        SelectedPostalCode = new PostalCodeWrapper(new PostalCode());
        EditPostalCode = new PostalCodeWrapper(new PostalCode());
    }
}
