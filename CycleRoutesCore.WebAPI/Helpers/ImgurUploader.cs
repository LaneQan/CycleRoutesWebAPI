using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CycleRoutesCore.WebAPI.Helpers
{
    public class ImgurUploader
    {
        public string UploadImage(string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.imgur.com/3/image");
            request.Headers.Add("Authorization", "Client-ID bb862a5e6837fbc");
            request.Method = "POST";

            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bytes = enc.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            Stream writer = request.GetRequestStream();
            writer.Write(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            WebHeaderCollection header = response.Headers;

            var encoding = ASCIIEncoding.ASCII;

            string json;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                json = reader.ReadToEnd();
            }
            ImgurResponse tmp = JsonConvert.DeserializeObject<ImgurResponse>(json);
            return tmp.Data.Link;
    }
}
}
