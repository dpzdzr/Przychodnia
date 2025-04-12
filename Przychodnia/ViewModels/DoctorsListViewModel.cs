using CommunityToolkit.Mvvm.Input;
using Przychodnia.Data;
using Przychodnia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Przychodnia.ViewModels
{
    class DoctorsListViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _login;

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        public ICommand SaveCommand { get; }
        public ObservableCollection<User> Users { get; set; }
        public DoctorsListViewModel()
        {
            using var db = new MyAppContext();
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

            using var db = new MyAppContext();

            db.Users.Add(newUser);
            db.SaveChanges();

            Users.Add(newUser);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
