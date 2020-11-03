using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Azure.DataLake.Store;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;

namespace AdlsClientTestV3DI
{
    public class AdlsClientClass : IAdlsClientClass
    {
        static AuthenticationContext context = new AuthenticationContext("https://login.windows.net/" + Environment.GetEnvironmentVariable("TenantID"));
        static ClientCredential cc = new ClientCredential(Environment.GetEnvironmentVariable("ApplicationId"), Environment.GetEnvironmentVariable("AuthenticationKey"));
        static AuthenticationResult result = context.AcquireTokenAsync("https://management.azure.com/", cc).Result;
        static ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
        public AdlsClient adlsClient = AdlsClient.CreateClient(Environment.GetEnvironmentVariable("adlsAccountName") + ".azuredatalakestore.net", cred);

        public AdlsClientClass()
        {

        }       
    }
}