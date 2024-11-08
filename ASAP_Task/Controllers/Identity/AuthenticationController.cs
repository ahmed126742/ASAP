using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ASAP.Application.Features.Users.CreateUser;
using ASAP.Domain.Entities;
using ASAP_Task.Authentication.Models.Dtos.Retrieval;
using ASAP_Task.Authentication.Static;
using ASAP_Task.Controllers;
using ASAP_Task.WebAPI.Authentication.Context;
using ASAP_Task.WebAPI.Authentication.Entity;
using ASAP_Task.WebAPI.Authentication.Extensions;
using ASAP_Task.WebAPI.Authentication.Models.Dtos.Processing;
using ASAP_Task.WebAPI.Authentication.Models.Dtos.Retrieval;
using ASAP_Task.WepApi.Authentication.Context;
using ASAP_Task.WepApi.Authentication.Models.Dtos.Processing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace ASAP_Task.WebAPI.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserApplicationManager _userManager;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthContext _authContext;
        private readonly UserController _userController;


        public AuthenticationController(
            UserApplicationManager userManager,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters,
            RoleManager<IdentityRole> roleManager,
            UserController userController
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _roleManager = roleManager;
            _userController = userController;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<AuthResultModel>> RegisterUser([FromBody] UserRegisterRequestDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.PhoneNumber == null || request.PhoneNumber == string.Empty)
                return BadRequest(MapToAuthResult(null, false, null, new List<string> { "Phone number is required!" }));

            var IdentityUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var registrationResult = await Register(IdentityUser, request.Password);
            if (!registrationResult.Result)
                return BadRequest(registrationResult);

            var user = new CreateUserRequest { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, PhoneNumber = request.PhoneNumber };
            await _userController.CreateUser(user, cancellationToken);
            return Ok(MapToAuthResult(null, true, null, null));
        }

        [HttpPost("RegisterSystemAdmin")]
        public async Task<ActionResult<AuthResultModel>> RegisterSystemAdmin([FromBody] UserRegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = new ApplicationUser { Email = request.Email, UserName = request.Email, PhoneNumber = request.PhoneNumber };
            var registrationResult = await Register(userIdentity, request.Password);
            if (!registrationResult.Result)
                return BadRequest(registrationResult);

            await _userManager.AddToRoleAsync(userIdentity, SystemRole.SystemAdmin);

            var user = new CreateUserRequest { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, PhoneNumber = request.PhoneNumber };
            await _userController.CreateUser(user, CancellationToken.None);
            return Ok(MapToAuthResult(null, true, null, null));
        }

        [Authorize(Roles = SystemRole.SystemAdmin)]
        [HttpPost("CreateSystemUser")]
        public async Task<ActionResult<AuthResultModel>> CreateSystemUser([FromBody] UserRoleRegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var applicationUser = new ApplicationUser { Email = request.Email, UserName = request.Email, PhoneNumber = request.PhoneNumber };
            var registrationResult = await Register(applicationUser, request.Password);
            if (!registrationResult.Result)
                return BadRequest(registrationResult);

            if (request.RoleType != null)
                await _userManager.AddToRoleAsync(applicationUser, request.RoleType.Value.GetEnumDescription());

            var user = new CreateUserRequest { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, PhoneNumber = request.PhoneNumber };
            await _userController.CreateUser(user, CancellationToken.None);
            return Ok(MapToAuthResult(null, true, null, null));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<LoginUserResultModel>> LoginUser([FromBody] UserLoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
                return BadRequest(MapToAuthResult(null, false, null, new List<string> { "User is not exist!" }));

            var isValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!isValid)
                return BadRequest(MapToAuthResult(null, false, null, new List<string> { "Password is wrong!" }));

            var userResponse = await _userController.GetUserByEmail(existingUser.Email);
            return Ok(await PopulateLoginUserResultModel(await GenerateToken(existingUser), userResponse, await _userManager.GetRolesAsync(existingUser)));
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<object>> ResetPassword([FromBody] UserLoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
                return BadRequest(MapToAuthResult(null, false, null, new List<string> { "User is not exist!" }));

            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            if (string.IsNullOrEmpty(token))
                return BadRequest(MapToAuthResult(null, false, null, new List<string> { "something went wrong!" }));

            var result = await _userManager.ResetPasswordAsync(existingUser,token, request.Password);
            if (!result.Succeeded)
                return BadRequest(MapToAuthResult(null, false, null, result.Errors.Select(x => x.Description).ToList()));

            var user = await _userController.GetUserByEmail(existingUser.Email);
            return Ok(MapToAuthResult(null, true, null, new List<string>()));
        }


        //[HttpPost("ValidateUser")]
        //public async Task<ActionResult<AuthResultModel>> ValidateUser([FromBody] ValidateUserDate request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (request.PhoneNumber == null || request.PhoneNumber == string.Empty)
        //        return BadRequest(MapToAuthResult(null, false, null, new List<string> { "Phone number is required!" }));

        //    var validatedUser = await VerifyUser(request);
        //    if (validatedUser.Result == false)
        //        return BadRequest(validatedUser);

        //    return Ok(MapToAuthResult(result: true));
        //}

        //[HttpPost]
        //[Route("SignInUsingMobileNumber")]
        //public async Task<ActionResult<CreateOTPResponse>> SignInUsingMobileNumber(CreateOTPRequest request, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var existingUser = await _userManager.FindByPhoneNumberAsync(request.MobileNumber);
        //    if (existingUser == null || !existingUser.TwoFactorEnabled)
        //        return BadRequest(MapToAuthResult(null, false, null, new List<string> { "User does not exist!" }));

        //    // Create OTP and save then retrieve it back to the user 
        //    return Ok(await _mediator.Send(request));
        //}

        //[HttpPost]
        //[Route("ValidateOtp")]
        //public async Task<ActionResult<LoginUserResultModel>> ValidateOtp(ValidateOtpRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(request);
        //    if (!result.Succeeded)
        //        return BadRequest(MapToAuthResult(null, false, null, result.errors));

        //    var existingUser = await _userManager.FindByPhoneNumberAsync(request.MobileNumber);
        //    if (existingUser == null)
        //        return BadRequest(MapToAuthResult(null, false, null, new List<string> { "User is not exist!" }));

        //    var userResponse = await _mediator.Send(new GetByUserNameRequest { UserName = existingUser.UserName });
        //    return Ok(await PopulateLoginUserResultModel(await GenerateToken(existingUser), userResponse));
        //}

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResultModel>> RefreshToken([FromBody] TokenRequestDto request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await VerifyAndGenerateResult(request);
            if (result == null)
                return BadRequest(MapToAuthResult(result: false, errors: new List<string> { "Invalid Tokens" }));

            return Ok(result);
        }

        //[HttpPost("UpdateAuthenticatedUserBasicData")]
        //[Authorize]
        //public async Task<ActionResult<UpdateUserBasicDataResponse>> UpdateUserBasicData(UpdateUserBasicDataRequest request,
        //    CancellationToken cancellationToken)
        //{
        //    await UpdateAuthenticatedUser(request.PhoneNumber);
        //    var response = await _mediator.Send(request, cancellationToken);
        //    return Ok(response);
        //}

        //internal async Task UpdateUser(string oldUserEmail, string email, string phoneNumber)
        //{
        //    var userIdentity = await _userManager.FindByEmailAsync(oldUserEmail);
        //    userIdentity.Email = email;
        //    userIdentity.UserName = email;
        //    userIdentity.PhoneNumber = phoneNumber;
        //    await _userManager.UpdateAsync(userIdentity);
        //}

        private async Task<AuthResultModel> Register(ApplicationUser user, string password)
        {
            if (user.PhoneNumber != string.Empty || user.PhoneNumber != null)
            {
                user.TwoFactorEnabled = true;
                var existingMobileNumber = await _userManager.FindByPhoneNumberAsync(user.PhoneNumber);
                if (existingMobileNumber != null)
                    return MapToAuthResult(null, false, null, new List<string> { "Mobile number already exist please go to log in page!" });
            }

            var existedUser = await _userManager.FindByEmailAsync(user.Email);
            if (existedUser != null)
                return MapToAuthResult(null, false, null, new List<string> { "User is already exist!" });


            var isCreated = await _userManager.CreateAsync(user, password);
            if (!isCreated.Succeeded)
                return MapToAuthResult(null, false, null, isCreated.Errors.Select(x => x.Description).ToList());

            return MapToAuthResult(null, true, null, null);
        }

        private async Task<LoginUserResultModel> PopulateLoginUserResultModel(AuthResultModel authModel, User userResponse, IList<string> roles)
        {
            return new LoginUserResultModel
            {
                AuthenticationResult = authModel,
                User = new UserModel
                {
                    Id = userResponse.Id,
                    Email = userResponse.Email,
                    PhoneNumber = userResponse.PhoneNumber,
                    FirstName = userResponse.FirstName,
                    SecondName = userResponse.LastName,
                    Role = roles.FirstOrDefault()
                }
            };
        }

        private async Task<AuthResultModel> GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTConfig:Secret_Key").Value);
            // Token Descriptor to input all need data to jwt
            var expiry = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JWTConfig:ExpiryTimeFrame").Value));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(await GetUserClaimsAsync(user)),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JWTConfig:ExpiryTimeFrame").Value)),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var generatedToken = tokenHandler.WriteToken(securityToken);

            user.RefreshToken = GenerateRefreshToken();
            user.JwtId = securityToken.Id;
            user.IsRevoked = false;
            user.IsUsed = false;
            user.AddDate = DateTime.Now;
            user.RefreshTokenExpiryTime = expiry.AddMinutes(5);

            await _userManager.UpdateAsync(user);

            return MapToAuthResult(generatedToken, true, user.RefreshToken, null);
        }

        // why this algorithm
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            var userResponse = await _userController.GetUserByEmail(user.Email);
            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                 new Claim("id", userResponse.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims != null)
                claims.AddRange(userClaims);

            // should be after next line 
            #region custome roleClaims

            //var roles = await _roleManager.Roles.Where(x => userRoles.Contains(x.Name)).ToListAsync();
            //var RolesClaims = await _authContext.RoleClaims
            //    .Where(x => roles.Select(x => x.Id).Contains(x.RoleId))
            //    .ToListAsync();

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            //    var roleClaims = RolesClaims.Where(x => x.RoleId == role.Id);
            //    foreach (var roleClaim in roleClaims)
            //    {
            //        claims.Add(roleClaim.ToClaim());
            //    }
            //}

            #endregion

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null)
                    continue;

                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                if (roleClaims != null)
                    claims.AddRange(roleClaims);
            }
            return claims;
        }

        private async Task<AuthResultModel> VerifyAndGenerateResult(TokenRequestDto request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {

                _tokenValidationParameters.ValidateLifetime = false;

                var tokenInVerification = jwtTokenHandler.ValidateToken(request.Token, _tokenValidationParameters, out var validToken);
                if (validToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                        return null;
                }

                var expiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiryDateTime = TimeStampToDateTime(expiryDate);
                if (expiryDateTime > DateTime.Now)
                    return MapToAuthResult(result: false, errors: new List<string> { "Expired token" });

                var user = await _userManager.FindUserByRefreshTokenAsync(request.RefreshToken);
                if (user == null || user?.RefreshToken == null || user?.IsUsed == true || user?.IsRevoked == true)
                    return MapToAuthResult(result: false, errors: new List<string> { "Invalid token" });

                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (user?.JwtId != jti)
                    return MapToAuthResult(result: false, errors: new List<string> { "Invalid token" });

                if (user?.RefreshTokenExpiryTime < DateAndTime.Now)
                    return MapToAuthResult(result: false, errors: new List<string> { "Expired token" });

                user.IsUsed = true;
                await _userManager.UpdateAsync(user);
                return await GenerateToken(user);
            }
            catch (Exception ex)
            {
                return MapToAuthResult(result: false, errors: new List<string> { ex.Message });
            }
        }

        private async Task<AuthResultModel> VerifyUser(ValidateUserDate request)
        {
            if (request.PhoneNumber != string.Empty || request.PhoneNumber != null)
            {
                var existingMobileNumber = await _userManager.FindByPhoneNumberAsync(request.PhoneNumber);
                if (existingMobileNumber != null)
                    return MapToAuthResult(null, false, null, new List<string> { "Mobile number already exist please go to log in page!" });
            }

            var userValidatorResult = new UserValidator<ApplicationUser>();
            var userResult = await userValidatorResult.ValidateAsync(_userManager, new ApplicationUser { Email = request.Email, UserName = request.Email, PhoneNumber = request.PhoneNumber });
            if (!userResult.Succeeded)
                return MapToAuthResult(result: false, errors: userResult.Errors.Select(x => x.Description).ToList());

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return MapToAuthResult(null, false, null, new List<string> { "Email is already exist!" });

            var passwordResult = new PasswordValidator<ApplicationUser>();
            var res = await passwordResult.ValidateAsync(_userManager, new ApplicationUser { Email = request.Email }, request.Password);
            if (res.Succeeded)
                return MapToAuthResult(result: true);

            return MapToAuthResult(result: false, errors: res.Errors.Select(x => x.Description).ToList());
        }

        // to do 
        private DateTime TimeStampToDateTime(long timeStamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(timeStamp);
        }

        private AuthResultModel MapToAuthResult(string token = null, bool result = false, string refreshToken = null, List<string> errors = null)
        {
            return new AuthResultModel
            {
                Result = result,
                Token = token,
                RefreshToken = refreshToken,
                Errors = errors?.Select(x => x).ToList(),
            };
        }

        //private async Task UpdateAuthenticatedUser(string phoneNumber)
        //{
        //    var authenticatedUser = await _userManager.FindByNameAsync(_userContext.UserName);
        //    if (authenticatedUser == null)
        //        throw new Exception("User not Exist!");

        //    authenticatedUser.PhoneNumber = phoneNumber;
        //    await _userManager.UpdateAsync(authenticatedUser);
        //}
    }
}
