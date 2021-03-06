﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMDK.Util
{
    class MessageHistory
    {
        public long userid;
        public string message;
        public DateTime date;

        public MessageHistory()
        {
            // groupid = -1;
            userid = -1;
            message = "";
        }

        public MessageHistory(long _uid, string _message)
        {
            date = DateTime.Now;
            userid = _uid;
            message = _message;
        }

        public override string ToString()
        {
            return $"{date:yyyy-MM-dd_HH:mm:ss}\t{userid}\t{message}";
        }
    }


    class MessageHistoryGroup
    {
        public string filePath = "";
        public long uid;
        public bool isGroup;
        public Queue<MessageHistory> history = new Queue<MessageHistory>();

        public MessageHistoryGroup(string _rootpath, long _gid, bool _isGroup)
        {
            isGroup = _isGroup;
            uid = _gid;
            filePath = $"{_rootpath}/{(isGroup ? "group" : "private")}/{_gid}.txt";
        }

        public void addMessage(long user, string message)
        {
            MessageHistory h = new MessageHistory(user, message);
            history.Enqueue(h);
        }


        static int maxWriteTime = 100;
        public void write()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                int nowtime = 0;
                while (history.Count > 0)
                {
                    sb.AppendLine(history.Dequeue().ToString());
                    if (nowtime++ >= maxWriteTime) break;
                }
                if (sb.Length > 0)
                {
                    File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex);
            }
        }
    }


}
