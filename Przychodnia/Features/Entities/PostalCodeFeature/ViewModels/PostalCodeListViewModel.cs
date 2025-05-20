using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.PostalCodeFeature.Messages;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.PostalCodeFeature.ViewModels;

public partial class PostalCodeListViewModel : BaseViewModel
{
    private readonly IPostalCodeService _postalCodeService;
    private readonly IMapper _mapper;

    [ObservableProperty] private bool isEditMode;
    [ObservableProperty] private PostalCodeWrapper editPostalCode;
    [ObservableProperty] private PostalCodeWrapper? selectedPostalCode;
    [ObservableProperty] private ObservableCollection<PostalCodeWrapper> postalCodes = [];

    public PostalCodeListViewModel(IPostalCodeService postalCodeService, IDialogService dialogService,
        IMapper mapper, IMessenger messenger)
        : base(dialogService, messenger)
    {
        _postalCodeService = postalCodeService;
        _mapper = mapper;

        SaveCommand = new AsyncRelayCommand(SubmitPostalCodeAsync);
        CancelCommand = new RelayCommand(ClearForm, () => IsEditMode);
        DeleteCommand = new AsyncRelayCommand(DeletePostalCode, () => IsEditMode);

        EditPostalCode = CreateEmptyPostalCodeWrapper();
    }

    public string ActionButtonText => IsEditMode ? "Edytuj" : "Dodaj";
    public string FormHeader => IsEditMode ? "Edytuj wybrany kod pocztowy"
        : "Dodaj nowy kod pocztowy";

    public IAsyncRelayCommand SaveCommand { get; }
    public IRelayCommand CancelCommand { get; }
    public IAsyncRelayCommand DeleteCommand { get; }

    public override async Task InitializeAsync()
    {
        var items = await _postalCodeService.GetAllAsync();
        PostalCodes = [.. items.Select(p => new PostalCodeWrapper(p))];
    }

    private async Task DeletePostalCode()
    {
        await TryExecuteAsync(async () =>
        {
            if (SelectedPostalCode is not null &&
            _dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybrany kod pocztowy?"))
            {
                if (SelectedPostalCode.Id is int id)
                    await _postalCodeService.RemoveAsync(id);
                PostalCodes.Remove(SelectedPostalCode);
            }
        });
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
        await TryExecuteAsync(async () =>
        {
            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            if (EditPostalCode.Id is int id)
            {
                ValidateUserInput();

                await _postalCodeService.UpdateAsync(id, dto);
                _dialogService.Show("Sukces", "Pomyślnie zaktualizowano kod pocztowy");
                _mapper.Map(EditPostalCode, SelectedPostalCode);

                var entity = await _postalCodeService.GetByIdAsync(id);
                _messenger.Send(new PostalCodeChanged(new(entity!, EntityChangedAction.Edited)));
            }
        });
    }
    private async Task AddPostalCodeAsync()
    {
        await TryExecuteAsync(async () =>
        {
            ValidateUserInput();

            var dto = _mapper.Map<PostalCodeDTO>(EditPostalCode);
            var entity = await _postalCodeService.CreateAsync(dto);
            PostalCodes.Add(new(entity));

            _messenger.Send(new PostalCodeChanged(new(entity, EntityChangedAction.Added)));
            _dialogService.Show("Sukces", "Pomyślnie dodano kod pocztowy");

            ClearForm();
        });
    }
    private void ClearForm()
    {
        SelectedPostalCode = null;
        EditPostalCode = CreateEmptyPostalCodeWrapper();
    }
    private void ValidateUserInput()
    {
        if (EditPostalCode is null || !(EditPostalCode.IsValid))
            throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");
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
