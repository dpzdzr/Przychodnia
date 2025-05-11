using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Przychodnia.ViewModel.Wrapper.WrapperHelper;

namespace Przychodnia.ViewModel.Wrapper;

public partial class LaboratoryWrapper : ObservableObject
{
    [ObservableProperty] private int? id;
    [ObservableProperty] private string? name;
    [ObservableProperty] private string? type;
    [ObservableProperty] private UserWrapper? manager;
    [ObservableProperty] private List<UserWrapper>? workers;
    
    public LaboratoryWrapper() { }
    public LaboratoryWrapper(Laboratory entity, bool includeManager = false, bool includeWorkers = false)
    {
        Id = entity.Id;
        Name = entity.Name;
        Type = entity.Type;

        if (includeManager && entity.Manager is not null)
            Manager = new(entity.Manager);

        if (includeWorkers && entity.Workers is not null)
            Workers = entity.Workers?.Select(u => new UserWrapper(u)).ToList();
    }

    public string ManagerFullName
        => Manager is not null ? $"{Manager.FirstName} {Manager.LastName}" : string.Empty;

    partial void OnManagerChanged(UserWrapper? value)
    {
        OnPropertyChanged(nameof(ManagerFullName));
    }
}
