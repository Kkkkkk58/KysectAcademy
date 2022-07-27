using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Models.Entities.Owned;

[Owned]
internal class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; } = null!;
}