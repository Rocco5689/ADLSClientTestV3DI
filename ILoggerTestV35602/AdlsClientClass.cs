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
        
        public AdlsClient adlsClient = AdlsClient.CreateClient(Environment.GetEnvironmentVariable("adlsAccountName") + ".azuredatalakestore.net", 
            new TokenCredentials(context.AcquireTokenAsync("https://management.azure.com/", 
                new ClientCredential(Environment.GetEnvironmentVariable("ApplicationId"), 
                    Environment.GetEnvironmentVariable("AuthenticationKey"))).Result.ToString()));

        public AdlsClientClass()
        {

        }       
    }
}