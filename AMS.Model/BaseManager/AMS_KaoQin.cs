using System;
using System.ComponentModel;
using DotNet.Utilities;

namespace AMS.Entity
{
    /// <summary>
    /// 考勤信息主表
    /// </summary>
    [Description("考勤管理")]
    [Key("ItemsId")]
    public class AMS_KaoQin
    {
        private string _userid;
        private DateTime _thedate;
        private DateTime? _time1;
        private string _place1;
        private DateTime? _time2;
        private string _place2;
        private int _status;
        private string _photo;
        private string _comment;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime TheDate
        {
            set { _thedate = value; }
            get { return _thedate; }
        }
        /// <summary>
        /// 签到时间1
        /// </summary>
        public DateTime? Time1
        {
            set { _time1 = value; }
            get { return _time1; }
        }
        /// <summary>
        /// 签到地点1
        /// </summary>
        public string Place1
        {
            set { _place1 = value; }
            get { return _place1; }
        }
        /// <summary>
        /// 签到时间2
        /// </summary>
        public DateTime? Time2
        {
            set { _time2 = value; }
            get { return _time2; }
        }
        /// <summary>
        /// 签到地点2
        /// </summary>
        public string Place2
        {
            set { _place2 = value; }
            get { return _place2; }
        }
        /// <summary>
        /// 签到状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 签到照片
        /// </summary>
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
        /// <summary>
        /// 签到说明
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
    }
}
