// See https://aka.ms/new-console-template for more information

using GenericRepositoryDapper;

Console.WriteLine("Hello, World!");

var employeeRepository=new  EmployeeRepository();

// employeeRepository.Add(new Employee{Name = "Tuğba",Surname = "Yavuz"});
await employeeRepository.AddAsync(new Employee{Name = "Ali",Surname = "Yavuz"});

var employee = employeeRepository.GetById(4);
employee.Name = "Musti";
employeeRepository.Update(employee);
employee.Surname = "Yavuzz";
await employeeRepository.UpdateAsync(employee);

var emp = await employeeRepository.GetByIdAsync(5);
emp.Name = "Tubik";
await employeeRepository.UpdateAsync(emp);
