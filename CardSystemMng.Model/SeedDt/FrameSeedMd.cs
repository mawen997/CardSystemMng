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
                
                myContext.Create_Model_ClassFileByDBTalbe($@"E:\实验室\ASP.NET COR学习\CardSystemMng\CardSystemMng.Model\Models", "CardSystemMng.Model.Models", new string[] { }, "");
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
                myContext.Create_IRepository_ClassFileByDBTalbe($@"E:\实验室\ASP.NET COR学习\CardSystemMng\CardSystemMng.IRepository", "CardSystemMng.IRepository", new string[] { }, "");
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
                myContext.Create_IServices_ClassFileByDBTalbe($@"E:\实验室\ASP.NET COR学习\CardSystemMng\CardSystemMng.IServices", "CardSystemMng.IServices", new string[] { "Module" }, "");
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
                myContext.Create_Repository_ClassFileByDBTalbe($@"E:\实验室\ASP.NET COR学习\CardSystemMng\CardSystemMng.Repository", "CardSystemMng.Repository", new string[] { "Module" }, "");
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
                myContext.Create_Services_ClassFileByDBTalbe($@"E:\实验室\ASP.NET COR学习\CardSystemMng\CardSystemMng.Services", "CardSystemMng.Services", new string[] { "Module" }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}