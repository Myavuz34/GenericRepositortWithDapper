namespace GenericRepositoryDapper;

public class EmployeeRepository:Repository<Employee>,IEmployeeRepository
{
    public EmployeeRepository()
    {
        TableName = TableNameMapper(typeof(Employee));
    }
}