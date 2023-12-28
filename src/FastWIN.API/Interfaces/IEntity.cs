namespace fastwin.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
