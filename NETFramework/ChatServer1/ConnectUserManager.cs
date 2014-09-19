﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer1
{
    class ConnectUserManager
    {
        int MaxUserCount = 0;

        Dictionary<int, ConnectUser> UserSessionMap = new Dictionary<int, ConnectUser>();
        Dictionary<string, ConnectUser> UserIDMap = new Dictionary<string, ConnectUser>();
        

        public void Init(int maxUserCount)
        {
            MaxUserCount = maxUserCount;
        }

        public ERROR_CODE AddUser(int sessionID, string userID)
        {
            if (UserSessionMap.ContainsKey(sessionID))
            {
                return ERROR_CODE.ADD_USER_DUPLICATION_SESSION;
            }

            if (UserIDMap.ContainsKey(userID))
            {
                return ERROR_CODE.ADD_USER_DUPLICATION_ID;
            }

            var user = new ConnectUser();
            user.SetInfo(sessionID, userID);

            UserSessionMap.Add(sessionID, user);
            UserIDMap.Add(userID, user);

            return ERROR_CODE.NONE;
        }

        public void RemoveUser(int serial)
        {
            var user = GetUser(serial);

            if (user != null)
            {
                UserSessionMap.Remove(serial);
                UserIDMap.Remove(user.ID);
            }
        }

        public ConnectUser GetUser(int serial)
        {
            ConnectUser user = null;
            UserSessionMap.TryGetValue(serial, out user);
            return user;
        }
    }

    class ConnectUser
    {
        public int Serial { get; private set; }
        public string ID { get; private set; }

        public void SetInfo(int serial, string id)
        {
            Serial = serial;
            ID = id;
        }
    }
}