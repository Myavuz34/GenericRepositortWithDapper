using Dapper.Contrib.Extensions;

namespace GenericRepositoryDapper;

/// <summary>
/// Class name can be different. Because we use Table Attribute
/// </summary>
[Table("Person")]
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }
}