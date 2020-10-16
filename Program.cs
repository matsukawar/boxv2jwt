using Box.V2;
using Box.V2.Config;
using Box.V2.JWTAuth;
using System;
using System.IO;

namespace BoxApiSample
{
    static class Program
    {
        /// <summary>
        /// Main Program
        ///
        /// TODO
        /// * Install "Box.V2 Version 3.18.0" from nuget.org
        /// * Access Box Developers page, download private key and passphase.
        /// * Download json config from Box Developers page and set private key and passphase, add it to the project file.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists("./your_config.json") == false)
                return;

            // Create Box Config from Json (JWTAuth)
            IBoxConfig oBoxConfig = null;
            using (var oFileStream = new FileStream("./your_config.json", FileMode.Open))
                oBoxConfig = BoxConfig.CreateFromJsonFile(oFileStream);

            var tUseProxy = false; // If you use the proxy, please change it "true".
            if (tUseProxy)
                oBoxConfig.WebProxy = new System.Net.WebProxy("<your proxy server>", <your proxy port no>);

            // Create JWT Auth Object
            var oBoxJWTAuth = new BoxJWTAuth(oBoxConfig);

            // Get Access Token and Client
            var tUseAdminToken = false; // fixed setting
            BoxClient oBoxClient = null;
            if (tUseAdminToken)
            {
                // Admin Token
                var oAdminToken = oBoxJWTAuth.AdminToken();
                oBoxClient = oBoxJWTAuth.AdminClient(oAdminToken);
            }
            else
            {
                // User Token
                var oUserToken = oBoxJWTAuth.UserToken("<API access user ID>");
                oBoxClient = oBoxJWTAuth.UserClient(oUserToken, "<API access user ID>");
                
            }

            // Get Filder Items (limit 500 items)
            var oFiles = oBoxClient.FoldersManager.GetFolderItemsAsync("<Folder ID>", 1000).Result;
            
            // ---
            // Enjoy, Welcome to BOX API world !
            // ---
        }
    }
}
