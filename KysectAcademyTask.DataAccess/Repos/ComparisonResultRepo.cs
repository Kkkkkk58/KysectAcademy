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

    public IQueryable<ComparisonResult> GetQueryWithProps(string fileName1, string fileName2, string metrics)
    {
        return Table
            .Include(c => c.File1Navigation)
            .Include(c => c.File2Navigation)
            .Where(c => (c.File1Navigation.Path == fileName1 && c.File2Navigation.Path == fileName2
                            || c.File1Navigation.Path == fileName2 && c.File2Navigation.Path == fileName1)
                        && c.Metrics == metrics);
    }

}