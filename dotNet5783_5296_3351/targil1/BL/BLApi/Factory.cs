//using BlApi;
//using BlImplementation;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLApi
//{
//    public static class Factory
//    {
//        public static IBl Get()
//        {
//            Bl b = new Bl();
//            return b;
//        }
//    }
//}
namespace BlApi;

using BlImplementation;
using System.Reflection;


public static class Factory
{
    public static IBl? Get()
    {
        IBl b = new Bl();
        return b;
    }
}

