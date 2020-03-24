using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;
using CardSystemMng.Common.Helper;
namespace CardSystemMng.Api.Controllers
{
    /// <summary>
    /// 啊手动阀手动阀
    /// </summary>
    [Route("api/Values")]
    [Produces("application/json")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //[NonAction]
        ///// <summary>
        ///// 色弱发无色的发射点
        ///// </summary>
        ///// <returns></returns>
        //public string GetTokel()
        //{
        //    var cl = new Claim[] {
        //      new Claim(ClaimTypes.Name, "mawen"),
        //        new Claim(JwtRegisteredClaimNames.Email, "403648571@qq.com"),
        //        new Claim(JwtRegisteredClaimNames.Sub, "1"), };//主题subject，就是id uid};


        //    return new Model.Models.Module().MToString();
        //}
        ///// <summary>
        ///// 啊手动阀手动阀
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[NonAction]
        //public async Task<string> Get()
        //{
        //    var info = string.Format("api执行线程:{0}", Thread.CurrentThread.ManagedThreadId);
        //    var infoTask = await Gettestdata();
        //    var infoTaskFinished = string.Format("api执行线程（task调用完成后）:{0}", Thread.CurrentThread.ManagedThreadId);
        //    return string.Format("{0},{1},{2}", info, infoTask, infoTaskFinished);
        //}
        ///// <summary>
        ///// 啊手动阀手动阀
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("Gettestdata/")]
        //[NonAction]
        //// [Route("data/Values/Gettestdata")]
        //public async Task<string> Gettestdata()
        //{
        //    await Task.Delay(500);
        //    MiniProfiler.Current.CustomTiming("测试" + DateTime.Now, "sdfsdfsdfsdf");
        //    return string.Format("task 执行线程:{0}", Thread.CurrentThread.ManagedThreadId);

        //}
       //[Produces("application/json")]
        [HttpGet]
        [Route("Getdata/")]
        /// <summary>
        /// 获取json
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Models.Module Getdata(string id)
        {
            return new Model.Models.Module();
        }
    }
}