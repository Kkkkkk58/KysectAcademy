using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class HomeWorkRepo: BaseRepo<HomeWork>, IHomeWorkRepo
{
    protected HomeWorkRepo(FileComparisonDbContext context) : base(context)
    {
    }

    protected HomeWorkRepo(DbContextOptions<FileComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<HomeWork> GetQueryWithProps(string name)
    {
        return Table
            .Where(h => h.Name == name);
    }
}