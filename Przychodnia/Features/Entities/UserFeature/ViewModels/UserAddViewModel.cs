using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Entities.LaboratoryFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Messages;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.UserTypesFeature.Services;
using Przychodnia.Shared.Services.DialogService;
using System.Windows.Input;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels;

public class UserAddViewModel : UserFormBaseViewModel<UserAddFormData>
{
    private readonly IUserService _userService;

    public UserAddViewModel(IUserService userService, IUserTypeService userTypeService,
        ILaboratoryService labService, IDialogService dialogService, IMapper mapper, IMessenger messenger)
        : base(userTypeService, labService, dialogService, mapper, messenger)
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(AddUserAsync);
    }

    public static string HeaderText => "Dodaj użytkownika";
    public static string ActionButtonText => "Dodaj";

    public ICommand SaveUserCommand { get; }

    public override async Task InitializeAsync() => await InitializeFormDataAsync();

    private async Task AddUserAsync()
    {
        try
        {
            var dto = _mapper.Map<UserDTO>(FormData);
            var entity = await _userService.CreateAsync(dto);
            NotifyUserAdded(entity);
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego użytkownika");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.Message}\n{ex.InnerException}");
        }
        ClearForm();
    }
    private void ClearForm() => FormData.ClearForm();
    private void NotifyUserAdded(User user)
        => _messenger.Send(new UserAddedMessage(user));
}
