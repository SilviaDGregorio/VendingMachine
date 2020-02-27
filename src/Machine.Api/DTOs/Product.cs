namespace Machine.Api.DTOs
{
    public class Product
    {
        public ProductType Type {get;set;}
        public string Name => Type.ToString();
        public int Price {get;set;}
        public int Stock { get; set; }
    }
    public enum ProductType
    {
        Tea = 0,Espresso = 1,Juice = 2,ChickenSoup = 3
    }
}
