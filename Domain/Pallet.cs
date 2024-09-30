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
        Boxes = [];
    }
    public bool IsFull()
    {
        return Boxes.Any() && Boxes.Count == Capacity && Boxes.Last().IsFull();
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
    public void AddItemToPallet(Item item, string gtin, int boxFormat)
    {
        if (Boxes.Any() && !Boxes.Last().IsFull())
        {
            Boxes.Last().AddItem(item);
        }
        else
        {
            Box newBox = new Box(gtin, boxFormat);
            newBox.Pallet = this;
            newBox.AddItem(item);
            Boxes.Add(newBox);
        }
    }
    public void AddItemToLastBox(Item item)
    {
        Box box = Boxes.Last();
        box.AddItem(item);
    }
    private string CreateCode(string gtin, int capacity)
    {
        return $"01{gtin}37{capacity}21{Id}";
    }

}
