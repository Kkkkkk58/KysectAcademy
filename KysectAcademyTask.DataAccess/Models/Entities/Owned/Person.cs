using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Models.Entities.Owned;

[Owned]
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; } = null!;
}