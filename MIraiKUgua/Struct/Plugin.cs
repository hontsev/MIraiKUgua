﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using MMDK.Util;
using MMDK.Core;

namespace MMDK.Struct
{
    public abstract class Plugin
    {
        public string PluginName = "";
        public string PluginPath = "";

        protected Config config;
        protected IGlobalFunc BOT;


        public Plugin(string _name)
        {
            PluginName = _name;
            
        }

        public void Init(IGlobalFunc _func, Config _config, string _path)
        {
            BOT = _func;
            config = _config;
            PluginPath = $"{_path}{PluginName}/";
            
            if(!string.IsNullOrWhiteSpace(PluginPath))
            {
                if (!Directory.Exists(PluginPath))
                {
                    Directory.CreateDirectory(PluginPath);
                }
            }

            InitSource();
        }

       

        protected abstract void InitSource();

        public abstract bool HandleMessage(Message msg);

        public abstract void Dispose();


    }
}
