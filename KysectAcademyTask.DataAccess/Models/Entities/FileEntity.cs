using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class FileEntity : BaseEntity
{
    public string Path { get; set; }
    public int SubmitId { get; set; }
    public Submit SubmitNavigation { get; set; }

    public ICollection<ComparisonResult> AsFile1ComparisonResults { get; set; }
    public ICollection<ComparisonResult> AsFile2ComparisonResults { get; set; }
}