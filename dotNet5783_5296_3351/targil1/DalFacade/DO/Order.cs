namespace DO;

public struct Order
{

    public int ID { get; set; }

    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddres { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public override string ToString() => $@"
ID: {ID}
Product CustomerName: {CustomerName}
CustomerEmail: {CustomerEmail}
CustomerAddres: {CustomerAddres}
OrderDate: {OrderDate}
ShipDate: {ShipDate}
DeliveryDate: {DeliveryDate}
";

}
