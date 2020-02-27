using Machine.Api.DTOs;
using Machine.Api.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Machine.Api.Models
{
    public class VendingMachine : IVendingMachine
    {
        private readonly ConcurrentDictionary<int, Dictionary<int, int>> justCalculated;
        private readonly Stock _stock;
        public VendingMachine(Stock stock)
        {
            justCalculated = new ConcurrentDictionary<int, Dictionary<int, int>>();
            _stock = stock;
        }
        
        public int InsertedEnoughtMoney(List<int> insertedMoney, ProductType productName)
        {
            int amount = insertedMoney.Sum();
            int price = _stock.GetProduct(productName).Price;
            return amount-price;
        }

        public Dictionary<int,int> Buy(List<int> insertedMoney, int moneyToReturn,ProductType product)
        {
            _stock.InsertCoins(insertedMoney);
            var coinsToReturn = Algorithm(new Dictionary<int, int>(), moneyToReturn, 0);
            if (coinsToReturn != null)
            {
                _stock.BoughtProduct(product);
                _stock.RemoveCoins(coinsToReturn);
            }
            return coinsToReturn;
        }

        public List<Product> GetProducts()
        {
            return _stock.GetProducts();
        }

        private Dictionary<int,int>  Algorithm(Dictionary<int,int> change, int amount, int indexForChangeList)
        {
            if (amount == 0) return change;
            if (amount < 0 || indexForChangeList >= _stock.GetDifferentCoins()) return null;
            int coint = _stock.GetCoint(indexForChangeList);
            if (!justCalculated.TryGetValue(amount - coint, out var getTheCoint) && _stock.HaveMoreCoint(change, indexForChangeList))
            {
                getTheCoint = Algorithm(InsertCountInDictionary(change, coint), amount - coint, indexForChangeList);
                if (getTheCoint != null) justCalculated.TryAdd(amount, getTheCoint);
            }
            if (!justCalculated.TryGetValue(amount, out var notGetTheCoint))
            {
                notGetTheCoint = Algorithm(new Dictionary<int, int>(change), amount, ++indexForChangeList);
                if (notGetTheCoint != null) justCalculated.TryAdd(amount, notGetTheCoint);
            }

            return GetTheMinimumCoints(getTheCoint, notGetTheCoint);
        }

        public bool CheckStock(ProductType product)
        {
            return _stock.CheckStock(product);
        }

        private static Dictionary<int, int> GetTheMinimumCoints(Dictionary<int, int> getTheCoint, Dictionary<int, int> notGetTheCoint)
        {

            if(getTheCoint is null) return notGetTheCoint;
            if(notGetTheCoint is null) return notGetTheCoint;
            return getTheCoint.Count < notGetTheCoint.Count ? getTheCoint : notGetTheCoint;           
        }

        private Dictionary<int, int> InsertCountInDictionary(Dictionary<int, int> change, int coint)
        {
            var inserted= change.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
            if(!inserted.ContainsKey(coint)) inserted.Add(coint,0);
            inserted[coint]+=1;
            return inserted;
        }
    }
}
