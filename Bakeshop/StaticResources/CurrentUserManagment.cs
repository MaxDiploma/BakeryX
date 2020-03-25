using Bakeshop.EF.Models;

namespace Bakeshop.StaticResources
{
    public static class CurrentUserManagment
    {
        private static BakeshopWorker _user;

        public static void FillUserWithData(BakeshopWorker worker)
        {
            _user = worker;
        }

        public static BakeshopWorker GetCurrentUser()
        {
            return _user;
        }

        public static void ClearCurrentUser()
        {
            _user = null;
        }
    }
}
