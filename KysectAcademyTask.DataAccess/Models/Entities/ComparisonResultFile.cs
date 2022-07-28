namespace KysectAcademyTask.DataAccess.Models.Entities;

public class ComparisonResultFile
{
    public int ComparisonResultId { get; set; }
    public ComparisonResult ComparisonResultNavigation { get; set; }
    public int FileId { get; set; }
    public FileEntity FileNavigation { get; set; }
}