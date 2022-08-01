using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos.Base;

public class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
{
    public FileComparisonDbContext Context { get; }
    public DbSet<T> Table { get; }
    private readonly bool _disposeContext;
    private bool _isDisposed;

    protected BaseRepo(FileComparisonDbContext context)
    {
        Context = context;
        Table = Context?.Set<T>();
        _disposeContext = false;
        _isDisposed = false;
    }

    protected BaseRepo(DbContextOptions<FileComparisonDbContext> options)
        : this(new FileComparisonDbContext(options))
    {
        _disposeContext = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing && _disposeContext)
        {
            Context.Dispose();
        }

        _isDisposed = true;
    }

    ~BaseRepo()
    {
        Dispose(false);
    }

    public virtual int Add(T item, bool persist = true)
    {
        Table.Add(item);
        return GetNumberOfChangedFields(persist);
    }

    public virtual int AddRange(IEnumerable<T> items, bool persist = true)
    {
        Table.AddRange(items);
        return GetNumberOfChangedFields(persist);
    }

    public virtual int Update(T item, bool persist = true)
    {
        Table.Update(item);
        return GetNumberOfChangedFields(persist);
    }

    public virtual int UpdateRange(IEnumerable<T> items, bool persist = true)
    {
        Table.UpdateRange(items);
        return GetNumberOfChangedFields(persist);
    }

    public virtual int Delete(T item, bool persist = true)
    {
        Table.Remove(item);
        return GetNumberOfChangedFields(persist);
    }

    public virtual int DeleteRange(IEnumerable<T> items, bool persist = true)
    {
        Table.RemoveRange(items);
        return GetNumberOfChangedFields(persist);
    }

    public virtual T Find(int id)
    {
        return Table.Find(id);
    }

    public virtual T FindAsNoTracking(int id)
    {
        return Table
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefault(x => x.Id == id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Table;
    }

    public virtual void ExecuteSqlQuery(string sql, params object[] sqlParams)
    {
        Context.Database.ExecuteSqlRaw(sql, sqlParams);
    }

    public virtual int SaveChanges()
    {
        return Context.SaveChanges();
    }

    private int GetNumberOfChangedFields(bool persist)
    {
        return persist ? SaveChanges() : 0;
    }
}