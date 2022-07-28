using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

internal class ComparisonResult : BaseEntity
{
    public string FileName1 { get; set; }
    public string FileName2 { get; set; }
    public string Metrics { get; set; }
    public double SimilarityRate { get; set; }

    public ICollection<ComparisonResultFile> ComparisonResultFiles { get; set; }
}