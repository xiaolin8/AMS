﻿using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace AMS.BLL
{
    public class HXService
    {
        string reqUrlFormat = "https://a1.easemob.com/{0}/{1}/";
        public string clientID { get; set; }
        public string clientSecret { get; set; }
        public string appName { get; set; }
        public string orgName { get; set; }
        public string token { get; set; }
        public string easeMobUrl { get { return string.Format(reqUrlFormat, orgName, appName); } }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HXService()
        {
            this.clientID = ConfigurationManager.AppSettings["client_id"];
            this.clientSecret = ConfigurationManager.AppSettings["client_secret"];
            this.appName = ConfigurationManager.AppSettings["appName"];
            this.orgName = ConfigurationManager.AppSettings["orgName"];
            this.token = QueryToken();
        }

        /// <summary>
        /// 使用app的client_id 和 client_secret登陆并获取授权token
        /// </summary>
        /// <returns></returns>
        string QueryToken()
        {
            if (string.IsNullOrEmpty(clientID) || string.IsNullOrEmpty(clientSecret)) { return string.Empty; }
            string cacheKey = clientID + clientSecret;
            if (System.Web.HttpRuntime.Cache.Get(cacheKey) != null &&
                System.Web.HttpRuntime.Cache.Get(cacheKey).ToString().Length > 0)
            {
                return System.Web.HttpRuntime.Cache.Get(cacheKey).ToString();
            }

            string postUrl = easeMobUrl + "token";
            StringBuilder _build = new StringBuilder();
            _build.Append("{");
            _build.AppendFormat("\"grant_type\": \"client_credentials\",\"client_id\": \"{0}\",\"client_secret\": \"{1}\"", clientID, clientSecret);
            _build.Append("}");

            string postResultStr = ReqUrl(postUrl, "POST", _build.ToString(), string.Empty);
            string token = string.Empty;
            int expireSeconds = 0;
            try
            {
                JObject jo = JObject.Parse(postResultStr);
                token = jo.GetValue("access_token").ToString();
                int.TryParse(jo.GetValue("expires_in").ToString(), out expireSeconds);
                //设置缓存
                if (!string.IsNullOrEmpty(token) && token.Length > 0 && expireSeconds > 0)
                {
                    System.Web.HttpRuntime.Cache.Insert(cacheKey, token, null, DateTime.Now.AddSeconds(expireSeconds), System.TimeSpan.Zero);
                }
            }
            catch { return postResultStr; }
            return token;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <returns>创建成功的用户JSON</returns>
        public string AccountCreate(string userName, string password)
        {
            StringBuilder _build = new StringBuilder();
            _build.Append("{");
            _build.AppendFormat("\"username\": \"{0}\",\"password\": \"{1}\"", userName, password);
            _build.Append("}");

            return AccountCreate(_build.ToString());
        }

        /// <summary>
        /// 创建用户(可以批量创建)
        /// </summary>
        /// <param name="postData">创建账号JSON数组--可以一个，也可以多个</param>
        /// <returns>创建成功的用户JSON</returns>
        public string AccountCreate(string postData) { return ReqUrl(easeMobUrl + "users", "POST", postData, token); }

        /// <summary>
        /// 获取指定用户详情
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>会员JSON</returns>
        public string AccountGet(string userName) { return ReqUrl(easeMobUrl + "users/" + userName, "GET", string.Empty, token); }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>重置结果JSON(如：{ "action" : "set user password",  "timestamp" : 1404802674401,  "duration" : 90})</returns>
        public string AccountResetPwd(string userName, string newPassword) { return ReqUrl(easeMobUrl + "users/" + userName + "/password", "PUT", "{\"newpassword\" : \"" + newPassword + "\"}", token); }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>成功返回会员JSON详细信息，失败直接返回：系统错误信息</returns>
        public string AccountDel(string userName) { return ReqUrl(easeMobUrl + "users/" + userName, "DELETE", string.Empty, token); }

        public string ReqUrl(string reqUrl, string method, string paramData, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;
                request.Method = method.ToUpperInvariant();

                if (!string.IsNullOrEmpty(token) && token.Length > 1)
                {
                    request.Headers.Add("Authorization", "Bearer " + token);
                }
                if (request.Method.ToString() != "GET" && !string.IsNullOrEmpty(paramData) && paramData.Length > 0)
                {
                    request.ContentType = "application/json";
                    byte[] buffer = Encoding.UTF8.GetBytes(paramData);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader stream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string result = stream.ReadToEnd();
                        return result;
                    }
                }
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        /// <summary>
        /// 获取消息记录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string getChatMessages(string userName)
        {
            return ReqUrl(easeMobUrl + "chatmessages", "GET", string.Empty, token);
        }

        /// <summary>
        /// 获取所有群组的简介
        /// </summary>
        /// <returns></returns>
        public string GetAllGroups() { return ReqUrl(easeMobUrl + "chatgroups", "GET", string.Empty, token); }

        /// <summary>
        /// 获取某个群组的详情
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetGroupDetailById(string groupId) { return ReqUrl(easeMobUrl + "chatgroups/" + groupId, "GET", string.Empty, token); }

        /// <summary>
        /// 修改群组信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupname"></param>
        /// <param name="description"></param>
        /// <param name="maxusers"></param>
        /// <returns></returns>
        public string EditGroupById(string groupId, string groupname, string description, int maxusers)
        {
            StringBuilder _build = new StringBuilder();
            _build.Append("{");
            _build.AppendFormat("\"groupname\": \"{0}\",\"description\": \"{1}\",\"maxusers\": \"{2}\"", groupname, description, maxusers);
            _build.Append("}");
            return ReqUrl(easeMobUrl + "chatgroups/" + groupId, "PUT", _build.ToString(), token);
        }

        public string NewGroup(string groupname, string description, int maxusers, string owner, string[] members)
        {
            StringBuilder _build = new StringBuilder();
            _build.Append("{");
            _build.AppendFormat("\"groupname\": \"{0}\",\"desc\": \"{1}\",\"maxusers\": {2},\"owner\": \"{3}\",\"public\": true", groupname, description, maxusers, owner);
            if (members != null && members.Length > 0)
            {
                _build.Append(",\"members\":[");
                foreach (string s in members)
                {
                    _build.AppendFormat("\"{0}\",", s);
                }
                _build.Remove(_build.Length - 2, 1);//移除最后的逗号
                _build.Append("]");
            }
            _build.Append("}");
            return ReqUrl(easeMobUrl + "chatgroups/", "POST", _build.ToString(), token);

        }

        public string deleteGroup(string groupId)
        {
            return ReqUrl(easeMobUrl + "chatgroups/" + groupId, "DELETE", string.Empty, token);
        }


        public string addGroupMembers(string groupId, string[] members)
        {
            StringBuilder _build = new StringBuilder();
            _build.Append("{\"usernames\":[");
            foreach (string s in members)
            {
                _build.AppendFormat("\"{0}\",", s);
            }
            _build.Remove(_build.Length - 1, 1);//移除最后的逗号
            _build.Append("]}");
            return ReqUrl(easeMobUrl + "chatgroups/" + groupId + "/users", "POST", _build.ToString(), token);
        }

        public string deleteGroupMember(string groupId, string member)
        {
            return ReqUrl(easeMobUrl + "chatgroups/" + groupId + "/users/" + member, "DELETE", string.Empty, token);
        }
    }
}