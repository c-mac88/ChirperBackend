using Chirper.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Chirper.API.Infrastructure
{
    public class AuthorizationRepository : IDisposable
    {
        private UserManager<ChirperUser> _userManager;
        private ChirperDataContext _chirperDataContext;

        public AuthorizationRepository()
        {
            _chirperDataContext = new ChirperDataContext();
            var userStore = new UserStore<ChirperUser>(_chirperDataContext);
            _userManager = new UserManager<ChirperUser>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var user = new ChirperUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DisplayName = model.DisplayName, 
                UserName = model.EmailAddress,
                Email = model.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<ChirperUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}