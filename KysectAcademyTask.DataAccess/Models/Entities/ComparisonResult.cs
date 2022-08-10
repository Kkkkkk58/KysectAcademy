using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class ComparisonResult : BaseEntity
{
    public int Submit1Id { get; set; }
    public virtual Submit Submit1Navigation { get; set; }

    public int Submit2Id { get; set; }
    public virtual Submit Submit2Navigation { get; set; }

    public double SimilarityRate { get; set; }
}