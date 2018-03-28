using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint.Client;

namespace SPOwithModernAuth

{
    class Program
    {
        static void Main(string[] args)
        {

            //https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azureacs
            //http://www.ktskumar.com/2016/05/authenticate-sharepoint-using-pnp-authentication-manager/

            /*
            https://noixno.sharepoint.com/teams/Collaborating/_layouts/15/appregnew.aspx
            
            Client Id:  	32fb502b-37cb-4dce-bf5b-0a6e4e83a4c5
            Client Secret:  	89CKbViOt4YpzK0Sx9OBIg2xAGjtCM/B6azS8v4+ZnA=
            Title:  	SPOwithModernAuth
            App Domain:  	www.noixno.com
            Redirect URI:  	https://noixno.sharepoint.com/teams/Collaborating/ 
            */

            /*
            https://noixno.sharepoint.com/teams/Collaborating/_layouts/15/appinv.aspx
            https://noixno-admin.sharepoint.com/_layouts/15/appinv.aspx

            <AppPermissionRequests AllowAppOnlyPolicy="true">
                <AppPermissionRequest Scope="http://sharepoint/content/tenant" Right="FullControl" />
            </AppPermissionRequests>
            */
             
            string siteUrl = "https://noixno.sharepoint.com/teams/Collaborating/";
            string acsAppId = "32fb502b-37cb-4dce-bf5b-0a6e4e83a4c5";
            string acsSupport = "89CKbViOt4YpzK0Sx9OBIg2xAGjtCM/B6azS8v4+ZnA=";//GetString("ACS App Secret");

            OfficeDevPnP.Core.AuthenticationManager authManager = new OfficeDevPnP.Core.AuthenticationManager();
            
            using (var context = authManager.GetAppOnlyAuthenticatedContext(siteUrl, acsAppId, acsSupport))
            {
                context.Load(context.Web);
                context.ExecuteQueryRetry();
                System.Console.WriteLine(context.Web.Title);
            }
            
            System.Console.ReadLine();
        }
    }
}
