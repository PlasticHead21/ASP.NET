using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product _product, int _quantity)
        {
            CartLine line = lineCollection.Where(x => x.Product.ProductID == _product.ProductID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = _product, Quantity = _quantity });
            }
            else
            {
                line.Quantity += _quantity;
            }
        }
        public void RemoveLine(Product _product)
        {
            lineCollection.RemoveAll(x => x.Product.ProductID == _product.ProductID);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Product.Price * x.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
