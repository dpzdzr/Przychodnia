﻿using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.PatientFeature.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Messages;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Wrappers;
using Przychodnia.Features.Entities.UserTypesFeature.Services;
using Przychodnia.Features.Entities.UserTypesFeature.Wrappers;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System.Collections.ObjectModel;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels;

public partial class UserListViewModel : BaseListViewModel<UserWrapper>
{
    private readonly IUserService _userService;
    private readonly IUserTypeService _userTypeService;
    private readonly IMapper _mapper;

    private List<UserTypeWrapper> userTypes = [];
    [ObservableProperty]
    private ObservableCollection<string> userTypeNames = [];
    [ObservableProperty]
    private string selectedUserTypeName = string.Empty;
    [ObservableProperty]
    private string selectedUserFirstName = string.Empty;
    [ObservableProperty]
    private string selectedUserLastName = string.Empty;

    public UserListViewModel(IDialogService dialogService, IUserService userService,
    INavigationService navigationService, IServiceProvider serviceProvider,
    IUserTypeService userTypeService, IMessenger messenger, IMapper mapper)
        : base(dialogService, navigationService, serviceProvider, messenger)
    {
        _userService = userService;
        _userTypeService = userTypeService;
        _mapper = mapper;
        _userTypeService = userTypeService;

        _messenger.Register<UserChangedMessage>(this, (r, m) => HandleUserChangedMessage(m));
    }

    public static string HeaderText => "Użytkownicy";

    public override async Task InitializeAsync()
    {
        _allItems = [.. (await _userService.GetAllWithDetailsAsync())
            .Select(u => new UserWrapper(u, true))];
        Items = [.. _allItems];

        var names = await _userTypeService.GetNamesAsync();
        UserTypeNames = ["brak", .. names];

        var types = await _userTypeService.GetAllAsync();
        userTypes = [.. types.Select(t => new UserTypeWrapper(t))];
    }

    protected override async Task Add()
    {
        var addVm = _serviceProvider.GetRequiredService<UserAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }
    protected override async Task Edit()
    {
        var editVm = _serviceProvider.GetRequiredService<UserEditViewModel>();
        await editVm.InitializeAsync(SelectedItem);
        _navigationService.NavigateTo(editVm);
    }
    protected override async Task Remove()
    {
        await TryExecuteAsync(async () =>
        {
            if (SelectedItem?.Id is int userId &&
                Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
            {
                await _userService.RemoveAsync(userId);
                Items.Remove(SelectedItem);
            }
        });
    }
    protected override void Filter() => Items = [.. ApplyFilters()];
    protected override void ClearFilter()
    {
        SelectedUserTypeName = UserTypeNames.First();
        SelectedUserFirstName = string.Empty;
        SelectedUserLastName = string.Empty;

        Items = [.. _allItems];
    }

    private IEnumerable<UserWrapper> ApplyFilters()
    {
        var query = _allItems?.AsEnumerable() ?? [];

        if (SelectedUserTypeName != "brak" &&
            userTypes.FirstOrDefault(t => t.Name == SelectedUserTypeName) is { } selectedType)
        {
            query = query.Where(u => u.UserType!.Id == selectedType.Id);
        }

        query = FilterByStringAttribute(query, u => u.FirstName, SelectedUserFirstName);
        query = FilterByStringAttribute(query, u => u.LastName, SelectedUserLastName);

        return query;
    }
    private void HandleUserChangedMessage(UserChangedMessage message)
    {
        switch (message.Value.Action)
        {
            case EntityChangedAction.Added:
                HandleAdded((User)message.Value.Entity);
                break;
            case EntityChangedAction.Edited:
                HandleEdited((User)message.Value.Entity);
                break;
        }
    }
    private void HandleAdded(User entity)
    {
        Items.Add(new(entity));
    }
    private void HandleEdited(User entity)
    {
        var current = Items.First(p => p.Id == entity.Id);
        _mapper.Map(entity, current);
    }
}
