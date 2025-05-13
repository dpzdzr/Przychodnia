using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class LaboratoryWrapper : BaseWrapper
{
    [ObservableProperty] private string? name;
    [ObservableProperty] private string? type;
    [NotifyPropertyChangedFor(nameof(ManagerFullName))]
    [ObservableProperty] private UserWrapper? manager;
    [ObservableProperty] private List<UserWrapper>? workers;
    
    public LaboratoryWrapper() { }
    public LaboratoryWrapper(Laboratory entity, bool includeManager = false, bool includeWorkers = false)
    {
        Id = entity.Id;
        Name = entity.Name;
        Type = entity.Type;

        if (includeManager)
            Manager = WrapIfNotNull(entity.Manager, m => new UserWrapper(m, false));

        if (includeWorkers && entity.Workers is not null)
            Workers = MapListIfNotNull(entity.Workers, u => new UserWrapper(u, false));
    }

    public string? ManagerFullName
        => Manager is not null ? $"{Manager.FirstName} {Manager.LastName}" : null;
}
