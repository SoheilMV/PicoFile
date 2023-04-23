using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PicoFileSharp
{
    public class PicoFile
    {
        private string _url = string.Empty;
        private string _host = string.Empty;
        private string _num = string.Empty;

        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                string[] array = _url.Split(new char[] { '/' });
                _host = array[2];
                _num = array[4];
            }
        }

        public PicoFile(string url)
        {
            URL = url;
        }

        public async Task<string> DirectLink()
        {
            using (HttpClientHandler hch = new HttpClientHandler())
            {
                hch.AllowAutoRedirect = false;
                using (HttpClient hc = new HttpClient(hch, true))
                {
                    using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"https://{_host}/file/GenerateDownloadLink?fileId={_num}"))
                    {
                        req.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
                        req.Headers.Add("Accept", "*/*");
                        req.Headers.Add("Origin", "https://s8.picofile.com");
                        req.Headers.Add("Referer", _url);
                        req.Headers.Add("X-Requested-With", "XMLHttpRequest");
                        req.Headers.ExpectContinue = false;
                        using (HttpResponseMessage res = await hc.SendAsync(req))
                        {
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

        public async Task<string> DirectLink(string password)
        {
            using (HttpClientHandler hch = new HttpClientHandler())
            {
                hch.AllowAutoRedirect = false;
                using (HttpClient hc = new HttpClient(hch, true))
                {
                    using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"https://{_host}/file/generateDownloadLink?fileId={_num}"))
                    {
                        req.Content = new StringContent("password=" + password, Encoding.UTF8, "application/x-www-form-urlencoded");
                        req.Headers.Add("Accept", "*/*");
                        req.Headers.Add("Origin", "https://s8.picofile.com");
                        req.Headers.Add("Referer", _url);
                        req.Headers.Add("X-Requested-With", "XMLHttpRequest");
                        req.Headers.ExpectContinue = false;
                        using (HttpResponseMessage res = await hc.SendAsync(req))
                        {
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
    }
}
