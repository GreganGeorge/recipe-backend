using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FavouriteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult Get(int login_id)
        {
            string query = "select recipe.recipe_id,recipe.recipe_name,recipe.recipe_image from favourites,recipe where favourites.recipe_id=recipe.recipe_id and login_id=@login_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@login_id", login_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Route("Get1")]
        [HttpGet]
        public ActionResult Get1(int recipe_id)
        {
            var result = new { Param1 = recipe_id };
            string query = "select recipe_id,recipe_name,recipe_image from recipe where recipe_id like @recipe_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@recipe_id", recipe_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Route("Get2")]
        [HttpGet]
        public ActionResult Get2(int recipe_id,int login_id)
        {
            var result = new { Param1 = recipe_id };
            string query = "select * from favourites where recipe_id=@recipe_id and login_id=@login_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@recipe_id", recipe_id);
                    myCommand.Parameters.AddWithValue("@login_id", login_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(int login_id, int recipe_id)
        {
            string query = @"insert into favourites values(@login_id,@recipe_id)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@login_id", login_id);
                    myCommand.Parameters.AddWithValue("@recipe_id", recipe_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added to Favourites");
        }
        [HttpDelete]
        public JsonResult Delete(int recipe_id,int login_id)
        {
            string query = @"delete from favourites where recipe_id=@recipe_id and login_id=@login_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@recipe_id", recipe_id);
                    myCommand.Parameters.AddWithValue("@login_id", login_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Removed from favourites");
        }
    }
}
