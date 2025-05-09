using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Admin;

public class UserEditViewModel : UserFormBaseViewModel<UserEditFormData>
{
    private readonly IUserService _userService;

    private UserWrapper _editUserWrapper;

    public UserEditViewModel(IDialogService dialogService, ILaboratoryService labService, IUserTypeService userTypeService, IUserService userService, IMapper mapper) 
        : base(userTypeService, labService, dialogService, mapper)
    {
        _userService = userService;
        SaveUserCommand = new AsyncRelayCommand(EditUserAsync);
    }

    public static string HeaderText => "Edytuj użytkownika";
    public static string ActionButtonText => "Edytuj";
    public UserWrapper EditUserWrapper
    {
        get => _editUserWrapper;
        set => SetProperty(ref _editUserWrapper, value);
    }

    public IAsyncRelayCommand SaveUserCommand { get; }

    public async Task InitializeAsync(UserWrapper wrapper)
    {
        EditUserWrapper = wrapper;
        await base.InitializeFormDataAsync();
        _mapper.Map(EditUserWrapper, FormData);
    }

    private async Task EditUserAsync()
    {
        _mapper.Map(FormData, EditUserWrapper);
        await _userService.UpdateAsync(EditUserWrapper.Id, _mapper.Map<UserDTO>(EditUserWrapper));
        _dialogService.Show("Sukces", "Pomyślnie edytowano użytkownika");
    }
}
