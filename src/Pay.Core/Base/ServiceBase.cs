namespace Pay.Services.Responses
{
    public class ServiceBase
    {
        private IList<string> _notifications { get; set; }

        public ServiceBase()
        {
            _notifications = new List<string>();
        }

        public IReadOnlyList<string> Notifications => _notifications.ToList();
        public bool IsSuccessfully => !_notifications.Any();

        public bool HasNotifications => _notifications.Any();

        #region Methods
        protected ServiceBase AddNotification(string message)
        {
            _notifications.Add(message);
            return this;
        }

        protected ServiceBase AddNotifications(IList<string> notifications)
        {
            notifications
                .ToList()
                .ForEach(n => _notifications.Add(n));

            return this;
        }

        protected ServiceBase ClearNotifications()
        {
            _notifications.Clear();
            return this;
        }
        #endregion
    }
}