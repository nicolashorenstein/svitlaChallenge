namespace svitlaChallenge.Domain.Models;

public class Person
{
    public Guid Id { get; set; }
    public string GivenName { get; set; }
    public string SurName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? BirthLocation { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? DeathLocation { get; set; }

    // Navigation property for version history
    public virtual ICollection<PersonVersion> Versions { get; set; } = new List<PersonVersion>();
}