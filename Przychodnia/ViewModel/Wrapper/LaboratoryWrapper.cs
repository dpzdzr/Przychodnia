using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class LaboratoryWrapper : ObservableObject
{
    [ObservableProperty] private int? id;
    [ObservableProperty] private string? name;
    [ObservableProperty] private string? type;
    [ObservableProperty] private UserWrapper? manager;

    public LaboratoryWrapper(Laboratory entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Type = entity.Type;
        Manager = entity.Manager is User manager ? new(manager) : null;
    }

    public string ManagerFullName
        => Manager is not null ? $"{Manager.FirstName} {Manager.LastName}" : string.Empty;

    partial void OnManagerChanged(UserWrapper? value)
    {
        OnPropertyChanged(nameof(ManagerFullName));
    }
}
