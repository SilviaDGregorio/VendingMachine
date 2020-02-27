using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Machine.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly VendingMachine _machine;
        private readonly ILogger<MachineController> _logger;

        public MachineController(ILogger<MachineController> logger,VendingMachine machine)
        {
            _logger = logger;
            _machine = machine;
        }
        
        /// <summary>
        /// Get the stock of the machine.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Products        
        ///
        /// </remarks>
        [Produces("application/json")]
        [HttpGet]
        [Route("Products")]
        public async Task<ActionResult> GetProducts()
        {            
            return Ok(_machine.GetProducts());
        }
        /// <summary>
        /// Try to buy a specific drink.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Buy
        ///     {
        ///        "insertedMoney":[50,10,1,1],
        ///        "Product": 0
        ///     }
        ///
        /// </remarks>
        /// <param name="InsertedMoney">The list of the coint we inserted</param>
        /// <param name="ProductType">The id of the product</param>
        /// <returns>Edited user</returns>
        /// <response code="200">User saved. Returns the updated user.</response>
        [Produces("application/json")]
        [HttpPost]
        [Route("Buy")]
        [ProducesResponseType(typeof(Dictionary<int,int>),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<Dictionary<int,int>> Post(Inserted inserted)
        {
            if (!_machine.CheckStock(inserted.Product))            
                throw new HttpResponseException(){Status= 400, Value = "Sorry, we do not have this product now" };
            int moneyToReturn = _machine.InsertedEnoughtMoney(inserted.InsertedMoney,inserted.Product);
            if(moneyToReturn < 0)
                throw new HttpResponseException(){Status= 400, Value = "Insufficient amount" };
            else if(moneyToReturn == 0)
                return new Dictionary<int,int>();
            return _machine.Buy(inserted.InsertedMoney,moneyToReturn,inserted.Product);
        }
    }
}
