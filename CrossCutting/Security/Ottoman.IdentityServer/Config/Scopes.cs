namespace Ottoman.IdentityServer.Config
{
    using System.Collections.Generic;

    using IdentityServer3.Core.Models;

    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
                {                    
                    new Scope
                    { 
                        Name = "Test",
                        DisplayName = "Test",
                        Description = "Allow the application to manage galleries on your behalf.",
                        Type = ScopeType.Resource 
                    }
                };
        }
    }
}
