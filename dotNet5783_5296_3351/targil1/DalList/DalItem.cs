//using static Dal.DataSource;

using DO;
using System;
using static Dal.DataSource;

namespace Dal { 

public class DalItem : DalApi.IItem
{
    public  bool DoesItemIDExsists(int id)
    {
            DO.Item item = (from Item in DataSource.items where Item.ID == id select Item).FirstOrDefault();
            
            if (item.ID != 0)
            {
                return true;
            }

        
        return false;
            
    }
    public int Add(DO.Item item)
    {
            if (DoesItemIDExsists(item.ID))
            {
                throw new Exception("id exists");
            }
            else
            {
                DataSource.items.Add(item);
            }
            return item.ID;
    }
    public void output()
    {
        int i = items.Count;
        Console.WriteLine(items[i].ID);
        items[i].ID.ToString();

    }
    public void create(DO.Item item)
    {

        try
        {
            Add(item);
        }
        catch(Exception)
        {
            Console.WriteLine("item exists in dalitem create");
        }
        return;


    }
    public DO.Item Get(int itemid)
    {
        DO.Item item = DataSource.items.Find(Item => Item.ID == itemid);
        if (item.ID != 0)
        {
            return item;
        }
       
         else
         {
             throw new Exception("item does not exist");
         }
    }
    public DO.Item Get(Predicate<DO.Item> function)
    {
        return items.Find(function);
    }
    public IEnumerable<DO.Item> GetAll(Func<DO.Item, bool>? function=null )
    {
       
        DO.Item[] t = new DO.Item[items.Count];
          
            for (int i = 0; i < items.Count; i++)
        {
            t[i] = DataSource.items[i];
        }
        return function==null?t:t.Where(function);
    }


    public  void Update(DO.Item myitem)
    {
           
            for (int i = 0; i < items.Count; i++)
            {
                if (DataSource.items[i].ID == myitem.ID)
                {
                    DataSource.items[i] = myitem;
                    break;
                }
            }
        }
       
    public void Delete(int id)
    {
            DO.Item item = DataSource.items.Find(Item => Item.ID == id);
            if (item.ID != 0)
            {
                items.Remove(item);
            }
            else
            {
                throw new Exception("item is not found");
            }
        }
    }
}
