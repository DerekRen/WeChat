using System.Collections.Generic;
using System.Data;
using System.Text;
using TencentMsg.Common.CommService;
using TencentMsg.Model.MsgData;

namespace TencentMsg.DAL.MsgData
{
    public class MsgDataDAL
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
        public DataTable ExecuteDataTable(string show, string where, int pageIndex, int pageSize, ref int totalCount)
        {
            //string tables = "TMsgInfo mi left join MsgExtend me on mi.TMIId = me.TMETIId ";
            string tables = @"TMsgInfo mi ";
            StringBuilder sql = new StringBuilder();
            if (pageIndex == 1)//第一页
            {
                sql.AppendFormat(@"select top {2} {0} from {3} where 1=1 {1} order by mi.TMIId desc;"
                   , show, where, pageSize, tables);
            }
            else
            {
                int start = (pageIndex - 1) * pageSize;
                sql.AppendFormat(@"select top {3} {0} from {4}
                            where 1=1 {1} and mi.TMIId<
                            (select min(mi.TMIId) from {4} where mi.TMIId in 
                            ( select top {2} mi.TMIId from {4} order by mi.TMIId desc));"
                    , show, where, start, pageSize, tables);
            }
            StringBuilder sqlNum = new StringBuilder();
            sqlNum.AppendFormat(@" select mi.TMIId from {1} where 1=1 {0}", where, tables);
            DataSet TempSet = DBHelper.ExecuteDataSet(sql.ToString());//无法同时执行两个sql(坑爹啊！)
            DataSet NumSet = DBHelper.ExecuteDataSet(sqlNum.ToString());
            totalCount = ConvertHelper.ToInt(NumSet.Tables[0].Rows.Count);
            return TempSet.Tables[0];
        }
        /// <summary>
        /// 获取消息主表数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int getMsgInfoCount(string where)
        {
            string tables = @"TMsgInfo mi ";
            StringBuilder sqlNum = new StringBuilder();
            sqlNum.AppendFormat(@" select mi.TMIId from {1} where 1=1 {0}", where, tables);
            return DBHelper.ExecuteDataSet(sqlNum.ToString()).Tables[0].Rows.Count;
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
        public DataTable ExecuteDataTable(string show, string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select {0}  from TMsgInfo where 1=1 {1} ", show, where);
            return DBHelper.ExecuteDataTable(sql.ToString()); ;
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
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select {0}  from MsgExtend where 1=1 {1} ", show, where);
            return DBHelper.ExecuteDataTable(sql.ToString()); ;
        }
        /// <summary>
        /// 消息主表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMsgInfoBackId(MsgInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            int backId = 0;
            sql.AppendFormat(@"INSERT INTO TMsgInfo (TMIOpenId,TMIMsgType,TMIMsgRelative,TMISource,TMIContent,TMIAddTime)
                            values ('{0}','{1}','{2}','{3}','{4}','{5}')"
                            , model.TMIOpenId, model.TMIMsgType, model.TMIMsgRelative, model.TMISource, model.TMIContent, model.TMIAddTime);
            if (DBHelper.ExecuteSql(sql.ToString()) > 0)
            {
                DataTable dt = DBHelper.ExecuteDataTable("SElECT top 1 TMIId from TMsgInfo order by TMIId desc");
                if (ConvertHelper.checkData(dt))
                {
                    backId = ConvertHelper.ToInt(dt.Rows[0]["TMIId"]);
                }
            }
            return backId;
        }

        /// <summary>
        /// 消息主表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMsgInfo(MsgInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"INSERT INTO TMsgInfo (TMIOpenId,TMIMsgType,TMIMsgRelative,TMISource,TMIContent,TMIAddTime)
                            values ('{0}','{1}','{2}','{3}','{4}','{5}')"
                , model.TMIOpenId, model.TMIMsgType, model.TMIMsgRelative, model.TMISource, model.TMIContent, model.TMIAddTime);
            return DBHelper.ExecuteSql(sql.ToString()) > 0;
        }

        /// <summary>
        /// 消息扩展表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMsgExtend(MsgExtendModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"INSERT INTO MsgExtend (TMETIId,TMETitle,TMEDes,TMEMainId,TMESubId,TMEmusicid)
                            values ({0},'{1}','{2}','{3}','{4}','{5}')"
                , model.TMETIId, model.TMETitle, model.TMEDes, model.TMEMainId, model.TMESubId, model.TMEmusicid);
            return DBHelper.ExecuteSql(sql.ToString()) > 0;
        }

        /// <summary>
        /// 消息扩展表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMsgExtend(List<MsgExtendModel> modelList)
        {
            foreach (MsgExtendModel model in modelList)
            {
                if (!AddMsgExtend(model))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
