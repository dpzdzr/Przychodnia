using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
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
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public partial class PostalCodeListViewModel : BaseViewModel
{
    private readonly IPostalCodeService _postalCodeService;
    private readonly IDialogService _dialogService;
    private readonly IMapper _mapper;
    private readonly IMessenger _messenger;

    [ObservableProperty] private bool isEditMode;
    [ObservableProperty] private PostalCodeWrapper editPostalCode;
    [ObservableProperty] private PostalCodeWrapper? selectedPostalCode;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> postalCodes = [];

    public PostalCodeListViewModel(IPostalCodeService postalCodeService, IDialogService dialogService, IMapper mapper, IMessenger messenger)
    {
        _postalCodeService = postalCodeService;
        _dialogService = dialogService;
        _mapper = mapper;
        _messenger = messenger;

        SaveCommand = new AsyncRelayCommand(SubmitPostalCodeAsync);
        CancelCommand = new RelayCommand(ClearForm);
        DeleteCommand = new AsyncRelayCommand(DeletePostalCode, () => IsEditMode);

        EditPostalCode = CreateEmptyPostalCodeWrapper();
    }

    public string ActionButtonText => IsEditMode ? "Edytuj" : "Zapisz";
    public string FormHeader => IsEditMode ? "Edytuj wybrany kod pocztowy"
        : "Dodaj nowy kod pocztowy";

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
        if (_dialogService.Confirm("Potwierdzenie usunięcia",
            "Czy na pewno chcesz usunąć wybrany kod pocztowy?"))
        {
            try
            {
                if (SelectedPostalCode is not null)
                {
                    if (SelectedPostalCode.Id is int id)
                        await _postalCodeService.RemoveAsync(id);
                    PostalCodes.Remove(SelectedPostalCode);
                }
            }
            catch (Exception ex)
            {
                _dialogService.Error("Błąd", $"{ex.Message}");
            }
        }
    }
    private async Task SubmitPostalCodeAsync()
    {
        if (IsEditMode)
            await UpdatePostalCodeAsync();
        else
            await AddPostalCodeAsync();
    }
    private async Task UpdatePostalCodeAsync()
    {
        try
        {
            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            if (EditPostalCode.Id is int id)
                await _postalCodeService.UpdateAsync(id, dto);
            _dialogService.Show("Sukces", "Pomyślnie zaktualizowano kod pocztowy");
            _mapper.Map(EditPostalCode, SelectedPostalCode);
            if (SelectedPostalCode is not null)
                _messenger.Send(new PostalCodeAddedOrEditedMessage(SelectedPostalCode));
        }
        catch (Exception ex)
        {
            _dialogService.Error("Błąd", $"{ex.Message}");
        }
    }
    private async Task AddPostalCodeAsync()
    {
        try
        {
            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            var entity = await _postalCodeService.CreateAsync(dto);
            var wrapper = new PostalCodeWrapper(entity);
            PostalCodes.Add(wrapper);
            _messenger.Send(new PostalCodeAddedOrEditedMessage(wrapper));
            _dialogService.Show("Sukces", "Pomyślnie dodano kod pocztowy");
            ClearForm();
        }
        catch (Exception ex)
        {
            _dialogService.Error("Błąd", $"{ex.Message}");
        }
    }
    private void ClearForm()
    {
        SelectedPostalCode = null;
        EditPostalCode = CreateEmptyPostalCodeWrapper();
    }
    private static PostalCodeWrapper CreateEmptyPostalCodeWrapper() => new(new PostalCode());
    partial void OnIsEditModeChanged(bool value)
    {
        OnPropertyChanged(nameof(ActionButtonText));
        OnPropertyChanged(nameof(FormHeader));
    }
    partial void OnSelectedPostalCodeChanged(PostalCodeWrapper? value)
    {
        IsEditMode = value != null;
        if (value is not null)
            _mapper.Map(value, EditPostalCode);
        else
            EditPostalCode = CreateEmptyPostalCodeWrapper();
        (DeleteCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
    }
}
