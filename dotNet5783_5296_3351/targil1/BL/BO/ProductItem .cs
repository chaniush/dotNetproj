using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public string? Name { get; set; }
        public int ID { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int InStock { get; set; }
        public Color Color { get; set; }
        public int AmountInCart { get; set; }
        public override string ToString() => $@"
         Name:{Name} 
         ID:{ID}
         Price : {Price}
         Category: {Category}
         InStock: {InStock}
         Color: {Color}
         AmountInCart: {AmountInCart}";
    }
}
