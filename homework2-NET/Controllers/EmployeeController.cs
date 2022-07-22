using homework2_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace homework2_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public JsonResult Get()
        {
            string query = @"
                select * from public.employee
            ";
            List<Employee> employeeList = new List<Employee>();

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmpId = (int)myReader["empid"];
                        emp.EmpName = (string)myReader["empname"];
                        emp.DeptId = (int)myReader["deptid"];
                        employeeList.Add(emp);
                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(employeeList);
        }

        [HttpGet("GetBy{id:int}")]
        public JsonResult GetByIdAsync(int id)
        {
            string query = @"select * from public.employee where empid = " + id;

            Employee emp = new Employee();

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        emp.EmpId = (int)myReader["empid"];
                        emp.EmpName = (string)myReader["empname"];
                        emp.DeptId = (int)myReader["deptid"];

                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(emp);
        }

        [HttpGet("insert")]
        public JsonResult InsertCountry(string empid, string name, string deptid)
        {
            string emp_info = String.Format("'{0}','{1}','{2}')", empid, name, deptid);
            string query = @"insert into public.employee(empid,empname,deptid) values(" + emp_info;
            bool success = true;

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        myReader = command.ExecuteReader();

                        myReader.Close();
                        connection.Close();
                    }
                    catch (Exception)
                    {
                        success = false;
                    }

                }
            }
            if (success)
            {
                return new JsonResult("Employee added successfuly.");
            }
            else
            {
                return new JsonResult("Employee can't added.");
            }

        }

        [HttpGet("delete {id:int}")]
        public JsonResult DeleteById(int id)
        {
            string query = @"delete from public.employee where empid = " + id;
            bool success = true;

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        myReader = command.ExecuteReader();

                        myReader.Close();
                        connection.Close();
                    }
                    catch (Exception)
                    {
                        success = false;

                    }

                }
            }
            if (success)
            {
                return new JsonResult("Employee deleted successfuly.");
            }
            else
            {
                return new JsonResult("Employee can't deleted.");
            }
        }

        [HttpGet("EmployeeInfo")]
        public JsonResult EmployeesInfo(int id)
        {
            string query = "select public.employee.empname, public.country.countryname, public.department.deptname, public.folder.accesstype from public.employee " +
                               "inner join department on employee.deptid = department.departmentid " +
                               "inner join folder on employee.empid = folder.empid " +
                               "inner join country on department.countryid = country.countryid " +
                               "where employee.empid = " + id;

            EmpInfo emp_info = new EmpInfo();

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        emp_info.EmpName = (string)myReader["empname"];
                        emp_info.CountryName = (string)myReader["countryname"];
                        emp_info.DeptName = (string)myReader["deptname"];
                        emp_info.AccessType = (string)myReader["accesstype"];

                    }

                    myReader.Close();
                    connection.Close();
                }
            }

            

            return new JsonResult(emp_info);
        }

    }
}
