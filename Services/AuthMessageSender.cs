using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Identity.UI.Services;
using Twilio.Types;

namespace _2FAUsingIdentityServer4
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
    public class AuthMessageSender : IEmailSender , ISmsSender 
    {
       
        private readonly IOptions<TwilioSettings> _twilioSettings;

        

        public AuthMessageSender(IOptions<TwilioSettings> optionsEmailSettings)
        {
            _twilioSettings = optionsEmailSettings;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }

        public Task SendSmsAsync(string number, string message)
        {

            //var sid = "AC4bcab7875465ae040d589775212a1861";
            //var token = "5c1090f43a495c16978bddedf94ae7c8";
            //var from = "+19284400590";
            var sid = _twilioSettings.Value.Sid;
            var token = _twilioSettings.Value.Token;
            var from = _twilioSettings.Value.From;
            TwilioClient.Init(sid, token);
            MessageResource.CreateAsync(new PhoneNumber(number),
                from: new PhoneNumber(from),
                body: message);
            return Task.FromResult(0);
        }
    }
}
