using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Admin;

public class UserAddViewModel : UserFormBaseViewModel<UserAddFormData>
{
    private readonly IUserService _userService;

    private UserWrapper _addUserWrapper;

    public UserAddViewModel(IUserService userService, IUserTypeService userTypeService,
        ILaboratoryService labService, IDialogService dialogService)
        : base(userTypeService, labService, dialogService)
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(AddUserAsync);
    }

    public static string HeaderText => "Dodaj użytkownika";
    public static string ActionButtonText => "Dodaj";
    public UserWrapper AddUserWrapper
    {
        get => _addUserWrapper;
        set => SetProperty(ref _addUserWrapper, value);
    }

    public ICommand SaveUserCommand { get; }

    public async Task InitializeAsync(UserWrapper wrapper)
    {
        AddUserWrapper = wrapper;
        await base.InitializeFormDataAsync();
    }

    private void ClearForm() => FormData.ClearForm();
    private async Task AddUserAsync()
    {
        try
        {
            var entity = await _userService.CreateUserAsync(FormData.ToDTO());
            AddUserWrapper.LoadFromEntity(entity);

            ClearForm();
            _dialogService.Show("Sukces", "Pomyślnie dodano nowego użytkownika");
        }
        catch (Exception ex)
        {
            _dialogService.Show("Błąd", $"{ex.Message}");
        }
    }
}
