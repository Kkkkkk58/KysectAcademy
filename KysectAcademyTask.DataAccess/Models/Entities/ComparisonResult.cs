using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class ComparisonResult : BaseEntity
{
    public int File1Id { get; set; }
    public FileEntity File1Navigation { get; set; }

    public int File2Id { get; set; }
    public FileEntity File2Navigation { get; set; }

    public string Metrics { get; set; }
    public double SimilarityRate { get; set; }

   
}