namespace svitlaChallenge.Domain.Models;

public class PersonVersion
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string GivenName { get; set; }
    public string SurName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? BirthLocation { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? DeathLocation { get; set; }

    public DateTime UpdatedAt { get; set; } // Timestamp for when this version was created

    // Navigation property back to the Person
    public virtual Person Person { get; set; }
}