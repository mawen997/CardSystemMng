using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardSystemMng.Common.GlobalVar;
using CardSystemMng.Model.SeedDt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardSystemMng.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
  //  [Authorize(Permissions.Name)]
    public class DbFirstController : ControllerBase
    {
        private readonly MyContext myContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myContext"></param>
        public DbFirstController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        /// <summary>
        /// 生成整体框架
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetFrameFiles()
        {
            return FrameSeedMd.CreateModels(myContext)
                && FrameSeedMd.CreateIRepositorys(myContext)
                && FrameSeedMd.CreateIServices(myContext)
                && FrameSeedMd.CreateRepository(myContext)
                && FrameSeedMd.CreateServices(myContext)
                ;
        }


        /// <summary>
        /// 生成 Model 层文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetModelFiles()
        {
            return FrameSeedMd.CreateModels(myContext);
        }

        /// <summary>
        /// 生成 IRepository 层文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetIRepositoryFiles()
        {
            return FrameSeedMd.CreateIRepositorys(myContext);
        }

        /// <summary>
        /// 生成 IService 层文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetIServiceFiles()
        {
            return FrameSeedMd.CreateIServices(myContext);
        }

        /// <summary>
        /// 生成 Repository 层文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetRepositoryFiles()
        {
            return FrameSeedMd.CreateRepository(myContext);
        }

        /// <summary>
        /// 生成 Services 层文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetServicesFiles()
        {
            return FrameSeedMd.CreateServices(myContext);
        }

    }
}