using System.ComponentModel;
using DotNet.Utilities;
namespace AMS.Entity
{
    /// <summary>
    /// 会议室主表
    /// </summary>
    [Description("会议室管理")]
    [Key("RoomId")]
    public class AMS_MeetingRoom
    {
        private int _roomid;
        private string _roomname;
        private string _roomaddr;
        private int _roomcapacity;
        private string _roomdesc;
        private string _compid;
        private string _phone;
        private string _equipments;
        /// <summary>
        /// 
        /// </summary>
        public int RoomId
        {
            set { _roomid = value; }
            get { return _roomid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoomName
        {
            set { _roomname = value; }
            get { return _roomname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoomAddr
        {
            set { _roomaddr = value; }
            get { return _roomaddr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RoomCapacity
        {
            set { _roomcapacity = value; }
            get { return _roomcapacity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoomDesc
        {
            set { _roomdesc = value; }
            get { return _roomdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompId
        {
            set { _compid = value; }
            get { return _compid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Equipments
        {
            set { _equipments = value; }
            get { return _equipments; }
        }
    }
}
