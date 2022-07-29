using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class ComparisonResultRepo : BaseRepo<ComparisonResult>, IComparisonResultRepo
{
    public ComparisonResultRepo(FileComparisonDbContext context) : base(context)
    {
    }

    public ComparisonResultRepo(DbContextOptions<FileComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<ComparisonResult> GetResultOfFilesWithMetricsQuery(string fileName1, string fileName2, string metrics)
    {
        return Table
            .Where(c => (c.FileName1 == fileName1 && c.FileName2 == fileName2
                            || c.FileName1 == fileName2 && c.FileName2 == fileName1)
                        && c.Metrics == metrics);
    }
}