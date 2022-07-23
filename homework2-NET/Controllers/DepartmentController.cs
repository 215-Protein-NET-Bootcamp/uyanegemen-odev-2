using Microsoft.AspNetCore.Mvc;
using homework2_NET.Models;
using Npgsql;

namespace homework2_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public JsonResult Get()
        {
            string query = @"
                select * from public.department
            ";
            List<Department> departmentList = new List<Department>();

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
                        Department department = new Department();
                        department.DepartmentId = (int)myReader["departmentid"];
                        department.DeptName = (string)myReader["deptname"];
                        department.CountryId = (int)myReader["countryid"];
                        departmentList.Add(department);
                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(departmentList);
        }

        [HttpGet("GetBy{id:int}")]
        public JsonResult GetByIdAsync(int id)
        {
            string query = @"select * from public.department where departmentid = " + id;

            Department department = new Department();

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
                        department.DepartmentId = (int)myReader["departmentid"];
                        department.DeptName = (string)myReader["deptname"];
                        department.CountryId = (int)myReader["countryid"];

                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(department);
        }

        [HttpPost("insert")]
        public JsonResult InsertCountry(string depid, string name, string cnid)
        {
            string dept_info = String.Format("'{0}','{1}','{2}')", depid, name, cnid);
            string query = @"insert into public.department(departmentid,deptname,countryid) values(" + dept_info;
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
                return new JsonResult("Department added successfuly.");
            }
            else
            {
                return new JsonResult("Department can't added.");
            }

        }

        [HttpDelete("delete {id:int}")]
        public JsonResult DeleteById(int id)
        {
            string query = @"delete from public.department where departmentid = " + id;
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
                return new JsonResult("Department deleted successfuly.");
            }
            else
            {
                return new JsonResult("Department can't deleted.");
            }
        }

    }
}
