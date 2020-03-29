namespace PhiladelphiaInmateLocator.WebApi.Framework
{
    using Microsoft.AspNetCore.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    //
    // Summary:
    //     Specifies that the class or method that this attribute is applied to requires
    //     the specified authorization.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizeData
    {

        //
        // Summary:
        //     Initializes a new instance of the Microsoft.AspNetCore.Authorization.AuthorizeAttribute
        //     class.
        public AuthorizeAttribute ()
        {
            string roles = string.Empty;
        }

        //
        // Summary:
        //     Gets or sets a comma delimited list of schemes from which user information is
        //     constructed.
        public string AuthenticationSchemes { get; set; }
        //
        // Summary:
        //     Gets or sets the policy name that determines access to the resource.
        public string Policy { get; set; }
        //
        // Summary:
        //     Gets or sets a comma delimited list of roles that are allowed to access the resource.
        public string Roles { get; set; }
    }
}
