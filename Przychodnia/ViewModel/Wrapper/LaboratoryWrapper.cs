using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class LaboratoryWrapper(Laboratory entity) : ObservableObject
{
    [ObservableProperty] private int? id = entity.Id;
    [ObservableProperty] private string? name = entity.Name;
    [ObservableProperty] private string? type = entity.Type;
    [ObservableProperty] private UserWrapper? manager = 
        entity.Manager is User manager ? new(manager) : null;

    public string ManagerFullName
        => Manager is not null ? $"{Manager.FirstName} {Manager.LastName}" : string.Empty;
}
