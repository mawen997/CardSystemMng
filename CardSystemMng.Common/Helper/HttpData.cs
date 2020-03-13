using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Sockets;
using System.Web;
using static System.String;

namespace Common
{
    public class HttpData
    {
        #region POST请求数据

    public  static CookieContainer cooks = new CookieContainer();//国内的
    public static CookieContainer cooknet = new CookieContainer();//国际的
    private int km = 0;

       public HttpData()
    {
        System.Net.ServicePointManager.Expect100Continue = false;
    }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Gethtml(httpconfig info)
        {
            
            ServicePointManager.Expect100Continue = false;
            HttpWebResponse response = null;
            var encoding = Encoding.GetEncoding(info.encodeType);
          //  System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            try
            {
                // 设置参数
                var request = WebRequest.Create(info.url) as HttpWebRequest;
                if (request == null) return "异常错误";
                request.Proxy = null;
                if (info.url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        CheckValidationResult;
                }

                if (info.Set_Cookie != null)
                {
                    request.Headers["Cookie"] = info.Set_Cookie;
                }
                if (info.IsPort)
                {
                    request.Proxy = new WebProxy() {Address = new Uri("http://" + info.Ip)};
                }
                else
                {
                    request.Proxy = null;
                }
                //if (info.CookieContainer != null)
                //{
                //    request.CookieContainer = info.CookieContainer;
                //}
                //if (info.cookieColletion != null)
                //{
                //    info.cookieColletion = info.cookieColletion;
                //}
                if (info.HeadKey != null)
                {
                    foreach (DictionaryEntry stw in info.HeadKey)
                    {
                        SetHeaderValue(request.Headers, stw.Key.ToString(), stw.Value.ToString());
                        //  request.Headers.Set(stw.Key.ToString(), stw.Value.ToString());
                    }
                }
                request.Timeout = info.OutTime != 0 ? info.OutTime : 80000;
                request.AllowAutoRedirect = info.AllowAutoRedirect;
                request.Method = info.Method;
                if (info.Origin != null)
                {
                    request.Headers["Origin"] = info.Origin;
                }
                request.Referer = info.Referer;
                if (info.HeadKey != null)
                {
                    if (info.HeadKey.Contains("Accept-Language"))
                    {
                    }
                    else
                    {
                        request.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8"); //很重要
                    }
                    if (info.HeadKey.Contains("Accept-Encoding"))
                    {
                        request.Headers.Add(HttpRequestHeader.AcceptEncoding, info.HeadKey["Accept-Encoding"].ToString());
                    }
                }

                request.Accept = info.Accept;
                request.UserAgent = info.UserAgent;
                request.ContentType = info.ContentType;

                if (info.Method == "POST")
                {
                    var data = encoding.GetBytes(info.postdata);
                    request.ContentLength = data.Length;
                    var outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);
                    outstream.Close();
                }
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                if (info.cookieoff == 0 & response.Headers["set-cookie"] != null & response.Headers["set-cookie"] != "")
                {
                    info.OutCookies = response.Headers["set-cookie"];
                }
               // request.GetResponse();
                //直到request.GetResponse()程序才开始向目标网页发送Post请求        
                var instream = response.GetResponseStream();
                var sr = new StreamReader(instream, encoding);
                if (!IsNullOrEmpty(response.Headers["Location"]))
                {
                    info.Location = response.Headers["Location"];
                }
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (System.IO.Compression.GZipStream stream =
                        new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream, encoding))
                        {

                            return reader.ReadToEnd();
                        }
                    }
                }
                var hm = sr.ReadToEnd();
                return hm;
            }
            catch (Exception ex)
            {
                km++;
                if (km > 2)
                {
                    return ex + "|ErrContent";
                }
                return Gethtml(info);
            }
            finally
            {
                // ReSharper disable once ConstantConditionalAccessQualifier
                response?.Close();
            }
            

        }
        public  string Cookiegs = "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookie">网站的cookie</param>
        /// <param name="post">请求的方式--POST或GET</param>
        /// <param name="encodeType">网站的编码</param>
        /// <param name="postData">请求的数据</param>
        /// <returns></returns>
        public string Gethtmltb(out CookieContainer cookss,string url, string postData = "", 
            CookieContainer cookks = null, 
            string cookie = null, string post = "GET", 
            string encodeType = "utf-8")
        {
            ServicePointManager.Expect100Continue = false;
            Encoding encoding = Encoding.GetEncoding(encodeType);
            try
            {
                // 设置参数
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (cookie != null)
                {
                    request.Headers["Set-Cookie"] = cookie;
                }
               
                request.CookieContainer = cookks;
                request.AllowAutoRedirect = true;
                request.Method = post;
                //request.Headers["Origin"] = "http://www.jinri.cn";
                request.Proxy = null;
                request.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8");//很重要
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.2)";
                request.ContentType = "application/x-www-form-urlencoded";

                if (post == "POST")
                {
                    byte[] data = encoding.GetBytes(postData);
                    request.ContentLength = data.Length;
                    var outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);
                    outstream.Close();
                }
                //发送请求并获取相应回应数据
                var response = request.GetResponse() as HttpWebResponse;
                string hj = response.Headers["set-cookie"];
                //直到request.GetResponse()程序才开始向目标网页发送Post请求        
                var instream = response.GetResponseStream();
                if (instream != null)
                {
                    var sr = new StreamReader(instream, encoding);
                    cookss = cookks;
                    string hm = sr.ReadToEnd();
                    return hm;
                }
                else
                {
                    cookss = null;
                    return "";
                }
            }
            catch (Exception ex)
            {
                cookss = null;
                return ex.ToString();

            }


        }

        /// <summary>
        /// 返回数据流，主要用于下载xls文件
        /// </summary>
        /// <returns></returns>
        public byte[] GetDataHtml(httpconfig info)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            HttpWebResponse response = null;
            Encoding encoding = Encoding.GetEncoding(info.encodeType);

            try
            {
                // 设置参数
                var request = WebRequest.Create(info.url) as HttpWebRequest;
                if (request == null) return null;
                request.Proxy = null;
                if (info.Set_Cookie != null )
                {
                    request.Headers["Cookie"] = info.Set_Cookie;
                }
                //if (info.CookieContainer != null)
                //{
                //    request.CookieContainer = info.CookieContainer;
                //}
                //if (info.cookieColletion != null)
                //{
                //    info.cookieColletion = info.cookieColletion;
                //}
                if (info.OutTime > 0)
                {
                    request.Timeout = info.OutTime;
                }
                if (info.HeadKey != null)
                {
                    foreach (DictionaryEntry stw in info.HeadKey)
                    {
                        SetHeaderValue(request.Headers, stw.Key.ToString(), stw.Value.ToString());
                        //  request.Headers.Set(stw.Key.ToString(), stw.Value.ToString());
                    }
                }
                
                request.AllowAutoRedirect = info.AllowAutoRedirect;
                request.Method = info.Method;
                if (info.Origin != null)
                {
                    request.Headers["Origin"] = info.Origin;
                }
                request.Referer = info.Referer;
                request.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8"); //很重要
                request.Accept = info.Accept;
                request.UserAgent = info.UserAgent;
                request.ContentType = info.ContentType;
                if (info.Method == "POST")
                {
                    byte[] data = encoding.GetBytes(info.postdata);
                    request.ContentLength = data.Length;
                    var outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);
                    outstream.Close();
                }
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                if (response!=null&&info.cookieoff == 0 & response?.Headers["set-cookie"] != null & response.Headers["set-cookie"] != "")
                {
                    info.OutCookies = response.Headers["set-cookie"];
                }
                info.Filename = response?.Headers["Content-Disposition"];
                if (!IsNullOrEmpty(info.Filename))
                {
                    try
                    {
                        info.Filename=Path.GetExtension(info.Filename.Replace("\"", ""));
                    }
                    catch (Exception)
                    {

                        info.Filename = "";
                    }
                   
                }
                var strd=new MemoryStream();
                const int bufferSize = 512;
                var bytes = new byte[bufferSize];
                using (var stream = response?.GetResponseStream())
                {
                    try
                    {
                        if (stream != null)
                        {
                            var length = stream.Read(bytes, 0, bufferSize);

                            while (length > 0)
                            {
                                strd.Write(bytes, 0, length);
                                length = stream.Read(bytes, 0, bufferSize);
                            }
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                return strd.ToArray();
                //string hj = response.Headers["set-cookie"];
                ////直到request.GetResponse()程序才开始向目标网页发送Post请求        
                //instream = response.GetResponseStream();
                //sr = new StreamReader(instream, encoding);

                //string hm = sr.ReadToEnd();
                //return hm;
            }
            catch (Exception ex)
            {
                km++;
                if (km > 3)
                {
                    return null;
                }
               // response.Close();
                return GetDataHtml(info);

            }
            finally
            {
                // ReSharper disable once ConstantConditionalAccessQualifier
                response?.Close();
            }

        } 
        #endregion

        /// <summary>
        /// 国际
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse GetdataHtml(httpconfig info)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            Encoding encoding = Encoding.GetEncoding(info.encodeType);
            try
            {
                // 设置参数
                var request = WebRequest.Create(info.url) as HttpWebRequest;
                if (request == null) return null;
                if (info.OutCookies != null & info.CookieContainer==null)
                {
                    request.Headers["Cookie"] = info.OutCookies;
                }
                if (info.CookieContainer != null)
                {
                    request.CookieContainer = info.CookieContainer;
                }
                if (info.cookieColletion != null)
                {
                    info.cookieColletion = info.cookieColletion;
                }
                request.AllowAutoRedirect = info.AllowAutoRedirect;
                request.Method = info.Method;
                if (info.HeadKey != null)
                {
                    foreach (DictionaryEntry stw in info.HeadKey)
                    {
                        SetHeaderValue(request.Headers, stw.Key.ToString(), stw.Value.ToString());
                        //  request.Headers.Set(stw.Key.ToString(), stw.Value.ToString());
                    }
                }
                request.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8");//很重要
                request.Accept = info.Accept;
                request.Referer = info.Referer;
                request.UserAgent = info.UserAgent;
                request.ContentType = info.ContentType;
                request.Headers.Set("Accept-Encoding", "gzip,deflate,sdch");

                if (info.Method == "POST")
                {
                    byte[] data = encoding.GetBytes(info.postdata);
                    request.ContentLength = data.Length;
                    var outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);
                    outstream.Close();
                }
                //发送请求并获取相应回应数据
                
                // response = (HttpWebResponse)request.GetResponse();
               var response = request.GetResponse() as HttpWebResponse;
                if (info.cookieoff != 0) return response;
                if (response != null) info.OutCookies = response.Headers["Set-Cookie"];
                //string hj = response.Headers["set-cookie"];
                ////直到request.GetResponse()程序才开始向目标网页发送Post请求        
                //instream = response.GetResponseStream();
              //  sr = new StreamReader(instream, encoding);

                return response;
            }
            catch (Exception ex)
            {
                info.timcs++;
                return info.timcs > 3 ? null : GetdataHtml(info);
            }


        }
        

        public string Yzmtext = null;
        private bool CheckValidationResult(object sender,
        X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;// Always accept
        }


        public object Img(httpconfig info)
        {
            var tm = 0;
            var t = "";
            System.Net.ServicePointManager.DefaultConnectionLimit= 50;
            gt:
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
               
              
                request = WebRequest.Create(info.url) as HttpWebRequest;
                if (info.url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            CheckValidationResult;
                }
                request.ServicePoint.Expect100Continue = false;

                request.ServicePoint.UseNagleAlgorithm = false;
                request.ServicePoint.ConnectionLimit = 65500;
                request.AllowWriteStreamBuffering = false; request.Proxy = null;

                request.Proxy = null;
                if (info.Set_Cookie != null)
                {
                    if (request != null) request.Headers["Cookie"] = info.Set_Cookie.Replace("HttpOnly","");
                }
                if (info.CookieContainer == null)
                {
                    info.CookieContainer = new CookieContainer();
                }
                if(info.cookieColletion!=null||info.cookieoff==1)
                {
                    info.cookieColletion = info.cookieColletion;
                }
                var tms = new Stopwatch();
                if (request != null)
                {
                    request.AllowAutoRedirect = info.AllowAutoRedirect;
               
                    if (info.HeadKey != null)
                    {
                        foreach (DictionaryEntry stw in info.HeadKey)
                        {
                            SetHeaderValue(request.Headers, stw.Key.ToString(), stw.Value.ToString());
                            //  request.Headers.Set(stw.Key.ToString(), stw.Value.ToString());
                        }
                    }
                    if (!t.Contains("参数"))
                    {
                        SetHeaderValue(request.Headers, "Accept-Encoding", "gzip, deflate, sdch");
                    }
                    // request.Method=info.
                    if (info.Referer!=null)
                    {
                        request.Referer = info.Referer;
                    }
                    request.Accept = info.Accept;
                    request.UserAgent = info.UserAgent;
                    request.ContentType = info.ContentType;
                 
                    tms.Start();
                    response = request.GetResponse() as HttpWebResponse;
                    
                }
                if (info.cookieoff == 0)
                {
                    if (response != null) info.OutCookies = response.Headers["Set-Cookie"];
                }
                if (response != null)
                {
                    var bm = response.GetResponseStream();
                    info.OutImag = new Bitmap(bm);
                }
                tms.Stop();
                //if (HttpContext.Current != null)
                //{
                //    ("\r\n----------------------------------\r\nUrl：" + info.url + "\r\n平台名称：请求的时间\r\n" +
                //     "耗时：" + tms.Elapsed.ToString()).FileWrite(HttpContext.Current.Request.MapPath("~/Log/YzmLog/") +
                //                                               "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                //    info.Imgstream = new MemoryStream();
                //    info.OutImag.Save(info.Imgstream, ImageFormat.Jpeg);
                //    request?.Abort();
                //    response?.Close();
                //}
                return null;
            }
            catch(Exception ex)
            {
                request?.Abort();
                response?.Close();
                t = ex.Message;
                //("\r\n-----------------------------------------------\r\n" +
                // "Url：" + info.url + "\r\n错误信息：" + ex.ToString() + "\r\n").FileWrite(
                //    HttpContext.Current.Request.MapPath("~/Log/YzmLog/") + "\\Err" + DateTime.Now.ToString("yyyy-MM-dd") +
                //    ".txt");
                if (ex.ToString().Contains("参数"))
                {
                    tm++;
                    if (tm > 5)
                    {
                        return null;
                    }
                    goto gt;
                }
                return null;
            }
        }
     
        //解决 “Keep-Alive 和 Close 不能使用此属性设置 的问题
        public  void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
           // typeof(WebHeaderCollection)
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                if (collection != null) collection[name] = value;
            }

        }


        public System.Net.CookieContainer cookks { get; set; }
    }
    public class httpconfig
    {
        /// <summary>
        /// 是否使用代理IP
        /// </summary>
        public bool IsPort { get; set; }
        /// <summary>
        /// 代理IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 超时重新提交的次数
        /// </summary>
        public int timcs { get; set; }
        public string Origin { get; set; }
        public string Referer { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public CookieCollection cookieColletion { get; set; }
        public bool AllowAutoRedirect { get; set; }
       
        public string url { get; set; }
        /// <summary>
        /// 控制是否获取cookie，string，0代表获取，1代表不获取
        /// </summary>
        public int cookieoff { get; set; }
        /// <summary>
        ///  传送的数据
        /// </summary>
        public string postdata { get; set; }
        public string _encodeType { get; set; }
        public string encodeType
        {
            get
            {
                if(_encodeType==null)
                {
                    return "utf-8";
                }
                else
                {
                    return _encodeType;
                }
            }
            set
            {
                _encodeType = value;
            }
        }
        
        public string Set_Cookie { get; set; }
        public Hashtable HeadKey =new Hashtable();
        public string OutCookies { get; set; }
       
        public string OutHtml { get; set; }
        public string _Method { get; set; }
      /// <summary>
      /// 默认GET
      /// </summary>
        public string Method 
        {
            get
            {
                if(_Method==null)
                {
                    return "GET";
                }
                else
                {
                    return _Method;
                }
            }
            set
            {
                _Method = value;
            }
        }
        public string _Accept { get; set; }
        /// <summary>
        /// 默认text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
        /// </summary>
        public string Accept
        {
            get
            {
                if (_Accept == null)
                {
                    return "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                }
                else
                {
                    return _Accept;
                }
            }
            set
            {
                _Accept = value;
            }
        }

        public string _UserAgent { get; set; }
        /// <summary>
        /// 默认 Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.101 Safari/537.36
        /// </summary>
        public string UserAgent
        {
            get
            {
                if(_UserAgent==null)
                {
                    return "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36";
                }
                else
                {
                    return _UserAgent;
                }
            }
            set
            {
                _UserAgent = value;
            }
        }
        public Bitmap OutImag { get; set; }
        public MemoryStream Imgstream { get; set; }
        public string _ContentType { get; set; }
        public string ContentType
        {
            get
            {
                if(_ContentType==null)
                {
                    return "application/x-www-form-urlencoded";
                }
                else
                {
                    return _ContentType;
                }
            }
            set
            {
                _ContentType = value;
            }
        }
        public HttpData HttpSend=new HttpData();
        public string Location { get; set; }
        public bool IsErrCs { get;  set; }
        public int OutTime { get;  set; }
        public string Connection { get;  set; }
        public string Filename { get;  set; }
    }



   public class WebToolkit
    {
        /// <summary>
        /// Url结构
        /// </summary>
        struct UrlInfo
        {
            public string Host;
            public int Port;
            public string File;
            public string Body;
        }

        /// <summary>
        /// 解析URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static UrlInfo ParseURL(string url)
        {
            UrlInfo urlInfo = new UrlInfo();
            string[] strTemp = null;
            urlInfo.Host = "";
            urlInfo.Port = 80;
            urlInfo.File = "/";
            urlInfo.Body = "";
            int intIndex = url.ToLower().IndexOf("http://");
            if (intIndex != -1)
            {
                url = url.Substring(7);
                intIndex = url.IndexOf("/");
                if (intIndex == -1)
                {
                    urlInfo.Host = url;
                }
                else
                {
                    urlInfo.Host = url.Substring(0, intIndex);
                    url = url.Substring(intIndex);
                    intIndex = urlInfo.Host.IndexOf(":");
                    if (intIndex != -1)
                    {
                        strTemp = urlInfo.Host.Split(':');
                        urlInfo.Host = strTemp[0];
                        int.TryParse(strTemp[1], out urlInfo.Port);
                    }
                    intIndex = url.IndexOf("?");
                    if (intIndex == -1)
                    {
                        urlInfo.File = url;
                    }
                    else
                    {
                        strTemp = url.Split('?');
                        urlInfo.File = strTemp[0];
                        urlInfo.Body = strTemp[1];
                    }
                }
            }
            return urlInfo;
        }

        /// <summary>
        /// 发出请求并获取响应
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="body"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private static string GetResponse(string host, int port, string body, Encoding encode)
        {
            string strResult = Empty;
            byte[] bteSend = Encoding.ASCII.GetBytes(body);
            byte[] bteReceive = new byte[1024];
            int intLen = 0;

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Connect(host, port);
                    if (socket.Connected)
                    {
                        socket.Send(bteSend, bteSend.Length, 0);
                        while ((intLen = socket.Receive(bteReceive, bteReceive.Length, 0)) > 0)
                        {
                            strResult += encode.GetString(bteReceive, 0, intLen);
                        }
                    }
                    socket.Close();
                }
                catch { }
            }

            return strResult;
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Get(string url,string head, Encoding encode)
        {
            UrlInfo urlInfo = ParseURL(url);
            string strRequest = Format("GET {0}?{1} HTTP/1.1\r\nHost:{2}:{3}\r\nConnection:Close\r\n\r\n", urlInfo.File, urlInfo.Body, urlInfo.Host, urlInfo.Port.ToString());
            if (IsNullOrEmpty(head))
            {
                strRequest = Format("GET {0}?{1} HTTP/1.1\r\nHost:{2}:{3}\r\n{4}\r\n\r\n", urlInfo.File, urlInfo.Body, urlInfo.Host, urlInfo.Port.ToString(),head);
            }
            return GetResponse(urlInfo.Host, urlInfo.Port, strRequest, encode);
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Post(string url,string head, Encoding encode)
        {
            UrlInfo urlInfo = ParseURL(url);
            string strRequest = Format("POST {0} HTTP/1.1\r\nHost:{1}:{2}\r\nContent-Length:{3}\r\nContent-Type:application/x-www-form-urlencoded\r\nConnection:Close\r\n\r\n{4}", urlInfo.File, urlInfo.Host, urlInfo.Port.ToString(), urlInfo.Body.Length, urlInfo.Body);
            if (!IsNullOrEmpty(head))
            {
                strRequest = Format("POST {0} HTTP/1.1\r\nHost:{1}:{2}\r\nContent-Length:{3}\r\n{5}\r\n{4}", urlInfo.File, urlInfo.Host, urlInfo.Port.ToString(), urlInfo.Body.Length, urlInfo.Body,head);
            }
            return GetResponse(urlInfo.Host, urlInfo.Port, strRequest, encode);
        }
    }


}
