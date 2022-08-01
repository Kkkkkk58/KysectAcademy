using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class StudentRepo : BaseRepo<Student>, IStudentRepo
{
    public StudentRepo(FileComparisonDbContext context) : base(context)
    {
    }

    public StudentRepo(DbContextOptions<FileComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<Student> GetQueryWithProps(string firstName, string lastName, string groupName)
    {
        return Table?
            .Where(s => s.PersonalInformation.FirstName == firstName
                        && s.PersonalInformation.LastName == lastName
                        && s.GroupNavigation.Name == groupName);
    }
}