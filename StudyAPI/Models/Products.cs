using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAPI.Models
{
  public class Products
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public int Quantity { get; set; }
    public int LocationId { get; set; }
    public int FamilyId { get; set; }
  }
}
