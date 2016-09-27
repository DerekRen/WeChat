using System;
using System.ComponentModel;
using System.Reflection;

namespace TencentMsg.Common.CommonEnum
{
    /// <summary>
    /// 公共枚举
    /// </summary>
    public class CommonEnum
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        public enum enumMsgSource
        {
            /// <summary>
            /// 发送
            /// </summary>
            [Description("发送")]
            send = 1,
            /// <summary>
            /// 接收
            /// </summary>
            [Description("接收")]
            Receive = 0
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum enumMsgType
        {
            /// <summary>
            /// 文档消息类型
            /// </summary>
            text,

            /// <summary>
            /// 图片消息类型
            /// </summary>
            image,

            /// <summary>
            /// 语音消息类型
            /// </summary>
            voice,

            /// <summary>
            /// 视频消息类型
            /// </summary>
            video,

            /// <summary>
            /// 音乐消息类型
            /// </summary>
            music,

            /// <summary>
            /// 图文消息类型
            /// </summary>
            news,

            /// <summary>
            /// 缩略图类型
            /// </summary>
            thumb

        }

        #region 根据枚举值获得描述
        /// <summary>
        /// 根据枚举值获得描述
        /// </summary>
        /// <param name="enumtype">类型</param>
        /// <param name="value">枚举项的值</param>
        /// <returns></returns>
        public static string GetValueByEnumName(Type enumtype, int value)
        {
            string enumname = string.Empty;
            System.Reflection.FieldInfo[] fields = enumtype.GetFields();
            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    //枚举英文
                    string name = field.Name;
                    if ((int)System.Enum.Parse(enumtype, name, true) == value)
                    {
                        DescriptionAttribute da = null;
                        object[] arrobj = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (arrobj.Length > 0)
                        {
                            da = arrobj[0] as DescriptionAttribute;
                        }
                        if (da != null)
                        {
                            //枚举中文描述
                            enumname = da.Description;
                        }
                    }
                }
            }
            return enumname;
        }

        /// <summary>
        /// 根据枚举值获得描述
        /// </summary>
        /// <param name="enumtype">类型</param>
        /// <param name="value">枚举项的值</param>
        /// <returns></returns>
        public static string GetValueByEnumName(Type enumtype, string value)
        {
            string enumname = string.Empty;
            System.Reflection.FieldInfo[] fields = enumtype.GetFields();
            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    //枚举英文
                    string name = field.Name;
                    if (Convert.ToString(System.Enum.Parse(enumtype, name, true)).Equals(value))
                    {
                        DescriptionAttribute da = null;
                        object[] arrobj = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (arrobj.Length > 0)
                        {
                            da = arrobj[0] as DescriptionAttribute;
                        }
                        if (da != null)
                        {
                            //枚举中文描述
                            enumname = da.Description;
                        }
                    }
                }
            }
            return enumname;
        }
        #endregion
    }
}
