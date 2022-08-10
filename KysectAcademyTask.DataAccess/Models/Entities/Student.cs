using KysectAcademyTask.DataAccess.Models.Entities.Base;
using KysectAcademyTask.DataAccess.Models.Entities.Owned;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class Student : BaseEntity
{
    public Person PersonalInformation { get; set; }
    public virtual Group GroupNavigation { get; set; }
    public int GroupId { get; set; }
    public virtual ICollection<Submit> Submits { get; set; } = null!;
}