namespace Domain.Entities;

public abstract class BaseEntity
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
