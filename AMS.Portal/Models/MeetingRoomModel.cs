using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNet.Portal.Models
{
    public class MeetingRoomModel
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public string RoomAddr { get; set; }

        public int RoomCapacity { get; set; }

        public string RoomDesc { get; set; }

        public string CompId { get; set; }

        public string Phone { get; set; }

        public string Equipments { get; set; }
    }
}