namespace KysectAcademyTask.DataAccess.Repos.Base;

public interface IRepo<T>: IDisposable
{
    int Add(T item, bool persist = true);
    int AddRange(IEnumerable<T> items, bool persist = true);
    int Update(T item, bool persist = true);
    int UpdateRange(IEnumerable<T> items, bool persist = true);
    int Delete(T item, bool persist = true);
    int DeleteRange(IEnumerable<T> items, bool persist = true);
    T Find(int id);
    T FindAsNoTracking(int id);
    IEnumerable<T> GetAll();
    void ExecuteSqlQuery(string sql, params object[] sqlParams);
    int SaveChanges();
}