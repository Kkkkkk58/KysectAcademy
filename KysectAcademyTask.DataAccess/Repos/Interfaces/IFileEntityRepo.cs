using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface IFileEntityRepo: IRepo<FileEntity>
{
    public IQueryable<FileEntity> GetQueryWithProps(string path);
}