using System.Collections;
using AMS.Model;

namespace AMS.IBLL
{
    /// <summary>
    /// 首页快捷功能 - 接口
    /// </summary>
    public interface AMS_ShortcutIBLL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Insert(AMS_Shortcut entity);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        bool Delete(string KeyValue, string UserId);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        IList GetList(string UserId);
    }
}