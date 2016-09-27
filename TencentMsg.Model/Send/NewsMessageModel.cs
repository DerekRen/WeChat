using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TencentMsg.Model.Send
{
    /// <summary>
    /// 图文消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class NewsMessageModel:MsgBaseModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public NewsMessageModel()
        {
            this.msgtype = "news";
            this.news = new NewsModel();
        }
        //[XmlArray("news"), XmlArrayItem("articles")]
        /// <summary>
        /// 发送图文消息
        /// </summary>
        public NewsModel news { get; set; }

    }
    /// <summary>
    /// 发送图文消息(处理发送微信的json数据格式)
    /// </summary>
    public class NewsModel
    {
        public NewsModel()
        {
            this.articles = new List<NewsArticles>();
        }
        /// <summary>
        /// 图文内容
        /// </summary>
        public List<NewsArticles> articles { get; set; }
    }
    /// <summary>
    /// 图文内容
    /// </summary>
    public class NewsArticles
    {
        /// <summary>
        /// 图文标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图文描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl { get; set; }
    }
}
