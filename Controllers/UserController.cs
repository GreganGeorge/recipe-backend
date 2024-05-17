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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult Get(string reg)
        {
            string query = "select email,password from user_login where email=@email and password=@password";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    //myCommand.Parameters.AddWithValue("@email", reg[0].email);
                    //myCommand.Parameters.AddWithValue("@password", reg[0].password);
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
        public ActionResult Get1(string password,string email)
        {
            string query = "select login_id,email,password from user_login where email=@email and password=@password";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@password", password);
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
        public ActionResult Get2(int id)
        {
            string query = "select * from recipe where recipe_id=@id";
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
        [Route("Get3")]
        [HttpGet]
        public ActionResult Get3(string password, string email)
        {
            string query = "select email,password from admin_login where email=@email and password=@password";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@password", password);
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
        public ActionResult Get4(string email)
        {
            string query = "select email,username,address,phone_number from user_login where email=@email";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Route("Get5")]
        [HttpGet]
        public ActionResult Get5()
        {
            string query = "select * from suggestion";
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
        [Route("Post1")]
        [HttpPost]
        public JsonResult Post1(int recipeId,string recipeName,string recipeImage,string vegNonVeg,string nutrition,string instructions,string video)
        {
            string query = @"insert into recipe values(@recipeId,@recipeName,@recipeImage,@vegNonVeg,@nutrition,@instructions,@video)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@recipeId", recipeId);
                    myCommand.Parameters.AddWithValue("@recipeName", recipeName);
                    myCommand.Parameters.AddWithValue("@recipeImage", recipeImage);
                    myCommand.Parameters.AddWithValue("@vegNonVeg", vegNonVeg);
                    myCommand.Parameters.AddWithValue("@nutrition", nutrition);
                    myCommand.Parameters.AddWithValue("@instructions", instructions);
                    myCommand.Parameters.AddWithValue("@video", video);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [Route("Post2")]
        [HttpPost]
        public JsonResult Post2(string email, string username, string suggestion)
        {
            string query = @"insert into suggestion values(@email,@username,@suggestion)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@username", username);
                    myCommand.Parameters.AddWithValue("@suggestion", suggestion);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [Route("insertRecipeIngredient")]
        [HttpGet]
        public ActionResult insertRecipeIngredient(string numlistpass,int recipe_id)
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
                string query = @"insert into recipe_ingredients values(@recipe_id,@ingredient_id)";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@recipe_id", recipe_id);
                        myCommand.Parameters.AddWithValue("@ingredient_id", x.ingredient_id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            });
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(string password, string email)
        {
            string query = @"update user_login set password=@password where email=@email";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@password", password);
                    myCommand.Parameters.AddWithValue("@email", email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Password Changed successfully");
        }
        [Route("Put1")]
        [HttpPut]
        public JsonResult Put1(int recipe_id,string recipe_name, string recipe_image,string veg_nonveg,string nutrition,string instructions,string video)
        {
            string query = @"update recipe set recipe_name=@recipe_name,recipe_image=@recipe_image,veg_nonveg=@veg_nonveg,nutrition=@nutrition,instructions=@instructions,video=@video where recipe_id=@recipe_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@recipe_id", recipe_id);
                    myCommand.Parameters.AddWithValue("@recipe_name", recipe_name);
                    myCommand.Parameters.AddWithValue("@recipe_image", recipe_image);
                    myCommand.Parameters.AddWithValue("@veg_nonveg", veg_nonveg);
                    myCommand.Parameters.AddWithValue("@nutrition", nutrition);
                    myCommand.Parameters.AddWithValue("@instructions", instructions);
                    myCommand.Parameters.AddWithValue("@video", video);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfully");
        }
        [Route("Put2")]
        [HttpPut]
        public JsonResult Put2(string email,string username,string address,string phone_number)
        {
            string query = @"update user_login set username=@username,address=@address,phone_number=@phone_number where email=@email";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@username", username);
                    myCommand.Parameters.AddWithValue("@address", address);
                    myCommand.Parameters.AddWithValue("@phone_number", phone_number);
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
            string query = @"delete from recipe where recipe_id=@id";
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
