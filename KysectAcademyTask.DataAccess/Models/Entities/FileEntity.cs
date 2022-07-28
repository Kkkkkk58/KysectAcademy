using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

internal class FileEntity : BaseEntity
{
    public string Path { get; set; }
    public int SubmitId { get; set; }
    public Submit SubmitNavigation { get; set; }
    public ICollection<ComparisonResultFile> ComparisonResultFiles { get; set; } = null!;
}