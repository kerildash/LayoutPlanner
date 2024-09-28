namespace Domain;

public class Pallet
{
    public Guid Id { get; }
    public string Code { get; }
    public virtual List<Box>? Boxes { get; private set; }
    public int Capacity { get; }

    public Pallet(string gtin, int capacity)
    {
        Id = Guid.NewGuid();
        Code = CreateCode(gtin, capacity);
        Capacity = capacity;
    }
    public void AddBox(Box box)
    {
        if (Boxes.Count < Capacity)
        {
            Boxes.Add(box);
        }
        else
        {
            throw new InvalidOperationException("Cannot load the box to the pallet: this pallet is full");
        }
    }
    private string CreateCode(string gtin, int capacity)
    {
        return $"01{gtin}37{capacity}21{Id}";
    }

}
