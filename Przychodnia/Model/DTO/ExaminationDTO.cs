using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Model.DTO;

public record ExaminationDTO(int PatientId, int OrderedById, int PerformingDoctorId, int PerformingLaboratoryId);
