using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

internal class ComparisonResult : BaseEntity
{
    public string FileName1 { get; set; }
    public string FileName2 { get; set; }

    public ICollection<FileEntity> Files { get; set; } = null!;

    public string Metrics { get; set; }
    public double SimilarityRate { get; set; }
}