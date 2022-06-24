using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Dapper;
using Dapper.Contrib.Extensions;

namespace GenericRepositoryDapper;

public class Repository<T>:IRepository<T> where T : class
{
    protected string TableName { get; set; }

    public TReturn QuerySingle<TReturn>(string storeProcedure, DynamicParameters dynamicParameters)
    {
        using var connection=CreateConnection();
        var result = connection.QuerySingle<TReturn>(storeProcedure, dynamicParameters,
            commandType: CommandType.StoredProcedure);

        return result ?? throw new KeyNotFoundException($"{TableName} could not be found.");
    }

    public IEnumerable<TReturn> Query<TReturn>(string storeProcedure, DynamicParameters dynamicParameters)
    {
        using var connection=CreateConnection();
        var result = connection.Query<TReturn>(storeProcedure, dynamicParameters,
            commandType: CommandType.StoredProcedure);

        return result ?? throw new KeyNotFoundException($"{TableName} could not be found.");
    }

    public int Execute(string storeProcedure, DynamicParameters dynamicParameters)
    {
        using var connection=CreateConnection();
        return connection.Execute(storeProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<T> GetAll()
    {
        using var connection = CreateConnection();
        return connection.GetAll<T>();
    }

    public T GetById<TParameter>(TParameter id) where TParameter : struct
    {
        using var connection = CreateConnection();
        return connection.Get<T>(id);
    }

    public IEnumerable<T> GetBy(Func<T, bool> predicate)
    {
        using var connection = CreateConnection();
        return connection.Query<T>($"SELECT * FROM {TableName}").Where(predicate);
    }

    public void Add(T entity)
    {
        using var connection = CreateConnection();
        connection.Insert(entity);
    }

    public void Update(T entity)
    {
        using var connection = CreateConnection();
        connection.Update(entity);
    }

    public void Delete(T entity)
    {
        using var connection = CreateConnection();
        connection.Delete(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = CreateConnection();
        return await connection.GetAllAsync<T>();
    }

    public async Task<T> GetByIdAsync<TParameter>(TParameter id) where TParameter : struct
    {
        using var connection = CreateConnection();
        return await connection.GetAsync<T>(id) ?? throw new KeyNotFoundException("${TableName} with id [{id}] could not be found.");
    }

    public async Task AddAsync(T entity)
    {
        using var connection = CreateConnection();
        await connection.InsertAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        using var connection = CreateConnection();
        await connection.UpdateAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        using var connection = CreateConnection();
        await  connection.DeleteAsync(entity);
    }
    
    protected string TableNameMapper(Type type)
    {
        dynamic tableAttribute = type.GetCustomAttributes(false)
            .SingleOrDefault(att => att.GetType().Name.Equals("TableAttribute"))!;
        var name = string.Empty;
        if (tableAttribute!=null)
        {
            name = tableAttribute.Name;
        }

        return name;
    }

    private static SqlConnection SqlConnection()
    {
        return new SqlConnection("Data Source=127.0.0.1; Initial Catalog=TestDb; User Id=sa; Password=115411Myvz!");
    }
    
    private static IDbConnection CreateConnection()
    {
        var connection = SqlConnection();
        connection.Open();
        return connection;
    }
    
    private static IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
}