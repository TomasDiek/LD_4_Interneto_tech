using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using LD_4_Interneto_tech.Dto;
using LD_4_Interneto_tech.Errors;
using LD_4_Interneto_tech.Extensions;
using LD_4_Interneto_tech.Interfaces;
using LD_4_Interneto_tech.Models;
using System.Net;
using System.Net.Mail;

namespace LD_4_Interneto_tech.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;
        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.uow = uow;
        }

        // api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);

            ApiError apiError = new ApiError();

            if (user == null)
            {
                apiError.ErrorCode=Unauthorized().StatusCode;
                apiError.ErrorMessage="Invalid user name or password";
                apiError.ErrorDetails="This error appear when provided user id or password does not exists";
                return BadRequest(apiError);
            }

            var loginRes = new LoginResDto();
            loginRes.UserName = user.Username;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            ApiError apiError = new ApiError();

            if (loginReq.UserName.IsEmpty() || loginReq.Password.IsEmpty())
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User name or password can not be blank";
                return BadRequest(apiError);
            }

            if (await uow.UserRepository.UserAlreadyExists(loginReq.UserName)) {
                apiError.ErrorCode=BadRequest().StatusCode;
                apiError.ErrorMessage="User already exists, please try different user name";
                return BadRequest(apiError);
            }                

            uow.UserRepository.Register(loginReq.UserName, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        
        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Reset passowrd
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await uow.UserRepository.GetUserByResetToken(resetPasswordDto.ResetToken);
            if (user == null)
            {
                return BadRequest("Invalid or expired reset token.");
            }

            byte[] passwordHash, passwordKey;
            uow.UserRepository.HashPassword(resetPasswordDto.NewPassword, out passwordHash, out passwordKey);

            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            await uow.SaveAsync();

            return Ok("Password reset successfully.");
        }


        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset(PasswordResetRequestDto resetRequestDto)
        {
            var user = await uow.UserRepository.GetUserByUsername(resetRequestDto.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username.");
            }

            user.ResetToken = GenerateResetToken();
            await uow.SaveAsync();

            // Send reset instructions email
            SendResetInstructionsEmail(user.Email, user.ResetToken);

            return Ok("Password reset instructions sent successfully.");
        }


        private string GenerateResetToken()
        {
            return Guid.NewGuid().ToString();
        }
        private string HashResetToken(string resetToken)
        {
            // You can hash the reset token if needed
            return resetToken;
        }
        private void SendResetInstructionsEmail(string email, string resetToken)
        {
            // Email settings (replace with your SMTP server details)
            string smtpServer = "smtp-mail.outlook.com";
            int smtpPort = 587; // Example port number
            string smtpUsername = "tomdie1@outlook.com";
            string smtpPassword = "Random17s";
            bool enableSsl = true;

            // Sender email address
            string fromEmail = "tomdie1@outlook.com";

            // Recipient email address
            string toEmail = email;

            // Email subject
            string subject = "Password Reset Instructions";

            // Email body
            string body = $"Dear user,\n\nTo reset your password, please click the following link: http://localhost:4200/reset-password?token={resetToken}\n\nThis link will expire after a certain period.\n\nRegards,\nHomeFinder Team";

            // Create SMTP client
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.EnableSsl = enableSsl;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                // Create email message
                using (MailMessage mailMessage = new MailMessage(fromEmail, toEmail))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = false;

                    // Send email
                    smtpClient.Send(mailMessage);
                }
            }
        }



    }
}