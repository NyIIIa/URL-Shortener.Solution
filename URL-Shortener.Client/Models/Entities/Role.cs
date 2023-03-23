namespace URL_Shortener.Client.Models.Entities;

public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<User>? Users { get; set; }
}