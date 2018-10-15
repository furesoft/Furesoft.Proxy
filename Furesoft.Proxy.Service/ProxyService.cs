using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;

namespace Furesoft.Proxy.Service
{
    public partial class ProxyService
    {
        private ProxyServer proxyServer;

        public ProxyService()
        {
            
        }

        public void Start()
        {
            proxyServer = new ProxyServer();

            proxyServer.CertificateManager.TrustRootCertificate(true);

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
        }

        public void Stop()
        {
            proxyServer.BeforeRequest -= OnRequest;
            proxyServer.BeforeResponse -= OnResponse;
            proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;

            proxyServer.Stop();
        }

        #region "EventHandler"

        private static void OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            string hostname = e.WebSession.Request.RequestUri.Host;

            if (hostname.Contains("dropbox.com"))
            {
                //e.DecryptSsl = false;
            }
        }

        public static async Task OnRequest(object sender, SessionEventArgs e)
        {
            var requestHeaders = e.WebSession.Request.Headers;

            var method = e.WebSession.Request.Method.ToUpper();
            if ((method == "POST" || method == "PUT" || method == "PATCH"))
            {
                byte[] bodyBytes = await e.GetRequestBody();
                e.SetRequestBody(bodyBytes);

                string bodyString = await e.GetRequestBodyAsString();
                e.SetRequestBodyString(bodyString);

                e.UserData = e.WebSession.Request;
            }

            // ToDo: implement custom filter via regex
            if (e.WebSession.Request.RequestUri.AbsoluteUri.Contains("tor"))
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
            //Redirect example
            if (e.WebSession.Request.RequestUri.AbsoluteUri.Contains("wikipedia.org"))
            {
                //ToDo: implement redirection
                e.Redirect("https://www.paypal.com");
            }
        }

        public static async Task OnResponse(object sender, SessionEventArgs e)
        {
            var responseHeaders = e.WebSession.Response.Headers;

            if (e.WebSession.Request.Method == "GET" || e.WebSession.Request.Method == "POST")
            {
                if (e.WebSession.Response.StatusCode == 200)
                {
                    if (e.WebSession.Response.ContentType != null && e.WebSession.Response.ContentType.Trim().ToLower().Contains("text/html"))
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

        public static Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            //set IsValid to true/false based on Certificate Errors
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                e.IsValid = true;

            return Task.FromResult(0);
        }

        public static Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.FromResult(0);
        }

        #endregion
    }
}