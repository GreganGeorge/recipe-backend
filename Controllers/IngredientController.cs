using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public IngredientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("Get1")]
        [HttpGet]
        public JsonResult Get1()
        {
            string query = @"select ingredient_id,ingredient_name from ingredients";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
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
        public ActionResult Get2(int recipe_id)
        {
            var result = new { Param1 = recipe_id };
            string query = @"select ingredients.ingredient_name,ingredients.ingredient_id from ingredients,recipe_ingredients where recipe_ingredients.ingredient_id=ingredients.ingredients_id and recipe_ingredients.recipe_id=@recipe_id";
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
        [Route("Get3")]
        [HttpGet]
        public ActionResult Get3()
        {
            string query = @"select * from ingredients order by ingredient_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Route("Get4")]
        [HttpGet]
        public ActionResult Get4(int id)
        {
            string query = "select * from ingredients where ingredient_id=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Route("Post1")]
        [HttpPost]
        public JsonResult Post1(int ingredientId, string ingredientName, string ingredientImage, int price,int qty,string unit)
        {
            string query = @"insert into ingredients values(@ingredientId,@ingredientName,@ingredientImage,@price,@qty,@unit)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ingredientId", ingredientId);
                    myCommand.Parameters.AddWithValue("@ingredientName", ingredientName);
                    myCommand.Parameters.AddWithValue("@ingredientImage", ingredientImage);
                    myCommand.Parameters.AddWithValue("@price", price);
                    myCommand.Parameters.AddWithValue("@qty", qty);
                    myCommand.Parameters.AddWithValue("@unit", unit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [Route("Put1")]
        [HttpPut]
        public JsonResult Put1(int ingredient_id, string ingredient_name, string ingredient_image, int ingredient_price,string unit)
        {
            string query = @"update ingredients set ingredient_name=@ingredient_name,ingredient_image=@ingredient_image,ingredient_price=@ingredient_price,unit=@unit where ingredient_id=@ingredient_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ingredient_id", ingredient_id);
                    myCommand.Parameters.AddWithValue("@ingredient_name", ingredient_name);
                    myCommand.Parameters.AddWithValue("@ingredient_image", ingredient_image);
                    myCommand.Parameters.AddWithValue("@ingredient_price", ingredient_price);
                    myCommand.Parameters.AddWithValue("@unit", unit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfully");
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"delete from ingredients where ingredient_id=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
