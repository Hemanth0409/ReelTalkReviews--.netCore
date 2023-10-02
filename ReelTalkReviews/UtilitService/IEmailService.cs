using ReelTalkReviews.Models;

namespace ReelTalkReviews.UtilitService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
     }
}
