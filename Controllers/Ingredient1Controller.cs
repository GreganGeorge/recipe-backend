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
    public class Ingredient1Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Ingredient1Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult Get(int recipe_id)
        {
            var result = new { Param1 = recipe_id };
            string query = "select recipe.recipe_name,recipe.recipe_image,recipe.nutrition,recipe.instructions,recipe.video,ingredients.ingredient_name,ingredients.ingredient_id from recipe,ingredients,recipe_ingredients where recipe.recipe_id=recipe_ingredients.recipe_id and recipe_ingredients.ingredient_id=ingredients.ingredient_id and recipe_ingredients.recipe_id=@recipe_id";
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
        public JsonResult Get2()
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
        [Route("Get3")]
        [HttpGet]
        public ActionResult Get3( string numlistpass)
        {
            var ingredient_ids = new List<Ingredientpass>();
            if (numlistpass != null)
            {
                ingredient_ids = JsonConvert.DeserializeObject<List<Ingredientpass>>(numlistpass);
            }
            var count = 0;
            var ingredient_idspass = "";
            ingredient_ids.ForEach(x =>
            {
                if (count != 0)
                {
                    ingredient_idspass = ingredient_idspass + ",";
                }
                ingredient_idspass = ingredient_idspass + x.ingredient_id.ToString();
                count++;
            });
            ingredient_idspass = ingredient_idspass + "";
            var result = new { Param1 = ingredient_idspass };
            string query = "select recipe.recipe_name,recipe.recipe_id,recipe.recipe_image from recipe,recipe_ingredients where recipe.recipe_id=recipe_ingredients.recipe_id and recipe_ingredients.ingredient_id in (";
            for (int i = 0; i < ingredient_ids.Count; i++)
            {
                query += i == 0 ? "@" : ", @";
                query += $"ingredientId{i}";
            }
            query += ") group by recipe_name,recipe.recipe_id,recipe.recipe_image having count(distinct ingredient_id)=@count";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue($"@count", count);
                    for (int i = 0; i < ingredient_ids.Count; i++)
                    {
                        myCommand.Parameters.AddWithValue($"@ingredientId{i}", ingredient_ids[i].ingredient_id);
                    }
                    //myCommand.Parameters.AddWithValue("@ingredient_idspass","("+ingredient_idspass+")");
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
            //var result = new { Param1 = ingredient_id };
            //string query = "select distinct(recipe.recipe_name),recipe.recipe_id from recipe,recipe_ingredients where recipe.recipe_id=recipe_ingredients.recipe_id and recipe_ingredients.ingredient_id in (1,3)";
            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            //SqlDataReader myReader;
            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        myCommand.Parameters.AddWithValue("@ingredient_id", ingredient_id);
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader);
            //        myReader.Close();
            //        myCon.Close();
            //    }
            //}
            //return new JsonResult(table);
  
        }
        [Route("Get4")]
        [HttpGet]
        public JsonResult Get4(string password)
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
        [HttpPost]
        public JsonResult Post(Register reg)
        {
            string query = @"insert into user_login values(@username,@email,@password)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username", reg.username);
                    myCommand.Parameters.AddWithValue("@email", reg.email);
                    myCommand.Parameters.AddWithValue("@password", reg.password);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
    }
}
