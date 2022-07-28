using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

internal class Group : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Student> Students { get; set; } = null!;
}