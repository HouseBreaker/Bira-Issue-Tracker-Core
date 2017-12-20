namespace BiraIssueTrackerCore.Web.Infrastructure
{
    public static class PasswordConfiguration
    {
	    public const int MinLength = 1;

	    public const int MaxLength = 200;

	    public const string ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.";

	    public const string ConfirmationMessage = "The new password and confirmation password do not match.";

    }
}
