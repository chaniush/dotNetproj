namespace DO{

public struct Item
{
    public string Name { get; set; }
    public int ID { get;  set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }
    public Color Color { get; set; }
        
    

    public override string ToString() => $@"
     Product ID:{ID} 
     Name:{Name}
     category : {Category}
     Price: {Price}
     Amount in stock: {InStock}
     Color: {Color}";
}
    }
