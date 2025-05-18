using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Models;

public record LaboratoryDTO(string Name, string Type, int? ManagerId);
