using Dapper;

namespace GenericRepositoryDapper;

public interface IRepository<T> where T:class
{
    TReturn QuerySingle<TReturn>(string storeProcedure, DynamicParameters dynamicParameters);

    IEnumerable<TReturn> Query<TReturn>(string storeProcedure, DynamicParameters dynamicParameters);

    int Execute(string storeProcedure, DynamicParameters dynamicParameters);

    IEnumerable<T> GetAll();
    
    T GetById<TParameter>(TParameter id) where TParameter:struct ;

    IEnumerable<T> GetBy(Func<T, bool> predicate);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync<TParameter>(TParameter id) where TParameter : struct;

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

}