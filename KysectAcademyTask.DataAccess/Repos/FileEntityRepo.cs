using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class FileEntityRepo : BaseRepo<FileEntity>, IFileEntityRepo
{
    public FileEntityRepo(FileComparisonDbContext context) : base(context)
    {
    }

    public FileEntityRepo(DbContextOptions<FileComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<FileEntity> GetQueryWithProps(string path)
    {
        return Table
            .Where(f => f.Path == path);
    }
}