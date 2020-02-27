using FluentAssertions;
using Machine.Api.Models;
using System.Collections.Generic;
using Xunit;

namespace Machine.Api.Tests
{
    [Collection("VendingMachine")]
    public class VendingMachineTest
    {
        [Fact]
        public void GivenOneBigCoin_WhenBuyAndNeedChangeWithSmallCoint_AndWheDontHave_ThenReturnEmpty()
        {
            Stock stock = new Stock();
            List<int> inserted = new List<int>(){200};
            stock.RemoveCoins(new Dictionary<int, int>(){ {10,100},{ 20,100 } });
            VendingMachine vendingMachine = new VendingMachine(stock);
            var dictionary = vendingMachine.Buy(inserted,30,DTOs.ProductType.ChickenSoup);
            Assert.Null(dictionary);
           
        }
        [Fact]
        public void GivenOneBigCoin_WhenBuyAndNeedChangeWithSmallCoint_AndHaveIt_ThenReturnSortestList()
        {
            Stock stock = new Stock();
            Dictionary<int,int> expected = new Dictionary<int, int>(){{50,1},{20,1}};
            List<int> inserted = new List<int>(){200};
            VendingMachine vendingMachine = new VendingMachine(stock);
            var dictionary = vendingMachine.Buy(inserted,70,DTOs.ProductType.Tea);
            expected.Should().BeEquivalentTo(dictionary);
           
        }
        [Fact]
        public void GivenASuperBigNumber_WhenBuyAndNeedChangeWithSmallCoint_ThenReturnSortestList()
        {
            Stock stock = new Stock();
            Dictionary<int,int> expected = new Dictionary<int, int>(){{200,4},{20,1},{50,1 } };
            List<int> inserted = new List<int>(){200,200,200,200,200};
            VendingMachine vendingMachine = new VendingMachine(stock);
            var dictionary = vendingMachine.Buy(inserted,870,DTOs.ProductType.Tea);
            expected.Should().BeEquivalentTo(dictionary);
           
        }
        [Fact]
        public void GivenASmallCoints_WhenBuyAndNeedChangeWithSmallCoints_ThenReturnTheOnesThatYouGaveMe()
        {
            Stock stock = new Stock();
            Dictionary<int,int> expected = new Dictionary<int, int>(){ {5,1},{1,1},{2,1} };
            List<int> inserted = new List<int>(){100,5,5,5,2,1};
            VendingMachine vendingMachine = new VendingMachine(stock);
            var dictionary = vendingMachine.Buy(inserted,8,DTOs.ProductType.Tea);
            expected.Should().BeEquivalentTo(dictionary);           
        }

        [Fact]
        public void GivenMoreMoney_WhenBuy_ThenReturnMoreThan0()
        {
            Stock stock = new Stock();
            int expected = 88;
            List<int> inserted = new List<int>(){100,100,5,5,5,2,1};
            VendingMachine vendingMachine = new VendingMachine(stock);
            int amountOfMoneyToReturn = vendingMachine.InsertedEnoughtMoney(inserted,DTOs.ProductType.Tea);
            amountOfMoneyToReturn.Should().Equals(expected);           
        }
        [Fact]
        public void GivenLessMoney_WhenBuy_ThenReturnMoreLessThan0()
        {
            Stock stock = new Stock();
            int expected = -12;
            List<int> inserted = new List<int>(){100,5,5,5,2,1};
            VendingMachine vendingMachine = new VendingMachine(stock);
            int amountOfMoneyToReturn = vendingMachine.InsertedEnoughtMoney(inserted,DTOs.ProductType.Tea);
            amountOfMoneyToReturn.Should().Equals(expected);           
        }
        [Fact]
        public void GivenTheExactMoney_WhenBuy_ThenReturn0()
        {
            Stock stock = new Stock();
            int expected = 0;
            List<int> inserted = new List<int>(){100,20,5,5};
            VendingMachine vendingMachine = new VendingMachine(stock);
            int amountOfMoneyToReturn = vendingMachine.InsertedEnoughtMoney(inserted,DTOs.ProductType.Tea);
            amountOfMoneyToReturn.Should().Equals(expected);           
        }
    }
}
