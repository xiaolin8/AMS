using System.Collections;
using AMS.Model;

namespace AMS.IBLL
{
    /// <summary>
    /// 数据权限存储表 - 接口
    /// </summary>
    public interface AMS_DataPermissionIBLL
    {
        #region Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Insert(AMS_DataPermission entity);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Update(AMS_DataPermission entity);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        AMS_DataPermission GetEntity(string KeyValue);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <param name="ResourceId">授权项目</param>
        /// <returns></returns>
        AMS_DataPermission GetEntity(string RoleId, string ResourceId);
        #endregion

        #region 授权项目
        /// <summary>
        /// 组织机构列表
        /// </summary>
        /// <returns></returns>
        IList GetOrganizationList();
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        IList GetUserList();
        /// <summary>
        /// 员工列表
        /// </summary>
        /// <returns></returns>
        IList GetEmployeeList();
        #endregion
    }
}