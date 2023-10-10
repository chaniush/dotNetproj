using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IItem
{
    public IEnumerable<ItemForList> ProductListRequest(Func<DO.Item, bool>? function = null);//gets all the products list
    public Item ProductDetailsRequestM(int id);//gets a product by id
    public List<ProductItem> ProductDetailsRequestC();//gets all details of item

    public void Add(Item item);//adds an item
    public void Delete(int id);//deletes item by id
    public void Update(Item item);//updates item
    //public IEnumerable<ItemForList> GetByCategory(Category category);
}
