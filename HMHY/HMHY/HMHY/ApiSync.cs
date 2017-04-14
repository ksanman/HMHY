﻿using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HMHY
{
    /// <summary>
    /// This class handles syncing the local phone db to the server db. 
    /// </summary>
    class ApiSync
    {
        public Core core;
        public ApiSync() { core = new Core(); }

        public T GetTable<T>()
        {
            try
            {
                var con = new HttpRequestService();
                var request = con.MakeConnection(core.MainUserApiGetRequest, Method.GET);
                var response = con.Client.Execute<T>(request);
                T list = response.Result.Data;
                return list;
            }
            catch(Exception e) { throw; }
        }

        public T GetRow<T>()
        {
            try
            {
                var con = new HttpRequestService();
                var request = con.MakeConnection(core.MainUserApiGetRequest, Method.GET);
                var response = con.Client.Execute<T>(request);
                T row = response.Result.Data;
                return row;
            }
            catch(Exception e) { throw; }
        }

        public bool UpdateRow<T>(T row)
        {
            try
            {
                var con = new HttpRequestService();
                var request = con.MakeConnection(core.MainUserApiGetRequest, Method.PUT);
                var ser = new XmlSerializer(typeof(T));
                var strWtr = new StringWriter();
                Task<IRestResponse> response;
                using (XmlWriter writer = XmlWriter.Create(strWtr))
                {
                    ser.Serialize(writer, row);
                    var xml = strWtr.ToString();
                    request.AddXmlBody(xml);
                    response = con.Client.Execute(request);
                }

                if (!response.IsCompleted || response.IsCanceled || response.IsFaulted)
                {
                    return false;
                }

                return true;
            }
            catch(Exception e) { return false; }
        }

        public bool AddRow<T>(T row)
        {
            try
            {
                var con = new HttpRequestService();
                var request = con.MakeConnection(core.MainUserApiGetRequest, Method.POST);
                var ser = new XmlSerializer(typeof(T));
                var strWtr = new StringWriter();
                Task<IRestResponse> response;
                using (XmlWriter writer = XmlWriter.Create(strWtr))
                {
                    ser.Serialize(writer, row);
                    var xml = strWtr.ToString();
                    request.AddXmlBody(xml);
                    response = con.Client.Execute(request);
                }

                if (!response.IsCompleted || response.IsCanceled || response.IsFaulted)
                {
                    return false;
                }

                return true;
            }
            catch(Exception e) { return false; }
        }
    }
}
