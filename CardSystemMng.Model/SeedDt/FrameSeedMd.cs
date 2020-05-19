using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardSystemMng.Model.SeedDt
{
    public class FrameSeedMd
    { /// <summary>
      /// 生成Model层
      /// </summary>
      /// <param name="myContext"></param>
      /// <returns></returns>
        public static bool CreateModels(MyContext myContext)
        {

            try
            {
                var pl = Environment.CurrentDirectory;
                var fl = pl.Replace("CardSystemMng.Api", "") + @"CardSystemMng.Model\Models";
                myContext.Create_Model_ClassFileByDBTalbe(fl, "CardSystemMng.Model.Models", new string[] { }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 生成IRepository层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateIRepositorys(MyContext myContext)
        {

            try
            {
                var pl = Environment.CurrentDirectory;
                var fl = pl.Replace("CardSystemMng.Api", "") + @"CardSystemMng.IRepository";
                myContext.Create_IRepository_ClassFileByDBTalbe(fl, "CardSystemMng.IRepository", new string[] { }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 生成 IService 层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateIServices(MyContext myContext)
        {

            try
            {
                var pl = Environment.CurrentDirectory;
                var fl = pl.Replace("CardSystemMng.Api", "") + @"CardSystemMng.IServices";
                myContext.Create_IServices_ClassFileByDBTalbe(fl, "CardSystemMng.IServices", new string[] { }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 生成 Repository 层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateRepository(MyContext myContext)
        {

            try
            {
                var pl = Environment.CurrentDirectory;
                var fl = pl.Replace("CardSystemMng.Api", "") + @"CardSystemMng.Repository";
                myContext.Create_Repository_ClassFileByDBTalbe(fl, "CardSystemMng.Repository", new string[] { }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 生成 Service 层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateServices(MyContext myContext)
        {

            try
            {
                var pl = Environment.CurrentDirectory;
                var fl = pl.Replace("CardSystemMng.Api", "") + @"CardSystemMng.Services";
                myContext.Create_Services_ClassFileByDBTalbe(fl, "CardSystemMng.Services", new string[] {}, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}