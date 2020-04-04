using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class handleWrongCitynames
    {
        /// <summary>
        /// Funkcja sprawdzajaca czy nie wystapil blad 404 i inne 
        /// </summary>
        public bool checkUrl(string url)
        {
            bool tmp;

            try
            {
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        tmp = false;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError &&
                    ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        tmp = true;
                    }
                    else
                    {
                        tmp = true;
                    }
                }
                else
                {
                    tmp = true;
                }
            }

            return tmp;
        }

    }
}
