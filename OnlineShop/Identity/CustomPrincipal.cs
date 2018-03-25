using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace OnlineShop.Identity
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity ident)
        {
            this._identity = ident;
        }
        CustomIdentity _identity;
        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return _identity.Roles.Contains(role);
        }
    }
}