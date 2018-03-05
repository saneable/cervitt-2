using KaribouAlpha.Models;
using KaribouAlpha.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KaribouAlpha.Authentication
{
    public class AuthenticationRepository : IDisposable
    {
        private KaribouAlphaContext _context;
        private UserStore _userStore;
        private UserManager _userManager;

        public AuthenticationRepository()
        {
            UserValidator<User, long> userValidator;

            this._context = new KaribouAlphaContext();
            this._userStore = new UserStore(this._context);
            this._userManager = new UserManager(this._userStore);
            this._userManager.UserTokenProvider = new DataProtectorTokenProvider<User, long>(Startup.DataProtectionProvider.Create("ASP.NET Identity"))
            {
                TokenLifespan = TimeSpan.FromHours(24)
            }
            as IUserTokenProvider<User, long>;
            userValidator = (UserValidator<User, long>)this._userManager.UserValidator;
            userValidator.RequireUniqueEmail = true;
            userValidator.AllowOnlyAlphanumericUserNames = false;
        }

        public void Dispose()
        {
            this._userManager.Dispose();
            this._userStore.Dispose();
            this._context.Dispose();
        }

        public async Task<User> FindAsync(UserLoginInfo loginInfo)
        {
            User user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> RegisterNewUser(RegisterUserDTO registerUserDTO)
        {
            User user = new User();

            user.FirstName = registerUserDTO.FirstName;
            user.LastName = registerUserDTO.LastName;
            user.UserName = registerUserDTO.Email;
            user.Email = registerUserDTO.Email;
            user.RegistrationDate = DateTime.Now;
            user.SecurityStamp =Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if(result.Succeeded == true)
            {
                registerUserDTO.NewUserID = user.Id;
            }

            return result;
        }

        public async Task<IdentityResult> RegisterNewExternalUser(RegisterExternalUserDTO registerExternalUserDTO)
        {
            User user = new User();

            user.FirstName = registerExternalUserDTO.FirstName;
            user.LastName = registerExternalUserDTO.LastName;
            user.UserName = registerExternalUserDTO.Email;
            user.Email = registerExternalUserDTO.Email;
            user.RegistrationDate = DateTime.Now;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.EmailConfirmed = true;
            IdentityResult result = await _userManager.CreateAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(long userId, UserLoginInfo login)
        {
            IdentityResult result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public Client FindClient(string clientId)
        {
            var client = this._context.Clients.Find(clientId);

            return client;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await this._userManager.FindAsync(userName, password);

            return user;
        }

        public long FindUserExists(string email)
        {
            var user = this._context.Users.Where(a => a.UserName == email).FirstOrDefault();
            if (user != null)
            {
                return user.Id;
            }
            else
            {
                return 0;
            }
        }

    

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            RefreshToken existingToken = this._context.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                bool result = await RemoveRefreshToken(existingToken);
            }

            this._context.RefreshTokens.Add(token);

            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            RefreshToken refreshToken = await this._context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                this._context.RefreshTokens.Remove(refreshToken);

                return await this._context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            this._context.RefreshTokens.Remove(refreshToken);

            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            RefreshToken refreshToken = await this._context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public async Task<String> GenerateEmailConfirmationToken(long userId)
        {
            return this._userManager.GenerateEmailConfirmationToken(userId);
        }

        internal Task<IdentityResult> ConfirmEmailAsync(long userId, string confirmationToken)
        {
            return this._userManager.ConfirmEmailAsync(userId, confirmationToken);
        }

        internal User FindUserByUserName(string userName)
        {
            return this._userManager.FindByName(userName);
        }

        internal Task<bool> IsEmailConfirmedAsync(long userId)
        {
            return this._userManager.IsEmailConfirmedAsync(userId);
        }

        internal Task<string> GeneratePasswordResetTokenAsync(long userId)
        {
            return this._userManager.GeneratePasswordResetTokenAsync(userId);
        }

        internal Task<IdentityResult> ResetPasswordAsync(long userId, string passwordResetToken, string newPassword)
        {
            return this._userManager.ResetPasswordAsync(userId, passwordResetToken, newPassword);
        }

        internal IdentityResult ChangePassword(string userName, string currentPassword, string newPassword)
        {
            User user =  this._userManager.FindByName(userName);

            if(user == null)
            {
                return IdentityResult.Failed("User not found");
            }

            return this._userManager.ChangePassword(user.Id, currentPassword, newPassword);
        }

        public int ForgotPassword(ForgotPassword model)
        {
            this._context.ForgotPasswords.Add(model);
            return this._context.SaveChanges();
        }

        internal IdentityResult UpdateLoginDates(User user)
        {
            if (user == null)
            {
                return IdentityResult.Failed("User not found");
            }

            user.LastLoginDate = user.CurrentLoginDate;
            user.CurrentLoginDate = DateTime.Now;

            return this._userManager.Update(user);
        }


        
        public string CheckCode(string code)
        {
            var forgotPwdModel = this._context.ForgotPasswords.Where(a => a.OTP == code).SingleOrDefault();
            if(forgotPwdModel != null)
            {
                return forgotPwdModel.Email;
            }
            //return string.Empty;
            return null;
        }

        public KaribouAlphaContext  GetDbContext()
        {
            return this._context;
        }


    }
}