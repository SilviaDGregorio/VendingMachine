using System.Text.Json.Serialization;

namespace Web.DTOs
{
    public class Product
    {
        [JsonPropertyName("type")]
        public ProductType Type {get;set;}
        [JsonPropertyName("name")]
        public string Name => Type.ToString();
        [JsonPropertyName("price")]
        public int Price { get; set; }
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
    public enum ProductType
    {
        Tea = 0,Espresso = 1,Juice = 2,ChickenSoup = 3
    }
}
