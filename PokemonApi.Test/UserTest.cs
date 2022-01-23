//using Employees.Core.Dto;
//using Employees.Core.Enum;
//using Employees.Core.Intefaces;
//using Employees.Infrastructure.Services;
using Xunit;

namespace PokemonApi.Test
{
    public class UserTest
    {
        //IEmployeeFactory EmployeeFactory;

        public UserTest()
        {
            //EmployeeFactory = new EmployeeFactory();
        }

        [Fact]
        public void ShouldCalculateAnnualSalaryForHourlyContract()
        {
            //var employeeDto = new EmployeeDto { contractTypeName = ContractType.Hourly, hourlySalary = 300 };
            //var employeeAnaualSalary = 120 * employeeDto.hourlySalary * 12;

            ////act
            //var employee = EmployeeFactory.GetEmployee(employeeDto);


            ////assert
            //Assert.Equal(employeeAnaualSalary, employee.AnnualSalary);

        }

        [Fact]
        public void ShouldCalculateAnnualSalaryForMonthlyContract()
        {
            //var employeeDto = new EmployeeDto { contractTypeName = ContractType.Monthly, monthlySalary = 20000 };
            //var employeeAnaualSalary = employeeDto.monthlySalary * 12;

            ////act
            //var employee = EmployeeFactory.GetEmployee(employeeDto);

            ////assert
            //Assert.Equal(employeeAnaualSalary, employee.AnnualSalary);
        }
    }
}
