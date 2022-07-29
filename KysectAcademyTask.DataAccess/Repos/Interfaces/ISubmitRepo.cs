using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface ISubmitRepo: IRepo<Submit>
{
    public IQueryable<Submit> GetQueryWithProps(string authorFullName, string groupName, string homeWorkName, DateTime? submitDate);
}