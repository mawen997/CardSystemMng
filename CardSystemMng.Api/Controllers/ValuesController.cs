﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;

namespace CardSystemMng.Api.Controllers
{
    [Route("api/Values")]
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
        [HttpGet("Gettestdata/")]
       // [Route("data/Values/Gettestdata")]
        public IActionResult Gettestdata()
        {
            MiniProfiler.Current.CustomTiming("测试"+DateTime.Now,"sdfsdfsdfsdf");
            return Content("8555454");
        }
    }
}