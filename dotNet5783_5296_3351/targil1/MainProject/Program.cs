
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Program
{
    public static void Main(string[] args)
    {
        List<DO.Item> items = new List<DO.Item>();
        items.Add(new DO.Item
        {
            ID = 605199106,
            Name = "white gold hart pendant",
            Price = 5508,
            Category = DO.Category.NECKLACE,
            InStock = 3,
            Color = DO.Color.WHITE_GOLD
        });
        items.Add(new DO.Item
        {
            ID = 683182986,
            Name = "hart gold set",
            Price = 14715,
            Category = DO.Category.SETS,
            InStock = 5,
            Color = DO.Color.GOLD
        });
        items.Add(new DO.Item
        {
            ID = 726649871,
            Name = "solitaire lab diamond jewelry set",
            Price = 18795,
            Category = DO.Category.SETS,
            InStock = 2,
            Color = DO.Color.DIAMONDS
        });
        items.Add(new DO.Item
        {
            ID = 956248229,
            Name = "Custom Diamond Engagement Ring",
            Price = 6908,
            Category = DO.Category.RINGS,
            InStock = 14,
            Color = DO.Color.DIAMONDS
        });
        items.Add(new DO.Item
        {
            ID = 685547681,
            Name = "mens ring 925 sterling silver amber stone",
            Price = 2380,
            Category = DO.Category.RINGS,
            InStock = 10,
            Color = DO.Color.SILVER
        });
        items.Add(new DO.Item
        {
            ID = 219681168,
            Name = "PRETTY BRACELET",
            Price = 345,
            Category = DO.Category.BRACELET,
            InStock = 2,
            Color = DO.Color.WHITE_GOLD
        });
        items.Add(new DO.Item
        {
            ID = 232527337,
            Name = "Brilliant Earth White Gold Four-prong Round Diamond Stud Earrings",
            Price = 98765,
            Category = DO.Category.EARRINGS,
            InStock = 14,
            Color = DO.Color.WHITE_GOLD
        });
        items.Add(new DO.Item
        {
            ID = 681169515,
            Name = "lovely rosy necklace",
            Price = 444,
            Category = DO.Category.NECKLACE,
            InStock = 4,
            Color = DO.Color.ROSE_GOLD
        });
        items.Add(new DO.Item
        {
            ID = 622115947,
            Name = "silver oxidised Jhumki earing with mirror detailing",
            Price = 8040,
            Category = DO.Category.EARRINGS,
            InStock = 2,
            Color = DO.Color.SILVER
        });
        
        items.Add(new DO.Item
        {
            ID = 211066752,
            Name = "threads-diamond-necklace",
            Price = 330,
            Category = DO.Category.BRACELET,
            InStock = 9,
            Color = DO.Color.ROSE_GOLD
        });
        items.Add(new DO.Item
        {
            ID = 920742164,
            Name = "chain in rose gold",
            Price = 7410,
            Category = DO.Category.NECKLACE,
            InStock = 3,
            Color = DO.Color.DIAMONDS
        });
        items.Add(new DO.Item
        {
            ID = 719761430,
            Name = "gold link bracelet",
            Price = 240,
            Category = DO.Category.BRACELET,
            InStock = 10,
            Color = DO.Color.GOLD
        });
        XElement? Products = XDocument.Load("../Product.xml").Root;
        foreach (var item in items)
        {
            XElement? product = new XElement("Product");
            XElement? id = new XElement("id", item.ID);
            product.Add(id);
            XElement? name = new XElement("name", item.Name);
            product.Add(name);
            XElement? price = new XElement("price", item.Price);
            product.Add(price);
            XElement? category = new XElement("category", item.Category);
            product.Add(category);
            XElement? inStock = new XElement("inStock", item.InStock);
            product.Add(inStock);
            XElement? color = new XElement("color", item.Color);
            product.Add(color);
            Products?.Add(product);
            Products?.Save("../Product.xml");
        }
           
        
    }
}