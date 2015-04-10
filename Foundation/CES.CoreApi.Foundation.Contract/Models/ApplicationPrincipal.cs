﻿using System.Security.Principal;
using System.Threading;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ApplicationPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private string[] _roles = null;

        public ApplicationPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        public static ApplicationPrincipal Current
        {
            get { return Thread.CurrentPrincipal as ApplicationPrincipal; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public string[] Roles
        {
            get
            {
                //Findout Role and set here 
                return _roles;
            }
        }

        public bool IsInRole(string role)
        {
            //Findout Role and set here 
            return true;
        }
    }
}