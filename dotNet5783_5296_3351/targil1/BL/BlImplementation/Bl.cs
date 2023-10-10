using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

sealed public class Bl : IBl
{
    public ICart Cart => new BlCart();
    public IItem Item => new BlItem();
    public IOrder Order => new BlOrder();

}
