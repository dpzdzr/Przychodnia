using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Message;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using System.Windows.Input;

namespace Przychodnia.ViewModel.Admin;

public class UserAddViewModel : UserFormBaseViewModel<UserAddFormData>
{
    private readonly IUserService _userService;
    private readonly IMessenger _messenger;

    public UserAddViewModel(IUserService userService, IUserTypeService userTypeService,
        ILaboratoryService labService, IDialogService dialogService, IMapper mapper, IMessenger messenger)
        : base(userTypeService, labService, dialogService, mapper)
    {
        _userService = userService;
        _messenger = messenger;
        SaveUserCommand = new AsyncRelayCommand(AddUserAsync);
    }

    public static string HeaderText => "Dodaj użytkownika";
    public static string ActionButtonText => "Dodaj";

    public ICommand SaveUserCommand { get; }

    public async Task InitializeAsync() => await base.InitializeFormDataAsync();

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
