using svitlaChallenge.Domain.Models;

namespace svitlaChallenge.Application.Students.Commands.Students;

public class AddPersonCommand
{
    public string GivenName { get; set; }
    public string SurName { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string? BirthLocation { get; set; }
    public DateTime? DeathDate  { get; set; }
    public string? DeathLocation  { get; set; }
}