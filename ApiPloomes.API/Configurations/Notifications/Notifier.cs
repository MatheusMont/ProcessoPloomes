using ApiPloomes.DOMAIN.Interfaces.INotifier;
using ApiPloomes.DOMAIN.Models;

namespace ApiPloomes.API.Configurations.Notifications
{
    //Classe que faz o gerenciamento das notificações durante a execução das requisições
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
