using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

internal class Submit : BaseEntity
{
    public int StudentId { get; set; }
    public Student StudentNavigation { get; set; }
    public DateTime? Date { get; set; } = null!;
    public string Homework { get; set; }
    public virtual ICollection<FileEntity> Files { get; set; } = null!;
}