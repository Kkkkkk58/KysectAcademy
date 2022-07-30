using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface IGroupRepo : IRepo<Group>
{
    public IQueryable<Group> GetQueryWithProps(string name);
}