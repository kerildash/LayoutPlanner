namespace Domain;

public class Box
{
    public Guid Id { get; }
    public string Code { get; }
    public virtual List<Item>? Items { get; private set; }
    public virtual Pallet? Pallet { get; }
    public int Capacity { get; }

    public Box(string gtin, int capacity)
    {
        Id = Guid.NewGuid();
        Code = CreateCode(gtin, capacity);
        Capacity = capacity;
    }
    public void AddItem(Item item)
    {
        if (Items.Count < Capacity)
        {
            Items.Add(item);
        }
        else
        {
            throw new InvalidOperationException("Cannot load item to a box: the box is full");
        }
    }
    private string CreateCode(string gtin, int capacity)
    {
        return $"01{gtin}37{capacity}21{Id}";
    }
    
}
