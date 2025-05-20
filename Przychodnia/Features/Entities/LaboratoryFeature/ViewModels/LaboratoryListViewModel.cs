using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.LaboratoryFeature.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Wrappers;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.LaboratoryFeature.ViewModels;

public partial class LaboratoryListViewModel : BaseViewModel
{
    private readonly ILaboratoryService _labService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    [ObservableProperty] private bool isEditMode;
    [ObservableProperty] private LaboratoryWrapper editLab = new();
    [ObservableProperty] private LaboratoryWrapper? selectedLab;
    [ObservableProperty] private ObservableCollection<UserWrapper> workers = [];
    [ObservableProperty] private ObservableCollection<UserWrapper> managers = [];
    [ObservableProperty] private ObservableCollection<LaboratoryWrapper> labs = [];

    public LaboratoryListViewModel(IDialogService dialogService, ILaboratoryService labService,
        IUserService userService, IMapper mapper, IMessenger messenger)
        : base(dialogService, messenger)
    {
        _labService = labService;
        _userService = userService;
        _mapper = mapper;

        ActionButtonCommand = new AsyncRelayCommand(SubmitLaboratoryAsync);
        CancelButtonCommand = new RelayCommand(ClearForm, () => IsEditMode);
        RemoveButtonCommand = new AsyncRelayCommand(RemoveLaboratory, () => IsEditMode);

        EditLab = new(new Laboratory());
    }

    public string ActionButtonText => IsEditMode ? "Edytuj" : "Dodaj";
    public string FormHeaderText => IsEditMode ? "Edytuj wybrane laboratorium" : "Dodaj nowe laboratorium";

    public IAsyncRelayCommand ActionButtonCommand { get; }
    public IAsyncRelayCommand RemoveButtonCommand { get; }
    public IRelayCommand CancelButtonCommand { get; }

    public override async Task InitializeAsync()
    {
        var labs = await _labService.GetAllWithDetailsAsync();
        Labs = [.. labs.Select(l => new LaboratoryWrapper(l, true, true))];

        var managers = await _userService.GetUsersByUserTypeAsync(UserTypeEnum.KierownikLaboratorium);
        Managers = [.. managers.Select(m => new UserWrapper(m))];

        Managers.Insert(0, new UserWrapper(null, createDummy: true)); // <- dummy option
    }

    private async Task RemoveLaboratory()
    {
        await TryExecuteAsync(async () =>
        {
            var confirmation = _dialogService.Confirm("Potwierdzenie usunięcia",
                "Czy na pewno chcesz usunąć wybrane laboratorium?");
            if (SelectedLab?.Id is int labId && confirmation is true)
            {
                await _labService.RemoveAsync(labId);
                Labs.Remove(SelectedLab);
            }
        });
    }
    private async Task SubmitLaboratoryAsync()
    {
        await TryExecuteAsync(async () =>
        {
            ValidateUserInput();
            if (IsEditMode)
                await EditLaboratory();
            else
                await AddLaboratory();
        });
    }
    private async Task EditLaboratory()
    {
        await TryExecuteAsync(async () =>
        {
            if (EditLab.Id is int id)
            {
                var dto = _mapper.Map<LaboratoryDTO>(EditLab);
                await _labService.UpdateAsync(id, dto);
            }
            else
            {
                _dialogService.Error("Błąd", "Nie można zidentyfikować edytowanego laboratorium.");
                return;
            }
            _dialogService.Show("Sukces", "Pomyślnie edytowano wybrane laboratorium");
            _mapper.Map(EditLab, SelectedLab);
        });
    }
    private async Task AddLaboratory()
    {
        await TryExecuteAsync(async () =>
        {
            var dto = _mapper.Map<LaboratoryDTO>(EditLab);
            var entity = await _labService.CreateAsync(dto);
            Labs.Add(new LaboratoryWrapper(entity, includeManager: true, includeWorkers: true));
            _dialogService.Show("Sukces", "Pomyślnie dodano nowe laboratorium");
            ClearForm();
        });
    }
    private void ValidateUserInput()
    {
        if (EditLab is null || !(EditLab.IsValid))
            throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");
    }
    private void ClearForm()
    {
        SelectedLab = null;
        EditLab = new(new Laboratory());
    }
    private UserWrapper? FindMatchingManager(int? id)
        => Managers.FirstOrDefault(m => m.Id == id);

    partial void OnSelectedLabChanged(LaboratoryWrapper? value)
    {
        IsEditMode = value != null;
        if (value is not null)
        {
            _mapper.Map(value, EditLab);
            EditLab.Manager = FindMatchingManager(value.Manager?.Id);
        }
        else
            EditLab = new(new Laboratory());

        OnPropertyChanged(nameof(ActionButtonText));
        OnPropertyChanged(nameof(FormHeaderText));
        RemoveButtonCommand.NotifyCanExecuteChanged();
        CancelButtonCommand.NotifyCanExecuteChanged();
    }
}
