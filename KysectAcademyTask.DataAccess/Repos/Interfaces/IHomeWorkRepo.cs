using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface IHomeWorkRepo: IRepo<HomeWork>
{
    public IQueryable<HomeWork> GetQueryWithProps(string name);
}