
using System.Security.Cryptography;
using Dal;
using System;
using DalApi;
namespace Dal;

// צריך לממש
sealed internal class DalList : DalApi.IDal
{
    public DalApi.IItem Item => new Dal.DalItem();

    public DalApi.IOrder Order => new Dal.DalOrder();

    public DalApi.IItemInOrder ItemInOrder => new Dal.DalItemInOrder();

    public static IDal Instance { get; } = new DalList();
    private DalList()
    {

    }
}



