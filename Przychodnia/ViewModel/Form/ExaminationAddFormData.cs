using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Form;

public class ExaminationAddFormData : ExaminationBaseFormData
{
    public void ClearForm()
    {
        ExaminationType = default;
        Patient = null;
        OrderedBy = null;
        PerformingDoctor = null;
        PerformingLaboratory = null;
    }
}
