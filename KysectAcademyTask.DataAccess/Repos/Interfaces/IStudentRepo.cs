using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface IStudentRepo : IRepo<Student>
{
    public IQueryable<Student> GetQueryWithProps(string firstName, string lastName, string groupName);
}