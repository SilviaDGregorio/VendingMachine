using System.Collections.Generic;
using System.Linq;
using Machine.Api.DTOs;

namespace Machine.Api.Models
{
    public class Stock
    {
        private List<int> change;
        private readonly Dictionary<int, int> amountOfChange;
        private readonly Dictionary<ProductType,Product> productStock;
        public Stock()
        {
            productStock = new Dictionary<ProductType, Product>()
            {
                {ProductType.Tea, new Product(){ Type = ProductType.Tea,Price = 130,Stock = 10} },
                {ProductType.Espresso, new Product(){ Type = ProductType.Espresso,Price = 180,Stock = 20} },
                {ProductType.Juice, new Product(){ Type = ProductType.Juice,Price = 180,Stock = 20} },
                {ProductType.ChickenSoup, new Product(){ Type = ProductType.ChickenSoup,Price = 180,Stock = 15} }
            };
            change = new List<int>{ 50,20,10,100 };
            amountOfChange = new Dictionary<int, int>()
            {
                {50,100 },
                {20,100 },
                {10,100 },
                {100,100 }
            };

        }

        public int GetDifferentCoins()
        {
            return change.Count;
        }

        public void BoughtProduct(ProductType productType)
        {
            productStock[productType].Stock -= 1;
        }
        public void RemoveCoins(Dictionary<int,int> coins)
        {
            if(coins != null)
            {
                foreach (var coin in coins)
                {
                    amountOfChange[coin.Key] -= coin.Value;
                }
            }
        }

        public void InsertCoins(List<int> insertedMoney)
        {
            if(insertedMoney != null)
            {
                foreach (var coin in insertedMoney)
                {
                    if (!HasCoin(coin)) { amountOfChange.Add(coin, 0);}
                    amountOfChange[coin] += 1;
                }
                change = amountOfChange.Keys.OrderByDescending(x => x).ToList();
            }            
        }

        public bool CheckStock(ProductType productType) =>
        (productType) switch
        {
            ProductType.Tea =>  productStock[ProductType.Tea].Stock > 0,
            ProductType.Juice =>  productStock[ProductType.Juice].Stock > 0,
            ProductType.Espresso =>  productStock[ProductType.Espresso].Stock > 0,
            ProductType.ChickenSoup =>  productStock[ProductType.ChickenSoup].Stock > 0,
            _ => false
        };
        public bool HasCoin(int coint)
        {
            return amountOfChange.ContainsKey(coint);
        }
        public int GetCoint(int index)
        {
            return change[index];
        }
        public bool HaveMoreCoint(Dictionary<int, int> coinsAdded, int indexForChangeList)
        {
            int coin = GetCoint(indexForChangeList);
            int number = 0;
            coinsAdded?.TryGetValue(coin,out number);
            var doI =  amountOfChange[coin] >= (number+1);
            return doI;
        }
        public Product GetProduct(ProductType productName)
        {
            return productStock[productName];
        }
        public List<Product> GetProducts()
        {
            return productStock.Values.ToList();
        }
    }
}
