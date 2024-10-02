using Newtonsoft.Json;

namespace Domain;

public class Pallet
{
    public int Id { get; set; }
    [JsonIgnore]
    public string Gtin { get; }
    [JsonIgnore]
    public string CodeWithoutId { get; set; }
    public string Code { get => $"{CodeWithoutId}{Id}"; set { } }
    public virtual List<Box>? Boxes { get; private set; }
    public int Capacity { get; }
    public Pallet() { }

    public Pallet(string gtin, int capacity)
    {
        Gtin = gtin;
        Boxes = [];
        CodeWithoutId = CreateCode();
        Capacity = capacity;
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
            CodeWithoutId = CreateCode();
        }
    }
    public void AddItemToLastBox(Item item)
    {
        Box box = Boxes.Last();
        box.AddItem(item);
    }
    private string CreateCode()
    {
        return $"01{Gtin}37{Boxes.Count}21";
    }

}
