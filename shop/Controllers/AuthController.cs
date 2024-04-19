using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Data.Dal;

namespace shop.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataAccessor _dataAccessor;

        public AuthController(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        [HttpGet]
        public object Get(String email, String password)
        {
            var user = _dataAccessor.UserDao.Authenticate(email, password);
            String status;

            if (user == null)
            {
                status = "error";
            }
            else
            {
                status = "ok";
                HttpContext.Session.SetString("auth-user-id", user.Id.ToString());
            }

            return new { status };
        }
    }
}
