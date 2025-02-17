using Microsoft.AspNetCore.Components;
using Radzen;

namespace MealMentor.Client.State
{
    /// <summary>
    /// Wrapper for Radzen Notification Service
    /// </summary>
    public class NotificationState
    {
        private NotificationService NotificationService { get; set; }

        public NotificationState(NotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        private NotificationMessage? Message { get; set; }

        /// <summary>
        /// Notify after navigation if a message is set.
        /// </summary>
        public void ShowPendingMessages()
        {
            if (Message != null)
            {
                NotificationService.Notify(Message);
                Message = null;
            }
        }

        public void Notify(NotificationSeverity resultState, string summary , string detail)
        {
            NotificationService.Notify(resultState, summary, detail);
        }

        public void NotifyAfterNavigate(NotificationSeverity resultState, string summary, string detail)
        {
            Message = new NotificationMessage()
            {
                Severity = resultState,
                Summary = summary,
                Detail = detail
            };
        }
    }
}
