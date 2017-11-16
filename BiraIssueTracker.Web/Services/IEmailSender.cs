﻿using System.Threading.Tasks;

namespace BiraIssueTrackerCore.Web.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
