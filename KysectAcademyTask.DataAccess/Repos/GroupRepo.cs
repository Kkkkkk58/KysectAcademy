using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class GroupRepo : BaseRepo<Group>, IGroupRepo
{
    public GroupRepo(SubmitComparisonDbContext context) : base(context)
    {
    }

    public GroupRepo(DbContextOptions<SubmitComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<Group> GetQueryWithProps(string name)
    {
        return Table
            .Where(g => g.Name == name);
    }
}