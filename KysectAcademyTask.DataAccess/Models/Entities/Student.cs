using KysectAcademyTask.DataAccess.Models.Entities.Base;
using KysectAcademyTask.DataAccess.Models.Entities.Owned;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class Student : BaseEntity
{
    public Person PersonalInformation { get; set; }
    public Group GroupNavigation { get; set; }
    public int GroupId { get; set; }
    public ICollection<Submit> Submits { get; set; } = null!;
}