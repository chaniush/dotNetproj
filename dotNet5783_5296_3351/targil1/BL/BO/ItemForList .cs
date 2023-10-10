using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class ItemForList
    {
        public string? Name { get; set; }
        public int ID { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public Color Color { get; set; }
        public override string ToString() => $@"
         Name:{Name} 
         ID:{ID}
         Price : {Price}
         Category: {Category}
         Color: {Color}";
    }
    
}
//○	מזהה(מוצר)
//○	שם מוצר
//○	מחיר מוצר
//○	קטגוריה
