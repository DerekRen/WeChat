using System.Collections.Generic;
using System.Data;
using TencentMsg.DAL.MsgData;
using TencentMsg.IBLL.MsgData;
using TencentMsg.Model.MsgData;

namespace TencentMsg.BLL.MsgData
{
    public class MsgDataBLL : MsgDataIBLL
    {
        /// <summary>
        /// 数据库操作
        /// </summary>
        public MsgDataDAL data = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public MsgDataBLL()
        {
            this.data = new MsgDataDAL();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="show"></param>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string show, string where, int pageIndex, int pageSize, ref int totalCount)
        {
            return data.ExecuteDataTable(show, where, pageIndex, pageSize, ref totalCount);
        }
        /// <summary>
        /// 消息主表查询
        /// </summary>
        /// <param name="show"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string show, string where)
        {
            return data.ExecuteDataTable(show, where);
        }
        /// <summary>
        /// 查询消息扩展表信息
        /// </summary>
        /// <param name="show"></param>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable getMsgExtendDataTable(string show, string where)
        {
            return data.getMsgExtendDataTable(show, where);
        }
        /// <summary>
        /// 获取消息主表数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int getMsgInfoCount(string where)
        {
            return data.getMsgInfoCount(where);
        }
        /// <summary>
        /// 消息主表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMsgInfoBackId(MsgInfoModel model)
        {
            return data.AddMsgInfoBackId(model);
        }

        /// <summary>
        /// 消息主表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMsgInfo(MsgInfoModel model)
        {
            return data.AddMsgInfo(model);
        }
        /// <summary>
        /// 消息扩展表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMsgExtend(MsgExtendModel model)
        {
            return data.AddMsgExtend(model);
        }
        /// <summary>
        /// 消息扩展表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMsgExtend(List<MsgExtendModel> modelList)
        {
            return data.AddMsgExtend(modelList);
        }
    }
}
