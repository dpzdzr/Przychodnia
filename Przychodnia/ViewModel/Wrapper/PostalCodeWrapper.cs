using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class PostalCodeWrapper : ObservableObject
{
    public int Id { get; }

    [ObservableProperty]
    private string code;

    [ObservableProperty]
    private string city;

    public PostalCodeWrapper(PostalCode entity)
    {
        Id = entity.Id;
        code = entity.Code;
        city = entity.City;
    }

    public PostalCode ToEntity()
        => new() { Id = Id, Code = code, City = city };
}
