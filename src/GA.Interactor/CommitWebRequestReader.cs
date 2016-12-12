using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Authentication;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;

namespace GitAnalyser.Interactor
{
    public class CommitWebRequestReader
    {
        public async Task<RepositoryCommit[]> Read()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HandleCert;
            handler.PreAuthenticate = true;
            handler.AllowAutoRedirect = true;
            handler.UseDefaultCredentials = true;

            using (var httpClient = new HttpClient(handler))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Summat");

                    var request = httpClient.DefaultRequestHeaders;

                    var result = await httpClient.GetStringAsync("https://api.github.com/repos/jrhiston/git-analyser/commits");

                    return JsonConvert.DeserializeObject<RepositoryCommit[]>(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool HandleCert(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return true;
        }
    }
}
