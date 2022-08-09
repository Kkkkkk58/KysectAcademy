using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class ComparisonResultRepo : BaseRepo<ComparisonResult>, IComparisonResultRepo
{
    public ComparisonResultRepo(SubmitComparisonDbContext context) : base(context)
    {
    }

    public ComparisonResultRepo(DbContextOptions<SubmitComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<ComparisonResult> GetQueryWithProps(Submit submit1, Submit submit2)
    {
        return Table
            .Where(result => result.Submit1Navigation == submit1 && result.Submit2Navigation == submit2
                                   || result.Submit1Navigation == submit2 && result.Submit2Navigation == submit1);
    }
}