using System.Runtime.InteropServices.ComTypes;

namespace Codecool.CodecoolShop.Models
{
    public struct LineItem
    {
        public decimal price { get; set; }

        public int Quantity { get; set; }

        public Product product;
    }
}