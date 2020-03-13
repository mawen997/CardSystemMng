using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardSystemMng.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string GetTokel()
        {
            var cl = new Claim[] {
              new Claim(ClaimTypes.Name, "mawen"),
                new Claim(JwtRegisteredClaimNames.Email, "403648571@qq.com"),
                new Claim(JwtRegisteredClaimNames.Sub, "1"), };//主题subject，就是id uid};


            return "";
        }
    }
}