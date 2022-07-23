using homework2_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace homework2_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FolderController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public FolderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public JsonResult Get()
        {
            string query = @"
                select * from public.folder
            ";
            List<Folder> folderList = new List<Folder>();

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
                        Folder folder = new Folder();
                        folder.FolderId = (int)myReader["folderid"];
                        folder.EmpId = (int)myReader["empid"];
                        folder.AccessType = (string)myReader["accesstype"];
                        folderList.Add(folder);
                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(folderList);
        }

        [HttpGet("GetBy{id:int}")]
        public JsonResult GetByIdAsync(int id)
        {
            string query = @"select * from public.folder where folderid = " + id;

            Folder folder = new Folder();

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
                        folder.FolderId = (int)myReader["folderid"];
                        folder.EmpId = (int)myReader["empid"];
                        folder.AccessType = (string)myReader["accesstype"];

                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(folder);
        }

        [HttpPost("insert")]
        public JsonResult InsertCountry(string folderid, string empid, string accesstype)
        {
            string folder_info = String.Format("'{0}','{1}','{2}')", folderid, empid, accesstype);
            string query = @"insert into public.folder(folderid,empid,accesstype) values(" + folder_info;
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
                return new JsonResult("Folder added successfuly.");
            }
            else
            {
                return new JsonResult("Folder can't added.");
            }

        }

        [HttpDelete("delete {id:int}")]
        public JsonResult DeleteById(int id)
        {
            string query = @"delete from public.folder where folderid = " + id;
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
                return new JsonResult("Folder deleted successfuly.");
            }
            else
            {
                return new JsonResult("Folder can't deleted.");
            }
        }

    }
}
