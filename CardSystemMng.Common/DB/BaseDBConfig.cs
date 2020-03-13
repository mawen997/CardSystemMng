using CardSystemMng.Common.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CardSystemMng.Common.DB
{
    public class BaseDBConfig
    {
        /// <summary>
        /// 多库操作，默认加载的是appsettings.json设置为true的第一个db连接。
        /// </summary>
        public static List<MutiDBOperate> MutiConnectionString => MutiInitConn();
        private static string DifDBConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (System.Exception) { }
            }

            return conn[conn.Length - 1];
        }


        public static List<MutiDBOperate> MutiInitConn()
        {
            List<MutiDBOperate> listdatabase = new List<MutiDBOperate>();
            List<MutiDBOperate> listdatabaseSimpleDB = new List<MutiDBOperate>();
            string Path = "appsettings.json";
            using (var file = new StreamReader(Path))
            using (var reader = new JsonTextReader(file))
            {
                var jObj = (JObject)JToken.ReadFrom(reader);
                if (!string.IsNullOrWhiteSpace("DBS"))
                {
                    var secJt = jObj["DBS"];
                    if (secJt != null)
                    {
                        for (int i = 0; i < secJt.Count(); i++)
                        {
                            if (secJt[i]["Enabled"].ObjToBool())
                            {
                                listdatabase.Add(SpecialDbString(new MutiDBOperate()
                                {
                                    ConnId = secJt[i]["ConnId"].ObjToStr(),
                                    Conn = secJt[i]["Connection"].ObjToStr(),
                                    DbType = (DataBaseType)(secJt[i]["DBType"].ObjToInt()),
                                }));
                            }
                        }
                    }
                }


                // 单库，只保留一个
                if (!Appsettings.app(new string[] { "MutiDBEnabled" }).ObjToBool())
                {
                    if (listdatabase.Count == 1)
                    {
                        return listdatabase;
                    }
                    else
                    {
                        var dbFirst = listdatabase.FirstOrDefault(d => d.ConnId == Appsettings.app(new string[] { "MainDB" }).ObjToStr());
                        if (dbFirst == null)
                        {
                            dbFirst = listdatabase.FirstOrDefault();
                        }
                        listdatabaseSimpleDB.Add(dbFirst);
                        return listdatabaseSimpleDB;
                    }

                }

                return listdatabase;
            }
        }

        private static MutiDBOperate SpecialDbString(MutiDBOperate mutiDBOperate)
        {
            if (mutiDBOperate.DbType == DataBaseType.Sqlite)
            {
                mutiDBOperate.Conn = $"DataSource=" + Path.Combine(Environment.CurrentDirectory, mutiDBOperate.Conn);
            }
            else if (mutiDBOperate.DbType == DataBaseType.SqlServer)
            {
                mutiDBOperate.Conn = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1.txt", @"c:\my-file\dbCountPsw1.txt", mutiDBOperate.Conn);
            }
            else if (mutiDBOperate.DbType == DataBaseType.MySql)
            {
                mutiDBOperate.Conn = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1_MySqlConn.txt", @"c:\my-file\dbCountPsw1_MySqlConn.txt", mutiDBOperate.Conn);
            }
            else if (mutiDBOperate.DbType == DataBaseType.Oracle)
            {
                mutiDBOperate.Conn = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1_OracleConn.txt", @"c:\my-file\dbCountPsw1_OracleConn.txt", mutiDBOperate.Conn);
            }

            return mutiDBOperate;
        }
    }


    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
    public class MutiDBOperate
    {
        public string ConnId { get; set; }
        public string Conn { get; set; }
        public DataBaseType DbType { get; set; }
    }
}
