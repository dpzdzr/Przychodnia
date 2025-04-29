using CommunityToolkit.Mvvm.Input;
using Przychodnia.Data;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Przychodnia.ViewModel
{
    class DoctorsListViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _login;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public ICommand SaveCommand { get; }
        public ObservableCollection<User> Users { get; set; }
        public DoctorsListViewModel()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            Users = new ObservableCollection<User>(db.Users.ToList());

            SaveCommand = new RelayCommand(AddUser);
            

            var list = db.UserTypes.ToList();
            if(list.Count == 0)
            {
                var newType = new UserType { Name = "Lekarz", Id = 0 };
                db.UserTypes.Add(newType);
                db.SaveChanges();
            }

        }

        private void AddUser()
        {
            var newUser = new User { UserTypeId = 1, Login = Login };

            using var db = new AppDbContext();

            db.Users.Add(newUser);
            db.SaveChanges();

            Users.Add(newUser);
        }
    }
}
