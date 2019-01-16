using System;
using System.Collections.Generic;
using System.Text;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public interface INotificationService
    {
        void SendNotification(string notification);
    }
}
