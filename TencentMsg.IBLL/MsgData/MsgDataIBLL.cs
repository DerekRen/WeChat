using System.Collections.Generic;
using System.Data;
using TencentMsg.Model.MsgData;

namespace TencentMsg.IBLL.MsgData
{
    public interface MsgDataIBLL
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="show"></param>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string show, string where, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 消息主表查询
        /// </summary>
        /// <param name="show"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string show, string where);
        /// <summary>
        /// 查询消息扩展表信息
        /// </summary>
        /// <param name="show"></param>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable getMsgExtendDataTable(string show, string where);
        /// <summary>
        /// 获取消息主表数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int getMsgInfoCount(string where);
        /// <summary>
        /// 消息主表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddMsgInfoBackId(MsgInfoModel model);

        /// <summary>
        /// 消息主表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddMsgInfo(MsgInfoModel model);

        /// <summary>
        /// 消息扩展表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddMsgExtend(MsgExtendModel model);

        /// <summary>
        /// 消息扩展表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddMsgExtend(List<MsgExtendModel> modelList);
    }
}
