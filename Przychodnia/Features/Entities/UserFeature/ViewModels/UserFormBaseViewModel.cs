﻿using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.LaboratoryFeature.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;
using Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.UserTypesFeature.Services;
using Przychodnia.Features.Entities.UserTypesFeature.Wrappers;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels;

public abstract partial class UserFormBaseViewModel<TForm> : BaseViewModel
    where TForm : UserBaseFormData, new()
{
    private readonly ILaboratoryService _labService;
    private readonly IUserTypeService _userTypeService;
    protected readonly IMapper _mapper;

    [ObservableProperty] private ObservableCollection<UserTypeWrapper> userTypes = [];
    [ObservableProperty] private ObservableCollection<LaboratoryWrapper> laboratories = [];

    public UserFormBaseViewModel(IUserTypeService userTypeService, ILaboratoryService laboratoryService, 
        IDialogService dialogService, IMapper mapper, IMessenger messenger)
        : base(dialogService, messenger)
    {
        _mapper = mapper;
        _userTypeService = userTypeService;
        _labService = laboratoryService;
        FormData.PropertyChanged += OnFormDataPropertyChanged;
    }

    public TForm FormData { get; } = new();
    public bool IsDoctor
         => FormData.SelectedUserType?.IsDoctor == true;
    public bool IsLabTechnician
        => FormData.SelectedUserType?.IsLabTechnician == true;
    public bool HasLicenseNumber
        => FormData.SelectedUserType?.HasLicenseNumber == true;

    public async Task InitializeFormDataAsync()
    {
        var userTypes = await _userTypeService.GetAllAsync();
        UserTypes = [.. userTypes.Select(ut => new UserTypeWrapper(ut))];
        var laboratories = await _labService.GetAllAsync();
        Laboratories = [null, .. laboratories.Select(l => new LaboratoryWrapper(l))];

        FormData.SelectedUserType = UserTypes.First();
    }

    protected void ValidateFormData()
    {
        if (!FormData.IsValid)
            throw new ValidationException("Uzupełnij poprawnie wszystkie wymagane pola");
    }

    private void OnFormDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(FormData.SelectedUserType))
        {
            OnPropertyChanged(nameof(IsDoctor));
            OnPropertyChanged(nameof(IsLabTechnician));
            OnPropertyChanged(nameof(HasLicenseNumber));
        }
    }
}
