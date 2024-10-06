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
        return Boxes != null && Boxes.Count == Capacity && Boxes.Last().IsFull();
    }
    public void AddItemToPallet(Item item, string gtin, int boxFormat)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item),
                "Layout generation failed: the item is null");
        }
        if (Boxes is null)
        {
            Boxes = [];
        }
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
    private string CreateCode()
    {
        return $"01{Gtin}37{Boxes.Count}21";
    }

}
