using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class SubmitRepo : BaseRepo<Submit>, ISubmitRepo
{
    public SubmitRepo(FileComparisonDbContext context) : base(context)
    {
    }

    public SubmitRepo(DbContextOptions<FileComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<Submit> GetQueryWithProps(string authorFullName, string groupName, string homeWorkName, DateTime? submitDate)
    {
        return Table
            .Where(s => s.StudentNavigation.PersonalInformation.FullName == authorFullName
                        && s.StudentNavigation.GroupNavigation.Name == groupName
                        && s.HomeWorkNavigation.Name == homeWorkName
                        && s.Date == submitDate);
    }
}