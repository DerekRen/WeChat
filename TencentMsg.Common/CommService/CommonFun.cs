using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Net;

namespace TencentMsg.Common.CommService
{
    public class CommonFun
    {
        /// <summary>
        /// 时间戳转为DateTime时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间转换为时间戳
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public static string ApplicationFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["File"] + "/";
            }
        }
        /// <summary>
        /// 文件物理路径
        /// </summary>
        public static string PhysicalFilePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath(ApplicationFilePath) + "/";
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="saveRootFolder"></param>
        /// <param name="msg"></param>
        /// <param name="fileName"></param>
        public static void FileUpload(HttpPostedFile inputFile, string saveRootFolder, ref string msg, ref string newFileName)
        {
            try
            {
                msg = string.Empty;//错误消息
                if (!inputFile.FileName.Contains("."))
                {
                    msg = "上传的文件扩展名不正确";
                    return;
                }
                string ext = inputFile.FileName.Substring(inputFile.FileName.LastIndexOf("."));//扩展名
                newFileName = DateTime.Now.ToString("yyyy-MM-dd") + "/";
                if (!string.IsNullOrEmpty(saveRootFolder))
                {
                    newFileName = saveRootFolder + "/" + newFileName;
                }
                string FilefullPath = PhysicalFilePath + newFileName;
                //如果目录不存在，则创建相应目录. 
                if (!Directory.Exists(FilefullPath))
                {
                    Directory.CreateDirectory(FilefullPath);
                }
                newFileName += DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                FilefullPath = PhysicalFilePath + newFileName;
                //保存图片文件
                inputFile.SaveAs(FilefullPath);
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteExceptionLog("上传文件异常：" + ex.Message);
            }
           
        }
    }
}
