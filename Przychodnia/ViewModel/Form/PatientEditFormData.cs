using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Form;

public partial class PatientEditFormData : PatientFormDataBase
{
    [ObservableProperty] public int id;

    public void LoadFromPatient(Patient patient)
    {
        this.Id = patient.Id;
        this.FirstName = patient.FirstName;
        this.LastName = patient.LastName;
        this.Pesel = patient.Pesel;
        this.Street = patient.Street;
        this.HouseNumber = patient.HouseNumber;
        this.ApartmentNumber = patient.ApartmentNumber;
        this.PostalCode = patient.PostalCode;
        this.Sex = (Sex)patient.Sex;
    }

    public void LoadToPatient(Patient patient)
    {
        patient.FirstName = this.FirstName;
        patient.LastName = this.LastName;
        patient.Pesel = this.Pesel;
        patient.Street = this.Street;
        patient.HouseNumber = this.HouseNumber;
        patient.ApartmentNumber = this.ApartmentNumber;
        patient.PostalCode = this.PostalCode;
        patient.Sex = this.Sex;
    }
}
