using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace TencentMsg.DAL
{
    /// <summary> 
    /// OleDbServer数据访问帮助类 
    /// </summary> 
    public sealed class DBHelper
    {

        #region 数据库连接
        /// <summary> 
        /// 一个有效的数据库连接字符串 
        /// </summary> 
        /// <returns></returns> 
        public static string GetConnSting()
        {
            return ConfigurationManager.AppSettings["TencentMsgData"];
        }
        private static OleDbConnection connection;
        /// <summary>
        /// 获得一个唯一的CONNECTION 实例
        /// </summary>
        public static OleDbConnection Connection
        {
            get
            {
                string connectionstring = GetConnSting();

                if (connection == null)
                {
                    connection = new OleDbConnection(connectionstring);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();

                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        #endregion
        /// <summary>
        /// 返回DateSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            DataSet myds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, Connection);
            oda.Fill(myds);
            connection.Close();
            return myds;
        }
        /// <summary>
        /// 返回DateTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql)
        {
            DataTable mydt = new DataTable();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, Connection);
            oda.Fill(mydt);
            connection.Close();
            return mydt;
        }
        /// <summary>
        /// 返回受影响条数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteSql(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            int num = cmd.ExecuteNonQuery();
            connection.Close();
            return num;
        }

    }
}
