using TicketsApps.Models;

namespace TicketsApps.AppCode
{
    public class ClientServices
    {
        private static ClientModel _clientModel;
        public static void AddClient(ClientModel clientModel)
        {
            _clientModel = clientModel;
        }
        public static ClientModel GetClient()
        {
            return _clientModel;
        }

    }
}
