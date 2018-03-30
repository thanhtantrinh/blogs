using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace cPanel.Identity
{
    public class CustomIdentity : IIdentity
    {
        private FormsAuthenticationTicket _ticket;
        private string[] _userDataPieces;
        public CustomIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
            _userDataPieces = ticket.UserData.Split("|".ToCharArray());
        }
        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
        }
        public bool IsAuthenticated
        {
            get { return true; }
        }
        public string Name
        {
            get { return _ticket.Name; }
        }

        public string[] Roles
        {
            get
            {
                return _userDataPieces[0].Split(';');
            }
        }
        public int ID
        {
            get
            {
                int id = 0;
                int.TryParse(_userDataPieces[1], out id);

                return id;
            }
        }

        public string DisplayName
        {
            get
            {
                return _userDataPieces[2];
            }
        }
        public long UserID
        {
            get
            {
                long id = 0;
                if (_userDataPieces.Count() >= 3)
                {
                    long.TryParse(_userDataPieces[1], out id);
                }
                return id;
            }
        }
    }
}