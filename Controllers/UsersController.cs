using BIkeRentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;



namespace BIkeRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string connectionString;
        public UsersController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("LjfDatabase");
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto userDto) 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_Users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // If the stored procedure requires parameters, you can add them like this:
                        // command.Parameters.AddWithValue("@paramName", paramValue);
                        command.Parameters.AddWithValue("@operation", "Create");
                        command.Parameters.AddWithValue("@name", userDto.Name);
                        command.Parameters.AddWithValue("@location", userDto.Location);

                        // Execute the stored procedure
                        command.ExecuteNonQuery(); // or use ExecuteReader() or ExecuteScalar() as needed
                    }
                }

            } catch (Exception ex) 
            {
                ModelState.AddModelError("User", "Error Creating User");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetUsers() 
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_Users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // If the stored procedure requires parameters, you can add them like this:
                        // command.Parameters.AddWithValue("@paramName", paramValue);
                        command.Parameters.AddWithValue("@operation", "Read");

                        // Execute the stored procedure
                        using (var reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                User user = new User();

                                user.Id = reader.GetInt32(0);
                                user.Name = reader.GetString(1);
                                user.Location = reader.GetString(2);

                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("User", "Error Getting Users");
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            User user = new User();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_Users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // If the stored procedure requires parameters, you can add them like this:
                        // command.Parameters.AddWithValue("@paramName", paramValue);
                        command.Parameters.AddWithValue("@operation", "Read");
                        command.Parameters.AddWithValue("@id", id);

                        // Execute the stored procedure
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                user.Id = reader.GetInt32(0);
                                user.Name = reader.GetString(1);
                                user.Location = reader.GetString(2);

                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("User", "Error Getting User");
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDto userDto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_Users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // If the stored procedure requires parameters, you can add them like this:
                        // command.Parameters.AddWithValue("@paramName", paramValue);
                        command.Parameters.AddWithValue("@operation", "Update");
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", userDto.Name);
                        command.Parameters.AddWithValue("@location", userDto.Location);

                        // Execute the stored procedure
                        command.ExecuteNonQuery(); // or use ExecuteReader() or ExecuteScalar() as needed
                    }
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("User", "Error Updating User");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id) 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_Users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // If the stored procedure requires parameters, you can add them like this:
                        // command.Parameters.AddWithValue("@paramName", paramValue);
                        command.Parameters.AddWithValue("@operation", "Delete");
                        command.Parameters.AddWithValue("@id", id);

                        // Execute the stored procedure
                        command.ExecuteNonQuery(); // or use ExecuteReader() or ExecuteScalar() as needed
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("User", "Error Deleting User");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
