using System.ComponentModel.DataAnnotations;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class FileEntity
{
    public string Path { get; set; }
    public int SubmitId { get; set; }
    public Submit SubmitNavigation { get; set; }

    [Timestamp]
    public byte[] TimeStamp { get; set; } = null!;

    public ICollection<ComparisonResult> AsFile1ComparisonResults { get; set; }
    public ICollection<ComparisonResult> AsFile2ComparisonResults { get; set; }
}