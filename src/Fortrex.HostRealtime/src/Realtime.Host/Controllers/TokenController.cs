using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Lib.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Realtime.Host.Models.AccountViewModels;
using Realtime.Host.Entities;
using Lib.Domain.User;
using Web.SourceCoin.Common;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTickR
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/token/[Action]")]
    public class TokenController : Controller
    {
        
        private CoreDatabaseContext _db;
        private IConfiguration Configuration;
        public TokenController(IConfiguration config, CoreDatabaseContext dbcontext)
        {
            _db = dbcontext;
            Configuration = config;
        }
        
        [HttpPost]
        [Produces(typeof(CustomJsonResult))]
        public IActionResult CreateToken([FromBody] LoginByTokenViewModel model)
        {
            var resultJson = new CustomJsonResult();
            try
            {
               
                if (string.IsNullOrEmpty(model.Token))
                {
                    resultJson.Message = "Fail";
                    resultJson.StatusCode = 401;
                    return Json(resultJson);
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                DateTime nowDate = ServerTime();//DateTime.Now;
                var result = _db.SessionLogin.Where(p => p.Token.Equals(model.Token) && p.IsActive==true && p.ExpireDate> nowDate).FirstOrDefault();
                if (result != null)
                {
                    var user = _db.Muser.Where(u => u.Id == result.UserId).FirstOrDefault();
                    var jwttoken = JwtTokenBuilder(user);
                    resultJson.Result = result;
                    resultJson.Message = "Success";
                    resultJson.StatusCode = 200;
                    resultJson.access_token = jwttoken;
                    return Json(resultJson);
                }
                else
                {
                    resultJson.Message = "Fail";
                    resultJson.StatusCode = 401;
                    return Json(resultJson);
                }
            }
            catch
            {
                //Console.WriteLine("ex: " + ex.Message);
                resultJson.Message = "Fail";
                resultJson.StatusCode = 400;
                return Json(resultJson);
            }
           
        }
        public DateTime ServerTime()
        {
          
            using (var sqlConnection = new SqlConnection(Configuration["ConnectionStrings:CoreDBConnection"]))
            {
                try
                {
                    sqlConnection.Open();
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "Select GETDATE()  as ServerTime";

                        using (var sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                return DateTime.Parse(sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("ServerTime")).ToString());
                            }
                        }
                    }
                }
                catch
                {
                    sqlConnection.Close();
                }
                finally
                {
                    sqlConnection.Close();
                }


            }
            return DateTime.Now;
        }
        private string JwtTokenBuilder(Muser user)
        {
            //prepare key and credentials
            var identity = new ClaimsIdentity();
            List<Claim> claims = new List<Claim>();
            claims.AddRange(this.GetUserRoleClaims(user));
            claims.Add(new Claim("AccountId", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

          
            
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(Configuration["JWT:key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(issuer: Configuration["JWT:issuer"],
                audience: Configuration["JWT:audience"], signingCredentials: credentials,
                claims: claims,
                expires: DateTime.Now.AddDays(30)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        private IEnumerable<Claim> GetUserRoleClaims(Muser user)
        {
            List<Claim> claims = new List<Claim>();
            IEnumerable<int> roleIds = _db.UserRoleMapping.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).ToList();
            if (roleIds != null)
            {
                foreach (int roleId in roleIds)
                {
                    var role = _db.Role.Where(r => r.Id == roleId).FirstOrDefault();
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }
            return claims;
        }
    }
}
