using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Shared;

public class LaboratoryListViewModel : BaseViewModel
{
    private readonly IDialogService _dialogService;
    private readonly ILaboratoryService _labService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    private ObservableCollection<LaboratoryWrapper> _labs;
    private LaboratoryWrapper? _selectedLab;
    private LaboratoryWrapper _editLab;
    private ObservableCollection<UserWrapper> _managers;
    private UserWrapper? _selectedManager;
    private bool _isEditMode;

    public LaboratoryListViewModel(IDialogService dialogService, ILaboratoryService labService,
        IUserService userService, IMapper mapper)
    {
        _dialogService = dialogService;
        _labService = labService;
        _userService = userService;
        _mapper = mapper;

        ActionButtonCommand = new AsyncRelayCommand(SubmitLaboratoryAsync);
        CancelButtonCommand = new RelayCommand(ClearForm);
        RemoveButtonCommand = new AsyncRelayCommand(RemoveLaboratory, () => IsEditMode);

        EditLab = new(new Laboratory());
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetProperty(ref _isEditMode, value);
    }
    public LaboratoryWrapper? SelectedLab
    {
        get => _selectedLab;
        set
        {
            if (SetProperty(ref _selectedLab, value))
            {
                IsEditMode = value != null;
                SelectedManager = value?.Manager is not null ? value.Manager : null;
                if (value is not null) _mapper.Map(value, EditLab);
                OnPropertyChanged(nameof(ActionButtonText));
                OnPropertyChanged(nameof(FormHeaderText));
                (RemoveButtonCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
            }
        }
    }
    public LaboratoryWrapper EditLab
    {
        get => _editLab;
        set => SetProperty(ref _editLab, value);
    }
    public UserWrapper SelectedManager
    {
        get => _selectedManager;
        set
        {
            if (SetProperty(ref _selectedManager, value))
                EditLab.Manager = value;
    }
    }
    public ObservableCollection<UserWrapper> Managers
    {
        get => _managers;
        set => SetProperty(ref _managers, value);
    }
    public ObservableCollection<LaboratoryWrapper> Labs
    {
        get => _labs;
        set => SetProperty(ref _labs, value);
    }
    public string ActionButtonText => IsEditMode ? "Edytuj" : "Dodaj";
    public string FormHeaderText => IsEditMode ? "Edytuj wybrane laboratorium" : "Dodaj nowe laboratorium";

    public IAsyncRelayCommand ActionButtonCommand { get; }
    public IAsyncRelayCommand RemoveButtonCommand { get; }
    public IRelayCommand CancelButtonCommand { get; }

    public async Task InitializeAsync()
    {
        var labs = await _labService.GetAllWithDetailsAsync();
        Labs = [.. labs.Select(l => new LaboratoryWrapper(l))];

        var managers = await _userService.GetUsersByUserType(UserTypeEnum.KierownikLaboratorium);
        Managers = [.. managers.Select(m => new UserWrapper(m))];
    }

    private async Task RemoveLaboratory()
    {

    }

    private async Task SubmitLaboratoryAsync()
    {
        if (IsEditMode)
        {

        }
        else
        {
            var dto = _mapper.Map<LaboratoryDTO>(EditLab);
            var entity = await _labService.AddAsync(dto);
            Labs.Add(new LaboratoryWrapper(entity));
            _dialogService.Show("Sukces", "Pomyślnie dodano nowe laboratorium");
            ClearForm();
        }
    }

    private void ClearForm()
    {
        SelectedLab = null;
        EditLab = new(new Laboratory());
    }

}
