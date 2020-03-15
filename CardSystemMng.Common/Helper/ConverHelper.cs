/*  作者：       tianzh
*  创建时间：   2012/7/22 15:38:20
*
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Web.Caching;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using System.Security.Cryptography;

namespace CardSystemMng.Common.Helper
{

    /// <summary>
    /// 强制转化辅助类(无异常抛出)
    /// </summary>
    public static class ConvertHelper
    {
        #region MD5加密
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(this string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", string.Empty);
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password = "")
        {
            string pwd = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
                {
                    MD5 md5 = MD5.Create(); //实例化一个md5对像
                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                    foreach (var item in s)
                    {
                        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                        pwd = string.Concat(pwd, item.ToString("X2"));
                    }
                }
            }
            catch
            {
                throw new Exception($"错误的 password 字符串:【{password}】");
            }
            return pwd;
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(this string password)
        {
            // 实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(s);
        } 
        #endregion

        #region 去除富文本中的HTML标签
        /// <summary>
        /// 去除富文本中的HTML标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(this string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
        #endregion
        public static readonly string Line = "\r\n------------------------------------------\r\n";
      
        public static string ToXmls(this string str)
        {
            try
            {
                string xml = str;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                StringWriter sw = new StringWriter();
                using (XmlTextWriter writer = new XmlTextWriter(sw))
                {
                    writer.Indentation = 2;  // the Indentation
                    writer.Formatting = Formatting.Indented;
                    doc.WriteContentTo(writer);
                    writer.Close();
                }

                return sw.ToString();
            }
            catch (Exception e)
            {
                return str;
            }
        }
        /// <summary>
        /// 获取对象的名称
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static string GetTypeName(this object ob)
        {
           return nameof(ob);
        }

       
        /// <summary>
        /// 图片截取
        /// </summary>
        /// <param name="b"></param>
        /// <param name="StartX"></param>
        /// <param name="StartY"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns></returns>
        public static Bitmap ToCep(this Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
       
    }
    /// <summary>
    /// 根据数据库返回的0和1或者false或true判断状态
    /// </summary>
    /// <returns></returns>
    public static string StTOMess(this object obj)
        {
            if (obj.ObjToStr() == "0")
            {
                return "添加失败！";
            }
            if (obj.ObjToInt() > 0)
            {
                return "操作成功！";
            }
            if (obj.ObjToBool())
            {
                return "更新成功！";
            }
            else
            {
                return "更新失败！";
            }
        }
        
       
        #region 获取文本文件的字符编码
        /// <summary>
        /// 获取文本文件的字符编码
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static System.Text.Encoding GetFileEncodeType(this string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            Byte[] buffer = br.ReadBytes(2);
            if (buffer[0]>=0xEF)
            {
                if (buffer[0]==0xEF&&buffer[1]==0xBB)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (buffer[0]==0xFE&&buffer[1]==0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (buffer[0]==0xFF&&buffer[1]==0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return System.Text.Encoding.Default;
            }
        } 
        #endregion
        /// <summary>
        /// 得到一年中的某周的起始日和截止日
        /// 年 nYear
        /// 周数 nNumWeek
        /// 周始 out dtWeekStart
        /// 周终 out dtWeekeEnd
        /// </summary>
        /// <param name="nYear"></param>
        /// <param name="nNumWeek"></param>
        /// <param name="dtWeekStart"></param>
        /// <param name="dtWeekeEnd"></param>
        public static void GetWeek(this int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            var dt = new DateTime(nYear, 1, 1);
            dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Sunday);
            if (dtWeekStart < new DateTime(nYear, 1, 1))
                dtWeekStart = new DateTime(nYear, 1, 1);

            dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1);
            if (dtWeekeEnd > new DateTime(nYear, 1, 1).AddYears(1))
                dtWeekeEnd = new DateTime(nYear, 1, 1).AddYears(1);
        }

      
        /**/

        /// <summary>
        /// 求某年有多少周
        /// 返回 int
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns>int</returns>
        public static int GetYearWeekCount(this int strYear)
        {
            var fDt = DateTime.Parse(strYear + "-01-01");
            var k = Convert.ToInt32(fDt.DayOfWeek); //得到该年的第一天是周几 
            if (k == 1)
            {
                var countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                var countWeek = countDay / 7 + 1;
                return countWeek;
            }
            else
            {
                var countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                var countWeek = countDay / 7 + 2;
                return countWeek;
            }
        }

        /**/

        /// <summary>
        /// 求当前日期是一年的中第几周
        /// </summary>
        /// <param name="curDay"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime curDay)
        {
            var firstdayofweek = Convert.ToInt32(Convert.ToDateTime(curDay.Year + "-01-01 ").DayOfWeek);

            var days = curDay.DayOfYear;
            var daysOutOneWeek = days - (7 - firstdayofweek);

            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            var weeks = daysOutOneWeek / 7;
            if (daysOutOneWeek % 7 != 0)
                weeks++;

            return weeks + 1;
        }

        /// <summary>
        /// Xml字符串转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T XmlStrToClass<T>(this string str)
        {
            var xml=new XmlDocument();
            xml.LoadXml(str);
            var json = JsonConvert.SerializeXmlNode(xml).Replace("@","").Replace("#","");
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>  
        /// 类转xml字符串  
        /// </summary>  
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>  
        public static string ClassToXmlStr<T>(this T source)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, source);
            return sw.ToString();
        }
        /// <summary>
        /// 图片转为base64编码的字符串
        /// </summary>
        /// <param name="imagefilename"></param>
        /// <returns></returns>
        public static string ImgToBase64String(this string imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// base64编码的字符串转为图片
        /// </summary>
        /// <param name="strbase64"></param>
        /// <returns></returns>
        public static Bitmap Base64StringToImage(this string strbase64)
        {
            try
            {
               // strbase64 = strbase64.Replace("%2B", "+");
                byte[] arr = Convert.FromBase64String(strbase64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save("test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save("test.bmp", ImageFormat.Bmp);
                //bmp.Save("test.gif", ImageFormat.Gif);
                //bmp.Save("test.png", ImageFormat.Png);
                ms.Close();
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ErrPath = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Images\\";

        public static void SaveImg(this Image img,string name)
        {
            if (!Directory.Exists(ErrPath))
            {
                Directory.CreateDirectory(ErrPath);
            }
            img.Save(ErrPath+name,ImageFormat.Jpeg);
        }

        /// <summary>
        /// 保存日志到本地
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="path"></param>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <param name="orderid"></param>
        /// <param name="pnr"></param>
        /// <param name="cmd"></param>
        public static void SaveFLog(this string mes, string path, string userid, string type, string orderid = null, string pnr = null, string cmd = null)
        {
            try
            {
                (Line + DateTime.Now + "[" + userid + "]" + type + orderid + pnr + cmd + ">：" + mes + Line).WriteShareFile(path);
            }
            catch (Exception)
            {

                // throw;
            }
        }

       
        /// <summary>
        /// 共享式的写入
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        public static void WriteShareFile(this string str,string path)
        {
            try
            {
                var pth = Path.GetDirectoryName(path);
                // ReSharper disable once AssignNullToNotNullAttribute
                if (path != null && !Directory.Exists(pth))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (pth != null) Directory.CreateDirectory(pth);
                }
                using (var file = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    var bty = Encoding.Default.GetBytes("\r\n-----------------------\r\n"+DateTime.Now+">\r\n"+str);
                    file.Write(bty, 0, bty.Length);
                }
            }
            catch (Exception)
            {
                
              //  throw;
            }
           
        }
        /// <summary>
        ///  DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToLong(this DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// Base64的解码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="eny">编码</param>
        /// <returns></returns>
        public static string ToDecBase64String(this string str,string eny="gbk")
        {
            try
            {
                return Encoding.GetEncoding(eny).GetString(Convert.FromBase64String(str));
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Base64的编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="eny">编码</param>
        /// <returns></returns>
        public static string ToEncBase64String(this string str, string eny = "gbk")
        {
            try
            {
                return Convert.ToBase64String(Encoding.GetEncoding(eny).GetBytes(str));
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 一般写入（快速）,一行一行的写入
        /// </summary>
        public static bool FileWriteLines(this string str,string path)
        {
            try
            {
                var pth = Path.GetDirectoryName(path);
                // ReSharper disable once AssignNullToNotNullAttribute
                if (path != null && !Directory.Exists(pth))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (pth != null) Directory.CreateDirectory(pth);
                }

                IEnumerable<string> strd = new[] {str};
                if (path != null) File.AppendAllLines(path, strd);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// Model转stringjson
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MToString(this object str)
        {
            return JsonConvert.SerializeObject(str);
        }

        public static void ToSaveFile(this string str,string path)
        {
            try
            {
                var pth = Path.GetDirectoryName(path);
                // ReSharper disable once AssignNullToNotNullAttribute
                if (path != null && !Directory.Exists(pth))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (pth != null) Directory.CreateDirectory(pth);
                }
                System.IO.File.WriteAllText(path, str);
            }
            catch (Exception e)
            {
                
            }
           
        }
       // [DllImport("kernel32.dll")]
       // public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

       // [DllImport("kernel32.dll")]
        //public static extern bool CloseHandle(IntPtr hObject);
        //public const int OF_READWRITE = 2;
        //public const int OF_SHARE_DENY_NONE = 0x40;
        //public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        /// <summary>
        /// 向文件中添加内容或创建新的文件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileWrite(this string str, string path)
        {
            try
            {
               // var lk = 0;
               // tp:
                var pth = Path.GetDirectoryName(path);
                // ReSharper disable once AssignNullToNotNullAttribute
                if (path != null && !Directory.Exists(pth))
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (pth != null) Directory.CreateDirectory(pth);
                }
                //IntPtr vHandle = _lopen(path, OF_READWRITE | OF_SHARE_DENY_NONE);
                //if (vHandle != HFILE_ERROR)
                //{
                    if (path != null) File.AppendAllText(path, str);
                //}
                //else
                //{
                //    if (lk >= 50)
                //    {
                //        return false;
                //    }
                //    Thread.Sleep(10);
                //    goto tp;
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 小数点字符串转数字
        /// </summary>
        /// <param name="nm"></param>
        /// <returns></returns>
        public static double Todouble(this object nm)
        {
            try
            {
                double dl = 0;
                Double.TryParse(nm.ObjToStr(), out dl);
                if (double.IsPositiveInfinity(dl))
                {
                    return 0;
                }
                else if (double.IsNaN(dl))
                {
                    return 0;
                }
                return  dl;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 不区分大小写的比较
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static  bool Equals(this object ob,List<object> list)
        {           
            return list.Exists(p=>p==ob);
        }
        /// <summary>
        /// 两个实体类相互赋值
        /// </summary>
        /// <typeparam name="T">目标类</typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static T ToCopyModel<T>(this object f)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(f));
        }


        public static void SaveFile(this byte[] by,string path)
        {
            FileStream fl = null;
            try
            {
                var fld = Path.GetDirectoryName(path);
                //实例化流对象
                if (!Directory.Exists(fld))
                {
                    Directory.CreateDirectory(fld);
                }
                using (fl = new FileStream(path, FileMode.Create))
                {
                    fl.Write(by,0,by.Length);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (fl != null)
                {
                    fl.Close();
                }
            }
        }
        /// <summary>
        /// 两个实体类相互赋值
        /// </summary>
        /// <typeparam name="T">目标类</typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static T ToCopyModel<T>(this T f)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(f));
            }
            catch (Exception)
            {

                return f;
            }
           
        }
        #region 金额小写转中文大写

        /// <summary>
        /// 金额小写转中文大写。
        /// 整数支持到万亿；小数部分支持到分(超过两位将进行Banker舍入法处理)
        /// </summary>
        /// <returns>转换后的字符串</returns>
        public static String NumGetStr(this object nums)
        {
            Double num = 0;
            try
            {
                num = Convert.ToDouble(nums);
            }
            catch (Exception)
            {

                return "";
            }

            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str4;    //数字的字符串形式
            string str5 = "";  //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch2 = "";    //数字位的汉字读法
            int nzero = 0;  //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式
            j = str4.Length;      //找出最高位
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                var str3 = str4.Substring(i, 1);    //从原num值中取出的值
                temp = Convert.ToInt32(str3);      //转换为数字
                string ch1;    //数字的汉语读法
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;

        }
        #endregion

        /// <summary>
        /// Json字符串转List数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="md"></param>
        /// <returns></returns>
        public static T ToModel<T>(this string md)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(md);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        #region 两个实体类相互赋值

        /// <summary>
        /// 两个实体类相互赋值
        /// </summary>
        /// <typeparam name="T">目标类</typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static T ToModel<T>(this object f) where T : new()
        {
            T b;
            b = Activator.CreateInstance<T>();
            var tt = f.GetType();//获取指定名称的类型
            object ff = Activator.CreateInstance(tt, null);//创建指定类型实例
            PropertyInfo[] fields = ff.GetType().GetProperties();//获取指定对象的所有公共属性

            var th =b.GetType();//获取指定名称的类型
            object fs = Activator.CreateInstance(th, null);//创建指定类型实例
            PropertyInfo[] field = fs.GetType().GetProperties();//获取指定对象的所有公共属性

            foreach (PropertyInfo t in fields)
            {
                foreach (var v in field)
                {
                    var value = t.GetValue(f, null);
                    if (v.Name== t.Name& value!=null)
                    {
                        if (!value.Equals(""))
                        {

                            if (v.PropertyType.IsGenericType)
                            {
                                Type genericTypeDefinition = v.PropertyType.GetGenericTypeDefinition();
                                if (genericTypeDefinition == typeof(Nullable<>))
                                {
                                    v.SetValue(b, string.IsNullOrEmpty(value.ToString()) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(v.PropertyType)), null);
                                }
                                continue;
                            }

                            v.SetValue(b, Convert.ChangeType(value, v.PropertyType), null);
                        }
                       // v.SetValue(b, td, null);//给对象赋值
                    }
                }
               
            }

            return b;
        }

        /// <summary>
        /// 两个实体类相互赋值
        /// </summary>
        /// <typeparam name="T">目标类</typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static List<T> ToListModel<T>(this object f) where T : new()
        {
            T b;
            var m = new List<T>();
            b = Activator.CreateInstance<T>();
            var tt = f.GetType();//获取指定名称的类型
            object ff = Activator.CreateInstance(tt, null);//创建指定类型实例

            var th = b.GetType();//获取指定名称的类型
            object fs = Activator.CreateInstance(th, null);//创建指定类型实例
            PropertyInfo[] field = fs.GetType().GetProperties();//获取指定对象的所有公共属性
            var mod = f.GetType().GetGenericArguments();
            foreach (var t in mod)
            {
                //System.Type to = t.PropertyType.GetProperties();//获取指定名称的类型
              //  var md = t.GetMethod("Find").ReturnType.GetProperties();
                        
                foreach (PropertyInfo mn in t.GetProperties())
                {
                    foreach (var v in field)
                    {
                        t.InvokeMember("List",
                            BindingFlags.GetProperty, null, f, null);
                        var value = mn.GetValue(t, null);
                        if (v.Name == mn.Name&value!=null)
                        {
                            if (!value.Equals(""))
                            {

                                if (v.PropertyType.IsGenericType)
                                {
                                    Type genericTypeDefinition = v.PropertyType.GetGenericTypeDefinition();
                                    if (genericTypeDefinition == typeof (Nullable<>))
                                    {
                                        v.SetValue(b,
                                            string.IsNullOrEmpty(value.ToString())
                                                ? null
                                                : Convert.ChangeType(value, Nullable.GetUnderlyingType(v.PropertyType)),
                                            null);
                                    }
                                    m.Add(b);
                                    continue;
                                }

                                v.SetValue(b, Convert.ChangeType(value, v.PropertyType), null);
                                m.Add(b);
                            }
                            // v.SetValue(b, td, null);//给对象赋值
                        }
                    }
                }

            }

            return m;
        }



        /// <summary>
        /// 复制实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CloneObject<T>(this object obj)
        {
            using (MemoryStream s = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, obj);
                s.Position = 0;
                return (T)bf.Deserialize(s);
            }
        }
        /// <summary>
        /// 复制实体类-不考虑是否可以序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyTo<T>(this object obj)
        {
            if (obj != null)
            {
                var json = JsonConvert.SerializeObject(obj);
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }

        #endregion
        /// <summary>
        /// 返回日期格式中的英文短月份
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateShort(this DateTime date)
        {
            var myDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            return myDtfi.GetAbbreviatedMonthName(date.Month);
        }
        /// <summary>
        /// 把数字转为标准的数字日期
        /// </summary>
        /// <returns></returns>
        public static string ToDateEntocn(this string date)
        {
            try
            {
                date = date.Trim();
                if (string.IsNullOrEmpty(date)||date[1]>127)
                {
                    return "";
                }
                else if (!date.Contains("-"))
                {
                    var bl = 0;
                    if (!int.TryParse(date, out bl))
                    {
                        return "";
                    }
                }
                if (date.Length != 8)
                {
                    if (date.Length != 5 & date.Length != 4) return "";
                    if (date.Length == 4)
                    {
                        date = "0" + date;
                    }
                    date = DateTime.Now.ToString("yy") + date.Substring(2) + date.Substring(0, 2);
                    return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                }
                else if(date.Length<14)
                {
                    //date = DateTime.Now.ToString("yy") + date.Substring(4) + date.Substring(0, 2);
                    return DateTime.ParseExact(date,"yyyyMMdd",null).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.ParseExact(date, "yyyyMMdd HH:mm:ss", null).ToString(CultureInfo.InvariantCulture);
                }
              
            }
            catch (Exception)
            {

                return "";
            }
        }
        public static DateTime ToWeekUpOfDate(this DateTime dt, DayOfWeek weekday, int number)
        {
            int wd1 = (int)weekday;
            int wd2 = (int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * number) : dt.AddDays(7 * number - wd2 + wd1);
        }

        #region 强制转化
        /// <summary>
        /// object转化为Bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object obj)
        {
            bool flag;
            if (obj == null)
            {
                return false;
            }
            if (obj.Equals(DBNull.Value))
            {
                return false;
            }
           
            if(obj.ToString().ToLower().Trim()=="true")
            {
                return true;
            }
            if (obj.ToString().ToLower().Trim() == "false")
            {
                return false;
            }
           
            if (obj.ObjToInt() >= 1)
            {
                return true;
            }
            if (obj.ObjToInt() <= 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// object转化为Bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ObjAlToBool(this object obj)
        {
            bool flag;
            if (obj == null)
            {
                return false;
            }
            if (obj.Equals(DBNull.Value))
            {
                return false;
            }

            if (obj.ToString().ToLower().Trim() == "true")
            {
                return true;
            }
            if (obj.ToString().ToLower().Trim() == "false")
            {
                return false;
            }

            if (obj.ObjToInt() >= 1)
            {
                return true;
            }
            if (obj.ObjToInt() <= 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// object强制转化为DateTime类型(吃掉异常)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ObjToDateNull(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ObjToStr()))
            {
                return null;
            }
            try
            {
                return new DateTime?(Convert.ToDateTime(obj));
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// object强制转化为DateTime类型(吃掉异常)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <summary>
        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }
        /// <summary>
        /// 时间截转换为标准时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToStampDateTime(this string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp+"0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        ///// <summary>
        ///// 清空缓存--一般情况请不要使用此方法
        ///// </summary>
        ///// <param name="x"></param>
        //public static void Clear(this Cache x)
        //{
        //    List<string> cacheKeys = new List<string>();
        //    IDictionaryEnumerator cacheEnum = x.GetEnumerator();
        //    while (cacheEnum.MoveNext())
        //    {
        //        cacheKeys.Add(cacheEnum.Key.ToString());
        //    }
        //    foreach (string cacheKey in cacheKeys)
        //    {
        //        x.Remove(cacheKey);
        //    }
        //}

        /// <summary>
        /// int强制转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ObjToInt(this object obj)
        {
            if (obj != null)
            {
                if (obj.ObjToStr().Contains("男"))
                {
                    return 1;
                }
                if (obj.ObjToStr().Contains("女"))
                {
                    return 0;
                }
                int num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (int.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }
        public static DateTime ToWeekUpOfDate(this DateTime dt, DayOfWeek weekday)
        {
            int wd1 = (int)weekday;
            int wd2 = (int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * 0) : dt.AddDays(7 * 0 - wd2 + wd1);
        }
        /// <summary>
        /// List转XML
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XmlDocument ListToXml(this object obj)
        {
            try
            {

            var strs = @"{
  ""?xml"": {
    ""@version"": ""1.0"",
    ""@standalone"": ""no""
  },
  ""root"": ";
            if (obj.ObjToStr().Contains("System.Collections")||!obj.ObjToStr().Contains(":"))
            {
                var js = JsonConvert.SerializeObject(obj);
                if (js.Length > 0 && js.Substring(0, 1).Equals("["))
                {
                    js = "{\"Listd\":" + js + "}";
                }
                var json = strs + js + "}";
                return JsonConvert.DeserializeXmlNode(json);
            }
                obj.ObjToStr();
            var kl= strs + obj.ObjToStr()+ "}";
            return JsonConvert.DeserializeXmlNode(kl);

            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// int强制转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 ObjToInt64(this object obj)
        {
            if (obj != null)
            {
                Int64 num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (Int64.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }

        /// <summary>
        /// 强制转化为long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ObjToLong(this object obj)
        {
            
            if (obj != null)
            {
                long num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (long.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }
        /// <summary>
        /// 强制转化可空int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ObjToIntNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            int dl;
            int.TryParse(obj.ObjToStr(), out dl);
            return dl;
            
        }
        /// <summary>
        /// 强制转化为string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToStr(this object obj)
        {
            try
            {

            
            if (obj == null)
            {
                return "";
            }
            if (obj.Equals(DBNull.Value))
            {
                return "";
            }
            return  Convert.ToString(obj);
            }
            catch (Exception)
            {

                return "";
            }
        }
        /// <summary>
        /// Decimal转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object obj)
        {
            Decimal dl;
            Decimal.TryParse(obj.ObjToStr(), out dl);
            return dl;
        }
        /// <summary>
        /// Decimal可空类型转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ObjToDecimalNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new decimal?(ObjToDecimal(obj));
        }
        #endregion

        #region 将List转换成DataTable
        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        public static DataTable MdToDataTable<T>(this List<T> data)
        {
            try
            {
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(data));
                return dt;
            }
            catch (Exception)
            {

                return null;
            }
           
        }
        #endregion

        #region 将List转换成DataTable
        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToTable<T>(this T data)
        {
            var datas = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(data));
            return datas;
        }
        //public static DataTable ToTable(this object data)
        //{
        //    var datas = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(data));
        //    return datas;
        //}
        #endregion



        #region 将T类型转为List
        /// <summary>
        /// 将T类型转为List
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="table">传入Table</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table)
        {
            List<T> list = new List<T>();
            try
            {
                T t;
                PropertyInfo[] propertypes;
                string tempName;
                foreach (DataRow row in table.Rows)
                {
                    t = Activator.CreateInstance<T>();
                    propertypes = t.GetType().GetProperties();
                    foreach (PropertyInfo pro in propertypes)
                    {
                        tempName = pro.Name;
                        if (table.Columns.Contains(tempName))
                        {
                            object value = row[tempName].ToString().TrimEnd(',').TrimStart(',').Trim();
                            if (!value.Equals(""))
                            {

                                if (pro.PropertyType.IsGenericType)
                                {
                                    Type genericTypeDefinition = pro.PropertyType.GetGenericTypeDefinition();
                                    if (genericTypeDefinition == typeof(Nullable<>))
                                    {
                                        pro.SetValue(t, string.IsNullOrEmpty(value.ToString()) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(pro.PropertyType)), null);
                                    }
                                    continue;
                                }

                                pro.SetValue(t, Convert.ChangeType(value, pro.PropertyType), null);
                            }
                        }
                    }
                    list.Add(t);
                }
            }
            catch
            {
                //list.Clear();
            }
            return list;
        }
        #endregion

        #region 流的转化
        /// <summary>
        /// 将流进行转换，以便能正常读取Lenth保存到本地
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static MemoryStream StreamCopyTo(this Stream str)
        {
            var  strem= new MemoryStream();
            const int bufferSize = 4096;
            var buffer = new byte[bufferSize];
            while (true)
            {
                int read = str.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                strem.Write(buffer, 0, read);
            }
            return strem;
        }
        /// <summary>
        /// 保存流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dirPath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool SaveFile(this Stream stream, string dirPath, string filename)
        {
            bool flag = false;
            StreamWriter objwrite = null;
            string path;
            try
            {
                if (!Directory.Exists(dirPath))//如果目录不存在则创建
                {
                    Directory.CreateDirectory(dirPath);
                }
                
                byte[] by=new byte[stream.Length];
                stream.Read(by,0,stream.Length.ObjToInt());
                stream.Seek(0, SeekOrigin.Begin); 
                path = dirPath + filename;
                objwrite = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
                objwrite.BaseStream.Write(by,0,by.Length);
               
                objwrite.Close();
                flag = true;
            }
            catch(Exception)
            {
                if (objwrite != null)
                    objwrite.Close();
            }
            return flag;
        }
        public static string ToString(this long num, int toBase)
        {
            if (num == 0) return "0";

           
            String str = "0123456789abcdefghijklmnopqrstuvwxyz";
            if (toBase > str.Length)
                throw new IndexOutOfRangeException();

            var numList = new List<char>();


            do
            {
                var remainder = num % toBase;
                numList.Add(str[remainder.ObjToInt()]);

                num = num / toBase;

                if (num != 0) continue;

                numList.Reverse();
                return new string(numList.ToArray());
            } while (true);
        }

        /// <summary>
        /// 保存流Byte
        /// </summary>
        /// <param name="by"></param>
        /// <param name="dirPath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool ByteToSaveFile(this byte[] by, string dirPath, string filename)
        {
            bool flag = false;
            StreamWriter objwrite = null;
            string path;
            try
            {
                if (!Directory.Exists(dirPath))//如果目录不存在则创建
                {
                    Directory.CreateDirectory(dirPath);
                }

                path = dirPath + filename;
                objwrite = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
                objwrite.BaseStream.Write(by, 0, by.Length);

                objwrite.Close();
                flag = true;
            }
            catch
            {
                if (objwrite != null)
                    objwrite.Close();
            }
            return flag;
        }
        #endregion


        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(this string str, string type)
        {
            return str.Substring(0, str.LastIndexOf(type, StringComparison.Ordinal));
        }

       

        #region 检查model的字段是否有空的字段
            /// <summary>
            /// 检查model的字段是否有空的字段
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="tm">传递的model</param>
            /// <param name="str">排除检查字段</param>
            /// <returns></returns>
        public static bool ToCeck<T>(this T tm, string[] str = null)
        {
            var prs = tm.GetType().GetProperties();
            return prs.Where(p => str != null && !str.Contains(p.Name)).All(p =>string.IsNullOrEmpty(p.GetValue(tm, null).ObjToStr()));
        }
        #endregion
        #region 根据提供的model和排除的string[]来组合sql语句
        /// <summary>
        /// 根据提供的model和排除的string[]来组合sqlWhere语句,排除字段里多出“|”代表用模糊查询
        /// $代表有日期段，$开始 $$结束
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tm"></param>
        /// <param name="str">排除的model字段</param>
        /// <returns></returns>
        public static string ToSql<T>(this T tm, string[] str = null)
        {
            var prs = tm.GetType().GetProperties();
            var sql = new StringBuilder();
            foreach (var p in prs.Where(p => str != null && !str.Contains(p.Name, StringComparer.CurrentCultureIgnoreCase) & p.GetValue(tm, null).ObjToStr() != ""))
            {
                if (sql.Length > 0) sql.AppendLine(" and ");
                if (p.Name.Equals("Startdate", StringComparison.CurrentCultureIgnoreCase))
                {
                    sql.AppendLine(p.Name + ">='" + p.GetValue(tm, null) + "'");
                    continue;
                }
                if (p.Name.Equals("endate", StringComparison.CurrentCultureIgnoreCase) ||
                    p.Name.Equals("enddate", StringComparison.CurrentCultureIgnoreCase))
                {
                    sql.AppendLine(p.Name + "<='" + p.GetValue(tm, null) + "'");
                    continue;
                }
                if (str != null && str.Contains(p.Name + "|", StringComparer.CurrentCultureIgnoreCase))
                {
                    sql.AppendLine(p.Name + " like '%" + p.GetValue(tm, null) + "%'");

                }
                else if (str != null && str.Contains(p.Name + "$", StringComparer.CurrentCultureIgnoreCase))
                {
                   // var sl = prs.Where(l => l.Name.Contains("$")).ToList();
                    sql.AppendLine(p.Name + " between " + p.GetValue(tm, null) + " ");
                }
                else
                {
                    sql.AppendLine(p.Name + "='" + p.GetValue(tm, null) + "'");
                }
            }
            return sql.ObjToStr();
        }
        /// <summary>
        /// 根据提供的model和排除的string[]来组合sqlWhere语句,排除字段里多出“|”代表用模糊查询
        /// $代表有日期段，$开始 $$结束
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tm"></param>
        /// <param name="str">排除的model字段</param>
        /// <returns></returns>
        public static string ToNewSql<T>(this T tm, string[] str = null)
        {
            var prs = tm.GetType().GetProperties();
            var sql = new StringBuilder();
            foreach (var p in prs.Where(p => str != null && !str.Contains(p.Name, StringComparer.CurrentCultureIgnoreCase) & p.GetValue(tm, null).ObjToStr() != ""))
            {
                if (sql.Length > 0) sql.AppendLine(" and ");
                if (p.Name.Equals("Startdate", StringComparison.CurrentCultureIgnoreCase))
                {
                    sql.AppendLine(p.Name + ">='" + p.GetValue(tm, null) + "'");
                    continue;
                }
                if (p.Name.Equals("endate", StringComparison.CurrentCultureIgnoreCase) ||
                    p.Name.Equals("enddate", StringComparison.CurrentCultureIgnoreCase))
                {
                    sql.AppendLine(p.Name + "<='" + p.GetValue(tm, null) + "'");
                    continue;
                }
                if (str != null && str.Contains(p.Name + "|", StringComparer.CurrentCultureIgnoreCase))
                {
                    sql.AppendLine(p.Name + " like '%" + p.GetValue(tm, null) + "%'");

                }
                else if (str != null && str.Contains(p.Name + "$", StringComparer.CurrentCultureIgnoreCase))
                {
                    var sl = prs.Where(l => l.Name.Contains(p.Name)).ToList();
                    string sl1;
                    string sl2;
                    if (sl[0].Name.Contains("2"))
                    {
                        sl1 = sl[1].GetValue(tm, null).ToString();
                        sl2 = sl[0].GetValue(tm, null).ToString();
                    }
                    else
                    {
                        sl1 = sl[0].GetValue(tm, null).ToString();
                        sl2 = sl[1].GetValue(tm, null).ToString();
                    }
                    sql.AppendLine(p.Name + " between '" + sl1 + "' and '"+ sl2+ "'");
                }
                else
                {
                    sql.AppendLine(p.Name + "='" + p.GetValue(tm, null) + "'");
                }
            }
            return sql.ObjToStr();
        }
        /// <summary>
        /// 获取该实体的属性
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static PropertyInfo ToPinfo(this Type obj)
        {

            var t = obj.GetProperties();
            return t[0];
        }



        #endregion
        #region 根据提供的model和排除的string[]来组合sql语句
        /// <summary>
        /// 根据提供的model和排除的string[]来组合sql完整语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tm"></param>
        /// <param name="str">排除的model字段</param>
        /// <returns></returns>
        public static string ToSqlD<T>(this T tm, string[] str = null)
        {
            var prs = tm.GetType().GetProperties();
            var sql = new StringBuilder();
            foreach (var p in prs.Where(p => str != null && !str.Contains(p.Name) & p.GetValue(tm, null).ObjToStr() != ""))
            {
                if (sql.Length > 0) sql.AppendLine(" and ");
                sql.AppendLine(p.Name + "='" + p.GetValue(tm, null) + "',");
            }
            sql.Insert(0, "select * from " + tm.GetType().Name + " where ");
            return sql.ObjToStr().TrimEnd(',');
        }
        #endregion

        #region 数据库操作提示
        public static string ToMess(this int str)
        {
            return str > 0 ? "添加成功！" : "添加失败！";
        }
        public static string ToMess(this bool str)
        {
            return str ? "更新成功！" : "更新失败";
        }
        public static string ToDelMess(this bool str)
        {
            return str ? "删除成功！" : "删除失败";
        }
        #endregion
    }
}
