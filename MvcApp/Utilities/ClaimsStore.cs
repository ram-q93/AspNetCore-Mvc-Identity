using System.Collections.Generic;
using System.Security.Claims;

namespace MvcApp.Utilities
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
            {
                new Claim(Constants.CreateRole, Constants.CreateRole),
                new Claim(Constants.EditRole,Constants.EditRole),
                new Claim(Constants.DeleteRole ,Constants.DeleteRole),
                new Claim(Constants.ManageUserClaims,Constants.ManageUserClaims)
            };
    }


}
