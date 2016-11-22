using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cyril_and_Methodius.Model
{
    class TextHandler
    {
        public string ReadFromFile(string fileLocation)
        {
            try
            {
                return System.IO.File.ReadAllText(fileLocation);
            }
            catch (Exception)
            {
                return "";
            }

        }
        public string ReadFromWeb(string fileWebLocation)
        {
            bool supportedMIME = false;
            try
            {
                WebClient client = new WebClient();
                byte[] myDataBuffer = client.DownloadData(fileWebLocation);
                string cType = client.ResponseHeaders["Content-Type"];
                supportedMIME = Helper.GetMIMEType(cType);
            }
            catch (Exception e)
            {
                throw new WebReadException("There was problem reading the file type.", e);
            }
            try
            {
                if (supportedMIME)
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(fileWebLocation);
                    StreamReader reader = new StreamReader(stream);
                    String content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (Exception e)
            {
                throw new WebReadException("There was problem reading from the file.", e);
            }
            return "";
        }
    }
}
