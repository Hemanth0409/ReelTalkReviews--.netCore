namespace ReelTalkReviews.Helper
{
    public class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"
<a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"">Reset Password</a>
";
        }
    }
}
