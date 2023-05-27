using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Microsoft.AspNetCore;
using BuisnessLogic;
using BuisnessLogic.Blob;

namespace RestaurantApi.Controllers
{

    [Authorize(Policy = "AdminOnly", AuthenticationSchemes = "Adminonlyscheme")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        IConfiguration configuration;
        ILogic logic;
        public AdminController(IConfiguration configuration1, ILogic _logic)
        {
            configuration = configuration1;
            logic = _logic;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                Console.WriteLine(user.Email, user.Password);
                int res = logic.AdminLogin(user.Email, user.Password);
                if (res != 0)
                {
                    var issuer = configuration["JwtAdmin:Issuer"];
                    var audience = configuration["JwtAdmin:Audience"];
                    var key = Encoding.UTF8.GetBytes(configuration["JwtAdmin:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature
                                        );

                    var subject = new ClaimsIdentity(new[]
                    {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("Role","Admin")
            });
                    var expires = DateTime.UtcNow.AddMinutes(10);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials,


                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok($"{jwtToken}|{res}");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            return Unauthorized();
        }

        [HttpPost("AdminSignup")]
        public IActionResult AdminSignUp([FromBody] AdminModel adminModel)
        {

            try
            {
                AdminModel admin = logic.AddAdmin(adminModel);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddDishes")]
        public IActionResult AddDishes([FromBody] DishesModel dishesModel)
        {
            try
            {
                DishesModel dishesModel1 = logic.AddDish(dishesModel);
                return Ok(dishesModel1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddDishPicture")]
        public IActionResult AddDishPicture(IFormFile formFile)
        {
            try
            {
                BlobClass blobClass = new BlobClass();
                Task<Azure.Response<Azure.Storage.Blobs.Models.BlobContentInfo>> q = blobClass.UploadFiles(formFile);
                int status = (int)q.Status;
                if (status == 1)
                {
                    return Ok(1);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpGet("GetAllDishes")]
        public IActionResult ViewAllDishes()
        {
            try
            {
                IEnumerable<DishesModel> dd = logic.ViewAllDish();
                return Ok(dd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateDish")]
        public IActionResult UpdateDish([FromBody] DishesModel dishesModel)
        {
            try
            {
                return Ok(logic.UpdateDish(dishesModel));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteDish/{id}")]
        public IActionResult DeleteDish([FromRoute] int id)
        {
            try
            {
                return Ok(logic.DeleteDish(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddTable")]
        public IActionResult AddTable([FromBody] TableSeatingModel tableSeatingModel)
        {
            try
            {
                return Ok(logic.AddTables(tableSeatingModel));
            }

            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpGet("ViewAllTable")]
        public IActionResult ViewAllTable()
        {
            try
            {
                IEnumerable<TableSeatingModel> tables = logic.ViewAllTable();
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateTable")]
        public IActionResult UpdateTable([FromBody] TableSeatingModel tableSeatingModel)
        {
            try
            {
                return Ok(logic.UpdateTable(tableSeatingModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteTable/{id}")]
        public IActionResult DeleteTableById([FromRoute] int id)
        {
            try
            {
                return Ok(logic.DeleteTable(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

