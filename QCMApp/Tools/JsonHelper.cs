using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace QCMApp.Tools
{
    /// <summary>
    /// Assistant de traitement Json
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Sérialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string Serialize(List<JSonSimpleClass> o)
        {
            if (o != null)
            {
                MemoryStream str = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSonSimpleClass[]));
                ser.WriteObject(str, o.ToArray());
                byte[] result = str.ToArray();
                str.Close();
                return Encoding.UTF8.GetString(result, 0, result.Length);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Sérialize
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Serialize(object o, Type type)
        {
            if (o != null)
            {
                try
                {
                    MemoryStream str = new MemoryStream();
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(type, null, int.MaxValue, false, new DateTimeSurrogate(), false);
                    ser.WriteObject(str, o);
                    byte[] result = str.ToArray();
                    str.Close();
                    return Encoding.UTF8.GetString(result, 0, result.Length);
                }
                catch (Exception ex)
                {
                    Logger.Ecrire(Logger.Niveau.Warning, ex.Message);
                    return string.Empty;
                }

            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Désérialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<JSonSimpleClass> Deserialize(string s)
        {
            if (s != null)
            {
                List<JSonSimpleClass> result = new List<JSonSimpleClass>();
                MemoryStream str = new MemoryStream(Encoding.UTF8.GetBytes(s));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSonSimpleClass[]));
                result.AddRange(ser.ReadObject(str) as JSonSimpleClass[]);
                str.Close();
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Désérialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static object Deserialize<T>(string s)
        {
            if (s != null)
            {
                MemoryStream str = new MemoryStream(Encoding.UTF8.GetBytes(s));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                object result = ser.ReadObject(str);
                str.Close();
                return result;
            }
            else
                return null;
        }
    }
}