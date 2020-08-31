using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OzNet
{
    public class Destination
    {
        public string Path { get; set; }
        public bool RequiresAuthentication { get; set; }
        static HttpClientHandler handler = new HttpClientHandler();
        static HttpClient client = new HttpClient(handler, false);
        public Destination(string uri, bool requiresAuthentication)
        {
            Path = uri;
            RequiresAuthentication = requiresAuthentication;
        }

        public Destination(string path)
            :this(path, false)
        {
        }

        private Destination()
        {
            Path = "/";
            RequiresAuthentication = false;
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = readStream.ReadToEnd();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request)))
            {
                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                handler.Credentials = CredentialCache.DefaultCredentials;
                client = new HttpClient(handler, false);
                var response = await client.SendAsync(newRequest);
             
                return response;              
            }
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            string requestPath = request.Path.ToString();
            string queryString = request.QueryString.ToString();
            string endpoint = string.Empty;

            if (string.IsNullOrEmpty(requestPath) == false && requestPath.Length > 1)
            {
                if (requestPath.Substring(1).IndexOf('/') > 0)
                {
                    endpoint = requestPath.Substring(requestPath.Substring(1).IndexOf('/') + 2);
                }
            }

            return Path + (string.IsNullOrEmpty(endpoint) == false ? "/" + endpoint : string.Empty) 
                + (string.IsNullOrEmpty(queryString) == false ? "/" + queryString : string.Empty);
        }

    }
}
