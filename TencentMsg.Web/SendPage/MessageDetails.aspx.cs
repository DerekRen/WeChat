using System;
using System.Data;
using System.Text;
using TencentMsg.BLL.MsgData;
using TencentMsg.Common.CommonEnum;
using TencentMsg.Common.CommService;

public partial class MessageDetails : BasePage
{
    int id = 0;
    MsgDataBLL miBLL = new MsgDataBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = ConvertHelper.ToInt(Request["id"]);
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    #region 加载信息
    /// <summary>
    /// 记载详细信息
    /// </summary>
    public void InitPage()
    {
        if (id > 0)
        {
            string where = " AND TMIId =" + id;
            DataTable dt = miBLL.ExecuteDataTable("*", where);//查询消息主表内容
            if (ConvertHelper.checkData(dt))
            {
                DataRow dr = dt.Rows[0];
                string msgType = ConvertHelper.ToString(dr["TMIMsgType"]);
                msgTypeTxt.Text = msgType;
                mediaId.Text = ConvertHelper.ToString(dr["TMIContent"]);
                StringBuilder strBr = new StringBuilder();
                #region 图片消息
                if (msgType == CommonEnum.enumMsgType.image.ToString())//图片消息
                {
                    string imgPath = CommMsgParam.GetMultimedia(ConvertHelper.ToString(dr["TMIContent"]), true);
                    strBr.AppendFormat("<img src=\"{0}\" >", CommonFun.ApplicationFilePath + imgPath);
                    textContent.InnerHtml = strBr.ToString();
                }
                #endregion

                #region 其他信息
                else
                {
                    DataTable extendMsg = GetExtendMsg(id);//查询扩展表内容
                    if (ConvertHelper.checkData(extendMsg))
                    {
                        foreach (DataRow de in extendMsg.Rows)//处理图文下的一对多关系
                        {
                            title.Text = ConvertHelper.ToString(de["TMETitle"]);
                            MainUrl.Text = ConvertHelper.ToString(de["TMEMainId"]);
                            SubUrl.Text = ConvertHelper.ToString(de["TMESubId"]);
                            description.Text = ConvertHelper.ToString(de["TMEDes"]);
                        }
                    }
                }
                #endregion

            }
        }
    }
    #endregion

    #region 根据主消息ID获取扩展消息
    /// <summary>
    /// 根据主消息ID获取扩展消息
    /// </summary>
    /// <param name="msgId"></param>
    /// <returns></returns>
    public DataTable GetExtendMsg(int msgId)
    {
        string where = " AND TMETIId=" + msgId;
        return miBLL.getMsgExtendDataTable(" * ", where);
    }
    #endregion
}