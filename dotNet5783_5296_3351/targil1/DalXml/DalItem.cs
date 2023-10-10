using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class DalItem : IItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Item item)
    {
        List<Item> lst = GetAll().ToList();//לוקח מהקובץ ושם ברשימה
        lst.Add(item);
        StreamWriter w = new StreamWriter("../Product.xml");
        XmlSerializer sery = new XmlSerializer(typeof(List<Item>));
        sery.Serialize(w, lst);//לוקח מהרשימה ושם בקובץ
            w.Close();
        return 0;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Item Get(int itemid)
    {
        List<DO.Item> lst =GetAll().ToList();
        return lst.Find(x => x.ID == itemid);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Item Get(Predicate<DO.Item> function)
    {
        List<DO.Item> lst = GetAll().ToList();
        Func<DO.Item, bool> func = new Func<DO.Item, bool>(function);
        return lst.Where(func).FirstOrDefault();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Item> GetAll(Func<DO.Item, bool>? function = null)
    {
        StreamReader r = new StreamReader("../Product.xml");
        XmlSerializer sery = new XmlSerializer(typeof(List<Item>));
        List<DO.Item> lst = new List<DO.Item> { };
        lst = (List<Item>)sery.Deserialize(r);
        r.Close();
        return function == null ? lst : lst.Where(function);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<DO.Item> lst = GetAll().ToList();
        Item item = lst.Find(x => x.ID == id); 
        lst.Remove(item);
        StreamWriter w = new StreamWriter("../Product.xml");
        XmlSerializer sery = new XmlSerializer(typeof(List<Item>));
        sery.Serialize(w, lst);//לוקח מהרשימה ושם בקובץ
        w.Close();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Item myitem)
    {
        Delete(myitem.ID);
        Add(myitem);
    }
   
}
