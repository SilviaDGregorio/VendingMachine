using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Machine.Api.DTOs;

namespace Machine.Api.Controllers
{
    public class Inserted
    {
        [Required]
        public List<int> InsertedMoney { get; set; }
        [Required]
        public ProductType Product { get; set; }
        
    }
}
