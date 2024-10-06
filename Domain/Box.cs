using Newtonsoft.Json;

namespace Domain;

public class Box 
{
    public int Id { get; set; }
    [JsonIgnore]
    private string? Gtin { get; }
    [JsonIgnore]
    public string CodeWithoutId { get; set; }
    public string Code { get => $"{CodeWithoutId}{Id}"; set { } }
    public virtual List<Item>? Items { get; private set; }
    [JsonIgnore]
    public virtual Pallet? Pallet { get; set; }
    public int Capacity { get; }

    public Box() { }
    public Box(string gtin, int capacity)
    {
        Gtin = gtin;
        Items = [];
        CodeWithoutId = CreateCode();
        Capacity = capacity;
    }
    public bool IsFull()
    {
        return Items.Count == Capacity;
    }
    public void AddItem(Item item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item),
                "Layout generation failed: the item is null");
        }
        if (Items is null)
        {
            Items = [];
        }
        if (Items.Count < Capacity)
        {
            item.Box = this;
            Items.Add(item);
            CodeWithoutId = CreateCode();
        }
        else
        {
            throw new InvalidOperationException(
                "Cannot load item to a box: the box is full");
        }
    }
    private string CreateCode()
    {
        return $"01{Gtin}37{Items.Count}21";
    }
}
