using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PicoFile_Direct_Link
{
    public class PicoFile
    {
        string url = string.Empty;
        string host = string.Empty;
        string num = string.Empty;
        string address = string.Empty;

        public PicoFile()
        {
        }

        public string URL
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                string[] array = url.Split(new char[] { '/' });
                host = array[2];
                num = array[4];
                address = "http://" + host + "/file/GenerateDownloadLink?fileId=" + num;
            }
        }

        public async Task<string> DirectLink()
        {
            CookieContainer cc = new CookieContainer();
            using (HttpClientHandler hch = new HttpClientHandler())
            {
                hch.AllowAutoRedirect = false;
                hch.CookieContainer = cc;
                hch.UseProxy = false;
                using (HttpClient hc = new HttpClient(hch))
                {
                    hc.DefaultRequestHeaders.Add("Referer", url);
                    hc.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    var res = await hc.PostAsync(address, new StringContent(""));
                    if (res.IsSuccessStatusCode)
                    {
                        return await res.Content.ReadAsStringAsync();
                    }
                    else if (res.StatusCode == HttpStatusCode.Forbidden)
                    {
                        throw new PicoFileException("Invalid password!");
                    }
                    else if (res.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new PicoFileException("Unable to download this file at this time!");
                    }
                    else
                    {
                        throw new PicoFileException("Error performing operation!");
                    }
                }
            }
        }

        public async Task<string> DirectLink(string password)
        {
            CookieContainer cc = new CookieContainer();
            using (HttpClientHandler hch = new HttpClientHandler())
            {
                hch.AllowAutoRedirect = false;
                hch.CookieContainer = cc;
                hch.UseProxy = false;
                using (HttpClient hc = new HttpClient(hch))
                {
                    hc.DefaultRequestHeaders.Add("Referer", url);
                    hc.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    var res = await hc.PostAsync(address, new StringContent("password=" + password, Encoding.UTF8, "application/x-www-form-urlencoded"));
                    if (res.IsSuccessStatusCode)
                    {
                        return await res.Content.ReadAsStringAsync();
                    }
                    else if (res.StatusCode == HttpStatusCode.Forbidden)
                    {
                        throw new PicoFileException("Invalid password!");
                    }
                    else if (res.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new PicoFileException("Unable to download this file at this time!");
                    }
                    else
                    {
                        throw new PicoFileException("Error performing operation!");
                    }
                }
            }
        }
    }
}
