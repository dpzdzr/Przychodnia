using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Wrappers;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;

public partial class LaboratoryWrapper : BaseWrapper
{
    [ObservableProperty]
    //[NotifyDataErrorInfo]
    //[Required(ErrorMessage = "Nazwa jest wymagana")]
    private string? name;

    [ObservableProperty]
    //[NotifyDataErrorInfo]
    //[Required(ErrorMessage = "Typ laboratorium jest wymagany")]
    private string? type;

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(ManagerFullName))]
    private UserWrapper? manager;
    
    [ObservableProperty]
    private List<UserWrapper>? workers;

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
