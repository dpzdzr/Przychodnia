using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class PostalCodeWrapper(PostalCode entity) : ObservableObject
{
    [ObservableProperty] private int id = entity.Id;
    [ObservableProperty] private string code = entity.Code;
    [ObservableProperty] private string city = entity.City;
    public PostalCodeWrapper Clone() => new(new PostalCode { Id = Id, Code = code, City = city });
}
