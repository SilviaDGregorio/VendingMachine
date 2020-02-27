using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.DTOs
{
    public class Inserted
    {
        [Required]
        public List<int> InsertedMoney { get; set; }
        [Required]
        public ProductType Product { get; set; } 
    }
}
