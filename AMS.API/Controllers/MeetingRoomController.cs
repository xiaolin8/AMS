using System.Collections;
using System.Web.Http;
using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.API.Controllers
{
    public class MeetingRoomController : ApiController
    {
        string state = "", message = "";
        int count = 0;
        AMS_MeetingRoomBLL bll = new AMS_MeetingRoomBLL();
        //
        // GET: /MeetingRoom/

        public string GetMeetingRooms()
        {
            IList list = bll.GetList();
            if (list != null && list.Count > 0)
            {
                state = "100"; message = "获取成功";
                return JsonHelper.ListToJson<AMS_MeetingRoom>(list, "AMS_MeetingRoom");
            }
            else
            {
                state = "104";
                message = "暂无任何用户信息";
                return null;
            }
        }

    }
}
