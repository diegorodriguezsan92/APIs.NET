using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPIrestfull.DataAccess;
using UniversityAPIrestfull.Helpers;
using UniversityAPIrestfull.Models.DataModels;

namespace UniversityAPIrestfull.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        // Example Users
        // TODO: change for real users in DB

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "almacen@imaginagroup.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                Email = "recepcion@imaginagroup.com",
                Name = "User1",
                Password = "User1"
            }
        };


        // Defining tokens

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if(Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                        {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),
                        }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);                            
            }catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);

            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]    // From this moment only Administrator role pass the authentication control
        public IActionResult GetUserList()
            {
            return Ok(Logins);
            }
    }
}
