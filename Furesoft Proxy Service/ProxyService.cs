using System.IO;
using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;
using Topshelf;

namespace Furesoft.Proxy
{
    public class ProxyService : ServiceControl
    {
        public static Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.FromResult(0);
        }

        public static Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            //set IsValid to true/false based on Certificate Errors
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                e.IsValid = true;

            return Task.FromResult(0);
        }

        public static async Task OnResponse(object sender, SessionEventArgs e)
        {
            var responseHeaders = e.HttpClient.Response.Headers;

            if (e.HttpClient.Request.Method == "GET" || e.HttpClient.Request.Method == "POST")
            {
                if (e.HttpClient.Response.StatusCode == 200 && e.HttpClient.Response.ContentType != null)
                {
                    if (e.HttpClient.Response.ContentType.Trim().ToLower().Contains("text/html"))
                    {
                        byte[] bodyBytes = await e.GetResponseBody();
                        e.SetResponseBody(bodyBytes);

                        string body = await e.GetResponseBodyAsString();
                        e.SetResponseBodyString(body);
                    }
                }
            }

            if (e.UserData != null)
            {
                var request = (Request)e.UserData;
            }
        }

        public async Task OnRequest(object sender, SessionEventArgs e)
        {
            var requestHeaders = e.HttpClient.Request.Headers;

            var method = e.HttpClient.Request.Method.ToUpper();
            if ((method == "POST" || method == "PUT" || method == "PATCH"))
            {
                byte[] bodyBytes = await e.GetRequestBody();
                e.SetRequestBody(bodyBytes);

                string bodyString = await e.GetRequestBodyAsString();
                e.SetRequestBodyString(bodyString);

                e.UserData = e.HttpClient.Request;
            }

            var uri = e.HttpClient.Request.RequestUri;
            var result = Query.QueryEvaluator.ParseQuery("content on \"furesoft.proxy\" display \"<h1> my own site 2</h1>\"");

            Query.QueryEvaluator.DoBlock(result, e);

            // Test Page
            if (uri.AbsoluteUri.Contains("furesoft.proxy.test"))
            {
                e.Ok("<html><h1>Furesoft Proxy Test Page</h1><p>All works fine!</p></html>");
            }
            if (uri.AbsoluteUri.Contains("test.redirect"))
            {
                HttpWebRequest http = WebRequest.CreateHttp("http://www.google.com" + uri.AbsolutePath);

                var respone = await http.GetResponseAsync();

                e.HttpClient.Response.ContentLength = respone.ContentLength;
                e.HttpClient.Response.ContentType = respone.ContentType;

                var ms = new MemoryStream();
                var strm = respone.GetResponseStream();

                await strm.CopyToAsync(ms);

                e.Ok(ms.ToArray());
            }

            /*if (filterOps.IsMatch(filterOps.GetFilters().ToArray(), uri.AbsoluteUri))
            {
                //ToDo: implement custom Block Template
                e.Ok("<!DOCTYPE html>" +
                      "<html><body><h1>" +
                      "Website Blocked" +
                      "</h1>" +
                      "<p>Blocked by furesoft web proxy.</p>" +
                      "</body>" +
                      "</html>");
            }
            */

            //Redirect example
            if (uri.AbsoluteUri.Contains("wikipedia.org"))
            {
                //ToDo: implement redirection
                e.Redirect("https://www.paypal.com");
            }
        }

        public bool Start(HostControl hostControl)
        {
            proxyServer = new ProxyServer();

            //proxyServer.CertificateManager.TrustRootCertificate(true);

            proxyServer.BeforeRequest += OnRequest;
            proxyServer.BeforeResponse += OnResponse;
            proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback += OnCertificateSelection;

            var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true)
            {
            };

            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();

            var transparentEndPoint = new TransparentProxyEndPoint(IPAddress.Any, 8001, true)
            {
                GenericCertificateName = "google.com"
            };

            proxyServer.AddEndPoint(transparentEndPoint);

            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            proxyServer.BeforeRequest -= OnRequest;
            proxyServer.BeforeResponse -= OnResponse;
            proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;

            proxyServer.Stop();

            return true;
        }

        private ProxyServer proxyServer;

        private static void OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            string hostname = e.HttpClient.Request.RequestUri.Host;

            if (hostname.Contains("dropbox.com"))
            {
                //e.DecryptSsl = false;
            }
        }
    }
}