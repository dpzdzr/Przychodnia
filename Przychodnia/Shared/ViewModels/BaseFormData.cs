using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Shared.ViewModels
{
    public class BaseFormData : ObservableValidator
    {
        public bool IsValid
        {
            get
            {
                ValidateAllProperties();
                return !HasErrors;
            }
        }
        public void ClearAllErrors()
        {
            var errorPropertyNames = GetErrors()
                .SelectMany(e => e.MemberNames)
                .Distinct()
                .ToList();

            foreach (var propertyName in errorPropertyNames)
            {
                ClearErrors(propertyName);
            }
        }
    }
}
