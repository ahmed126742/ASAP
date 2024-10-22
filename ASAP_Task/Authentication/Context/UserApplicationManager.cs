using ASAP_Task.WebAPI.Authentication.Entity;
using ASAP_Task.WepApi.Authentication.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ASAP_Task.WebAPI.Authentication.Context
{
    public class UserApplicationManager : UserManager<ApplicationUser>
    {
        private readonly AuthContext _authContext;

        public UserApplicationManager(AuthContext authContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _authContext = authContext;
        }

        public async Task<ApplicationUser> FindUserByRefreshTokenAsync(string refreshToken)
        {
            return await _authContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }

        public async Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
        {

            return await _authContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
