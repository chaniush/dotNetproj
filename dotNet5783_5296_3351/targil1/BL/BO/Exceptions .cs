using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

//internal class Exceptions
//{
public class DalException : Exception
{
    public DalException(Exception ex) : base(" exception in dal", ex) { }
}

public class inValidException : Exception
{
    public override string Message => "invalid exeption";
}
public class ItemInOrderException : Exception
{
    public ItemInOrderException(Exception ex) : base(" Item In Order Exception", ex) { }
}
public class DateException : Exception
{
    public override string Message => "date exeption";
}
public class OutOfStockException : Exception
{
    public override string Message => "out of stock exeption";
}
public class NotFoundException : Exception
{
    public NotFoundException(Exception ex) : base(" not found  Exception", ex) { }
}
public class ObjectDoesntExistException : Exception
{
    public ObjectDoesntExistException(Exception ex) : base(" object doesnt exist  Exception", ex) { }
}
public class NullExeption : Exception
{
    public NullExeption(Exception ex) : base(" object is not this type", ex) { }
}
//}
