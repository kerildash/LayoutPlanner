namespace Domain.Info;

public class Mission
{
    public int Id { get; set; }
    public DateTime DateAt { get; set; }
    public int CodeTypeId { get; set; }
    public Package Package { get; set; }
    public Product Product { get; set; }
}
