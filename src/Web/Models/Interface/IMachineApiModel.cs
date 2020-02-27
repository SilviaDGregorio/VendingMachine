using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Models.Interface
{
    public interface IMachineApiModel
    {
        Task<List<Product>> GetProducts();
        Task<(string,string)> Buy(Inserted inserted);
    }
}
