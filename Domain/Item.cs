namespace Domain;

public class Item
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public virtual Box? Box { get; set; }

    
}
