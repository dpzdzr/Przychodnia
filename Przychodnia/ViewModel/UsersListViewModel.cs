using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;

namespace Przychodnia.ViewModel;

public class UsersListViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IUserRepository _userRepository;
    private User _selectedUser;

    public ObservableCollection<User> Users { get; }
    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            if (SetProperty(ref _selectedUser, value))
            {
                OnPropertyChanged(nameof(CanExecute));
            }
        }
    }

    public bool CanExecute => SelectedUser != null;

    public ICommand DeleteUserCommand { get; }

    public UsersListViewModel(IUserRepository userRepository, IDialogService dialogService)
    {
        _dialogService = dialogService;
        _userRepository = userRepository;
        Users = [.. userRepository.GetAllWithUserType()];
        DeleteUserCommand = new RelayCommand(DeleteUser);
    }

    private void DeleteUser()
    {
        if (_dialogService.Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego użytkownika?"))
        {
            _userRepository.Remove(SelectedUser);
            Users.Remove(SelectedUser);
        }
    }


}
