using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class SubmitRepo : BaseRepo<Submit>, ISubmitRepo
{
    public SubmitRepo(SubmitComparisonDbContext context) : base(context)
    {
    }

    public SubmitRepo(DbContextOptions<SubmitComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<Submit> GetQueryWithProps(string authorFullName, string groupName, string homeWorkName,
        DateTime? submitDate)
    {
        return Table
            .Where(submit => submit.StudentNavigation.PersonalInformation.FullName == authorFullName
                             && submit.StudentNavigation.GroupNavigation.Name == groupName
                             && submit.HomeWorkNavigation.Name == homeWorkName
                             && submit.Date == submitDate);
    }
}