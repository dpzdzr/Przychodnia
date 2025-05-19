using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.LaboratoryFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.UserFeature.Wrappers;
using Przychodnia.Features.Entities.UserTypesFeature.Services;
using Przychodnia.Shared.Services.DialogService;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels;

public partial class UserEditViewModel : UserFormBaseViewModel<UserEditFormData>
{
    private readonly IUserService _userService;

    [ObservableProperty] private UserWrapper? editUserWrapper;

    public UserEditViewModel(IDialogService dialogService, ILaboratoryService labService, IUserTypeService userTypeService, 
        IUserService userService, IMapper mapper, IMessenger messenger)
        : base(userTypeService, labService, dialogService, mapper, messenger)
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(EditUserAsync);
    }

    public static string HeaderText => "Edytuj użytkownika";
    public static string ActionButtonText => "Edytuj";

    public IAsyncRelayCommand SaveUserCommand { get; }

    public async Task InitializeAsync(UserWrapper wrapper)
    {
        EditUserWrapper = wrapper;
        await InitializeFormDataAsync();
        _mapper.Map(EditUserWrapper, FormData);
        FormData.SelectedUserType = UserTypes.FirstOrDefault(ut => ut.Id == FormData.SelectedUserType!.Id);
    }

    private async Task EditUserAsync()
    {
        if (EditUserWrapper?.Id is int userId)
        {
            _mapper.Map(FormData, EditUserWrapper);
            await _userService.UpdateAsync(userId, _mapper.Map<UserDTO>(EditUserWrapper));
            _dialogService.Show("Sukces", "Pomyślnie edytowano użytkownika");
        }
    }
}
