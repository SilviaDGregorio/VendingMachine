using System;
using System.Collections.Generic;
using Machine.Api.DTOs;

namespace Machine.Api.Models.Interface
{
    public interface IVendingMachine
    {
        int InsertedEnoughtMoney(List<int> insertedMoney, ProductType productName);
        Dictionary<int,int> Buy(List<int> insertedMoney, int moneyToReturn,ProductType product);
    }
}
