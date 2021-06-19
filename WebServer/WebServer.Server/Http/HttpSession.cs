﻿using System.Collections.Generic;
using WebServer.Server.Common;

namespace WebServer.Server.Http
{
    public class HttpSession
    {
        public const string SessionCookieName = "WebServerSID";

        private Dictionary<string, string> data;

        public HttpSession(string id)
        {
            Guard.AgainstNull(id, nameof(id));

            this.Id = id;
            this.data = new Dictionary<string, string>();
        }

        public string Id { get; init; }

        public bool IsNew { get; set; }

        public string this[string key]
        {
            get => this.data[key];
            set => this.data[key] = value;
        }

        public int Count() => this.data.Count;

        public bool ContainsKey(string key)
            => this.data.ContainsKey(key);

        public void Remove(string key)
        {
            if (this.data.ContainsKey(key))
            {
                this.data.Remove(key);
            }
        }
    }
}
