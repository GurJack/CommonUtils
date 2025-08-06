using System.ServiceModel;

namespace CommonService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MainServiceSession:MainService
    {
        public MainServiceSession() : base()
        {
            if (ServiceSecurityContext.Current == null || ServiceSecurityContext.Current.PrimaryIdentity == null)
                SessionInfo.UserName = "Неизвестный пользователь";
            else
            {
                SessionInfo.UserName = !ServiceSecurityContext.Current.PrimaryIdentity.IsAuthenticated
                    ? "Неизвестный пользователь"
                    : $"{ServiceSecurityContext.Current.PrimaryIdentity.Name}";
                SetSessionInfo();
            }
        }
        public WebSessionInfo SetSessionInfo(string applicationName)
        {
            SessionInfo.ApplicationName = applicationName.Trim();
            SessionInfo.ServiceSession = OperationContext.Current.SessionId;
            return SessionInfo;
        }

        public WebSessionInfo GetSessionInfo()
        {

            return SessionInfo;
        }

    }
}
