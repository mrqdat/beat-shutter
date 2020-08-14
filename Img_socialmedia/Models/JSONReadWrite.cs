using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Img_socialmedia.Models
{
    public class JSONReadWrite
    {
        public JSONReadWrite() { }
        public int IsFileExist(string filename,string jsonString)
        {
            int a = 0;
            string root = "wwwroot";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            "json",
            filename);
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                using (var streamWriter = File.CreateText(path))
                {
                    streamWriter.Write(jsonString);
                }
            }
            else
            {
                string jSONString2 = Read(filename,"json");
                string jsonReplaced= "";
                if (jSONString2.Contains(","+jsonString ))
                {
                    jsonReplaced= jSONString2.Replace("," + jsonString , "");
                    a = 1;
                }
                else if (jSONString2.Contains(jsonString))
                {
                    if(jSONString2.Contains(jsonString + ","))
                    {
                        jsonReplaced = jSONString2.Replace(jsonString+",", "");
                        a = 1;
                    }
                    else
                    {
                        jsonReplaced = jSONString2.Replace(jsonString, "");
                        a = 1;
                    }
                }
                else
                {
                    if (jSONString2.Length.Equals(0))
                    {

                        jsonReplaced = jSONString2 + jsonString;
                    }
                    else
                    {
                        jsonReplaced = jSONString2 + "," + jsonString;
                    }
                }
                File.AppendAllText(path, jsonReplaced);
                Write(filename, "json", jsonReplaced);
            }
            return a;
        }
        public string Read(string fileName, string location)
        {
            string root = "wwwroot";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);

            string jsonResult="";
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    jsonResult = streamReader.ReadToEnd();
                }
            }
            return jsonResult;
        }
        public void Write(string fileName, string location, string jSONString)
        {
            string root = "wwwroot";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
                streamWriter.Close();
            }
        }
    }
}
