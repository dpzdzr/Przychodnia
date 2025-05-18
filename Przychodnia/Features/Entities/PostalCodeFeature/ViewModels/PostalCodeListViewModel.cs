using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.PostalCodeFeature.Messages;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.PostalCodeFeature.ViewModels;

public partial class PostalCodeListViewModel : BaseViewModel
{
    private readonly IPostalCodeService _postalCodeService;
    private readonly IMapper _mapper;
    private readonly IMessenger _messenger;

    [ObservableProperty] private bool isEditMode;
    [ObservableProperty] private PostalCodeWrapper editPostalCode;
    [ObservableProperty] private PostalCodeWrapper? selectedPostalCode;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> postalCodes = [];

    public PostalCodeListViewModel(IPostalCodeService postalCodeService, IDialogService dialogService, IMapper mapper, IMessenger messenger) : base(dialogService)
    {
        _postalCodeService = postalCodeService;
        _mapper = mapper;
        _messenger = messenger;

        SaveCommand = new AsyncRelayCommand(SubmitPostalCodeAsync);
        CancelCommand = new RelayCommand(ClearForm, () => IsEditMode);
        DeleteCommand = new AsyncRelayCommand(DeletePostalCode, () => IsEditMode);

        EditPostalCode = CreateEmptyPostalCodeWrapper();
    }

    public string ActionButtonText => IsEditMode ? "Edytuj" : "Zapisz";
    public string FormHeader => IsEditMode ? "Edytuj wybrany kod pocztowy"
        : "Dodaj nowy kod pocztowy";

    public IAsyncRelayCommand SaveCommand { get; }
    public IRelayCommand CancelCommand { get; }
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
            if (!EditPostalCode.IsValid)
                throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");

            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            if (EditPostalCode.Id is int id)
                await _postalCodeService.UpdateAsync(id, dto);
            _dialogService.Show("Sukces", "Pomyślnie zaktualizowano kod pocztowy");
            _mapper.Map(EditPostalCode, SelectedPostalCode);
            if (SelectedPostalCode is not null)
                _messenger.Send(new PostalCodeAltered(SelectedPostalCode));
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
            if (!EditPostalCode.IsValid)
                throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");

            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            var entity = await _postalCodeService.CreateAsync(dto);
            var wrapper = new PostalCodeWrapper(entity);
            PostalCodes.Add(wrapper);
            _messenger.Send(new PostalCodeAltered(wrapper));
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
        DeleteCommand.NotifyCanExecuteChanged();
        CancelCommand.NotifyCanExecuteChanged();
    }
}
