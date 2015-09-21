using System.Collections;
using System.Text;
using AMS.Model;
using DotNet.Common;

namespace AMS.IBLL
{
    /// <summary>
    /// 组织机构、部门 - 接口
    /// </summary>
    public interface AMS_OrganizationIBLL
    {
        #region Method
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        int GetRecordCount(string KeyValue);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Insert(AMS_Organization entity);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Update(AMS_Organization entity);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        bool Delete(string KeyValue);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        bool BatchDelete(string[] KeyValue);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        AMS_Organization GetEntity(string KeyValue);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        IList GetList();
        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        IList GetListWhere(StringBuilder where, SqlParam[] param);
        #endregion

        /// <summary>
        /// 根据组织机构主键，判断是否有下级节点
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        bool IsBelowMenu(string KeyValue);
    }
}