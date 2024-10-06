using Newtonsoft.Json;

namespace Domain;

public class Item
{
    public int Id { get; set; }
    public string Code { get; set; }
    [JsonIgnore]
    public Box? Box { get; set; }
    public Item() { }
}
