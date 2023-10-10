using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string ?CustomerAddres { get; set; }
        //public List<ItemInOrder>? Items = new List<BO.ItemInOrder>();{ get; set; }
        public List<ItemInOrder>? Items = new List<BO.ItemInOrder>();

        public List<ItemInOrder>? items
            {
            get { return Items; }
            set { Items = value; }
        }

    public double CartTotal { get; set; }
        
    
    public override string ToString()
    {
            NotFoundExeption ex = new NotFoundExeption();
            string tostring = "CustomerName: " + CustomerName + "\n CustomerEmail: " + CustomerEmail + "\n CustomerAddres: " + CustomerAddres + "\n CartTotal: " + CartTotal;
            int i = 0;
            foreach (var item in Items ?? throw new NullExeption(ex))
            {
                tostring += "\n item[" + (i + 1) + "]:" + item + "\n";
            }
        return tostring;
    }
  }
}


  
//○	שם קונה
//○	כתובת דוא"ל (מייל) של קונה
//○	כתובת של קונה
//○	רשימת פרטי הזמנה (מסוג OrderItem)
//○	מחיר כולל של סל הזמנה
