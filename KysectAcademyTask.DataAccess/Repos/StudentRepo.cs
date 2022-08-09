using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class StudentRepo : BaseRepo<Student>, IStudentRepo
{
    public StudentRepo(SubmitComparisonDbContext context) : base(context)
    {
    }

    public StudentRepo(DbContextOptions<SubmitComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<Student> GetQueryWithProps(string firstName, string lastName, string groupName)
    {
        return Table
            .Where(student => student.PersonalInformation.FirstName == firstName
                        && student.PersonalInformation.LastName == lastName
                        && student.GroupNavigation.Name == groupName);
    }
}