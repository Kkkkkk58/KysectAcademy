using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface IComparisonResultRepo : IRepo<ComparisonResult>
{
    public bool ContainsComparisonResultOfFilesWithMetrics(string fileName1, string fileName2, string metrics);

    public ComparisonResult GetComparisonResultOfFilesWithMetrics(string fileName1, string fileName2, string metrics);
}