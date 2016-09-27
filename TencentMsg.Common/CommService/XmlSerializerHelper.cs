using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TencentMsg.Common.CommService
{
    /// <summary>
    /// xml序列化反序列化
    /// </summary>
    public class XmlSerializerHelper
    {
        /// <summary>
        /// 将对象序列化为xml字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string Serialize<T>(T obj)
        {
            //序列化实体
            var serializer = new XmlSerializer(typeof(T));
            //注意如果不设置encoding默认将输出utf-16
            //注意这儿不能直接用Encoding.UTF8如果用Encoding.UTF8将在输出文本的最前面添加4个字节的非xml内容
            var stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Encoding = new UTF8Encoding(false) }))
            {
                serializer.Serialize(writer, obj);
            }
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        /// <summary>
        /// 将流反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">流</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(Stream stream)
        {
            return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
        }

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sourceData">源字符串</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(string sourceData)
        {
            StringReader reader = new StringReader(sourceData);
            return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        }

        /// <summary>
        /// 生成XML，不包含声明和命名空间
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string SerializeOnlyXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Indent = true;
            //settings.NewLineChars = "\r\n";
            settings.Encoding = new UTF8Encoding(false);
            //settings.IndentChars = "    ";
            // 不生成声明头
            settings.OmitXmlDeclaration = true;
            // 强制指定命名空间，覆盖默认的命名空间。
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj, namespaces);
            }
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
