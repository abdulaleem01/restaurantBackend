using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity;
using BuisnessLogic;
using Entities.DbEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApi
{
    [Authorize(Policy = "CustomerOnly", AuthenticationSchemes = "Customeronlyscheme")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        IConfiguration configuration;
        ILogic logic;
        // GET: api/values


        public CustomerController(IConfiguration configuration1, ILogic _logic)
        {
            configuration = configuration1;
            logic = _logic;
        }





        [AllowAnonymous]
        [HttpPost("CustomerLogin")]
        public IActionResult CustomerLogin(User user)
        {
            try
            {
                int val = logic.CustomerLogin(user.Email, user.Password);
                if (val != 0)
                {
                    var issuer = configuration["Jwt:Issuer"];
                    var audience = configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature
                                        );

                    var subject = new ClaimsIdentity(new[]
                    {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("Role","Customer")
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

                    return Ok($"{jwtToken}|{val}");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(0);

        }

        [AllowAnonymous]
        [HttpPost("CustomerSignUp")]
        public IActionResult CustomerSignup(CustomerModel customerModel)
        {
            try
            {
                CustomerModel customer = logic.AddCustomer(customerModel);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("CustomerById/{id}")]
        public IActionResult GetCustomerDetailById([FromRoute] int id)
        {
            try
            {
                CustomerModel customer = logic.GetCustomerDetailById(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] CustomerModel customerModel)
        {
            try
            {
                CustomerModel customer = logic.UpdateCustomerDetails(customerModel);
                return Ok(customerModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddVisit")]
        public IActionResult AddVisitDetails([FromBody] VisitDetailModel visitDetailModel)
        {
            try
            {
                VisitDetailModel visitDetail = logic.AddVisitDetail(visitDetailModel);
                return Ok(visitDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetVisitById/{id}")]
        public IActionResult GetVisitById([FromRoute] int id)
        {
            try
            {
                VisitDetailModel visitDetailModel = logic.GetVisitDetailById(id);
                return Ok(visitDetailModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("UpdateDeliveryStatus/{id}/{status}")]
        public IActionResult ChangeDeliveryStatus([FromRoute] int id, int status)
        {
            try
            {
                VisitDetailModel visitDetailModel = logic.ChangeDeliveryStatus(id, status);
                return Ok(visitDetailModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPut("UpdatePaymentStatus/{id}/{status}")]
        public IActionResult ChangePaymentStatus([FromRoute] int id, int status)
        {
            try
            {
                VisitDetailModel visitDetailModel = logic.ChangePaymentStatus(id, status);
                return Ok(visitDetailModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("AddOrderDetails")]
        public IActionResult AddOrders([FromBody] OrderDetailModel orderDetailModel)
        {
            try
            {
                OrderDetailModel orderDetailModel1 = logic.AddOrderDetail(orderDetailModel);
                return Ok(orderDetailModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOrderById/{id}")]
        public IActionResult DeleteOrder([FromRoute] int id)
        {
            try
            {
                OrderDetailModel orderDetailModel = logic.DeleteOrderDetails(id);
                return Ok(orderDetailModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrdersByVisitId/{id}")]
        public IActionResult GetOrderByVisit([FromRoute] int id)
        {
            try
            {
                IEnumerable<OrderDetailModel> orderDetailModels = logic.GetOrdersByVisitId(id);
                return Ok(orderDetailModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TotalOrderAmmount/{visitId}")]
        public IActionResult TotalOrder([FromRoute] int visitId)
        {
            try
            {
                return Ok(logic.TotalOrderAmount(visitId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddMultipleOrders")]
        public IActionResult AddMultipleOrders([FromBody] IEnumerable<OrderDetailModel> orderDetailModels)
        {
            try
            {
                return Ok(logic.AddMultipleOrders(orderDetailModels));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //From Admin Amenities

        [HttpGet("AvailableCustomers")]
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

        [HttpGet("AvailableDishes")]
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

        //Custom

        [HttpGet("GetDishesByVisitId/{visitId}")]
        public IActionResult GetDishesByVisitId([FromRoute] int visitId)
        {
            try
            {
                return Ok(logic.GetDishInfoByVisitId(visitId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDishInfoByVisitId/{visitId}")]
        public IActionResult GetDishInfoByVisitId([FromRoute] int visitId)
        {
            try
            {
                return Ok(logic.GetOrderDishByVisitId(visitId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IEnumerable<VisitDetail> GetAllVisitDetailsByCustomerId(int id);

        [HttpGet("GetAllVisitDetailsByCustomerId/{customerId}")]
        public IActionResult GetAllVisitDetailsByCustomerId([FromRoute] int customerId)
        {
            try
            {
                return Ok(logic.GetAllVisitDetailsByCustomerId(customerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdatePassword/{email}/{opass}/{npass}")]
        public IActionResult updatePass([FromRoute] string email, string opass, string npass)
        {
            try
            {
                return Ok(logic.ChangePassword(email, opass, npass));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}

