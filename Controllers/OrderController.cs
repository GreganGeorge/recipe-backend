using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using WebApplication3.Models;
using static WebApplication3.Controllers.StripeController;
namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("Get1")]
        [HttpGet]
        public ActionResult Get1(int login_id)
        {
            string query = "select ingredients.ingredient_name,ingredients.ingredient_image,cartorder.price,cartorder.quantity,cartorder.unit from ingredients,cartorder where ingredients.ingredient_id=cartorder.ingredient_id and login_id=@login_id";
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
        [HttpGet]
        public ActionResult Get()
        {
            string query = "select * from favourites";
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
        public JsonResult Post(string ingredientlistpass, int login_id)
        {
            var ingredient_ids = new List<CartItems>();
            if (ingredientlistpass != null)
            {
                ingredient_ids = JsonConvert.DeserializeObject<List<CartItems>>(ingredientlistpass);
            }
            foreach (var item in ingredient_ids)
            {
                string query = @"insert into cartorder (login_id, price, quantity, unit, ingredient_id)
                                     values (@login_id, @price, @quantity, @unit, @ingredient_id)";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@login_id", login_id);
                        myCommand.Parameters.AddWithValue("@price", item.price);
                        myCommand.Parameters.AddWithValue("@quantity", item.quantity);
                        myCommand.Parameters.AddWithValue("@unit", item.unit);
                        myCommand.Parameters.AddWithValue("@ingredient_id", item.ingredient_id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            return new JsonResult("");
        }
    }
}
