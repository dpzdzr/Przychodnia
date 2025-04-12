using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Models;
class ManagementPeriod
{
    public int ManagerId { get; set; }
    public virtual User Manager { get; set; }

    public int LaboratoryId { get; set; }
    public virtual Laboratory Laboratory { get; set; }

    public DateOnly from { get; set; }
    public DateOnly? to { get; set; }
}
