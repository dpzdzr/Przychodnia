using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Models;
class Laboratory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Type { get; set; }

    [Required]
    public User Manager { get; set; }

}
