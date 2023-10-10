using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace DalApi
{
   public interface ICrud<T>
    {
      
        public int Add(T t);
        public void Delete(int id);
        public void Update(T t);
        public T Get(int id);
        public T Get(Predicate<T> function);
        public IEnumerable<T> GetAll(Func<T, bool>? function = null);//Func<T?,bool?> b
    }
}
