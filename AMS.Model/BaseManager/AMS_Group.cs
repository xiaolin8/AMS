using System.ComponentModel;
using DotNet.Utilities;

namespace AMS.Entity
{
    /// <summary>
    /// 群组主表
    /// </summary>
    [Description("群组管理")]
    [Key("RoomId")]
    public class AMS_Group
    {
        #region Model
        private string _groupid;
        private string _groupname;
        private string _groupdesc;
        private string _groupowner;
        private int? _groupicon;
        private string _members;
        private int? _allowinvite;
        private int? _memberssize;
        private int? _maxusers;
        private int? _ispublic;
        /// <summary>
        /// 
        /// </summary>
        public string groupId
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string groupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string groupDesc
        {
            set { _groupdesc = value; }
            get { return _groupdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string groupOwner
        {
            set { _groupowner = value; }
            get { return _groupowner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? groupIcon
        {
            set { _groupicon = value; }
            get { return _groupicon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string members
        {
            set { _members = value; }
            get { return _members; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? allowInvite
        {
            set { _allowinvite = value; }
            get { return _allowinvite; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? membersSize
        {
            set { _memberssize = value; }
            get { return _memberssize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? maxUsers
        {
            set { _maxusers = value; }
            get { return _maxusers; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isPublic
        {
            set { _ispublic = value; }
            get { return _ispublic; }
        }
        #endregion Model
    }
}