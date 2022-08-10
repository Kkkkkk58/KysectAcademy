using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class Submit : BaseEntity
{
    public int StudentId { get; set; }
    public virtual Student StudentNavigation { get; set; }
    public DateTime? Date { get; set; } = null!;
    public int HomeWorkId { get; set; }
    public virtual HomeWork HomeWorkNavigation { get; set; }

    public virtual ICollection<ComparisonResult> AsSubmit1ComparisonResults { get; set; }
    public virtual ICollection<ComparisonResult> AsSubmit2ComparisonResults { get; set; }
}