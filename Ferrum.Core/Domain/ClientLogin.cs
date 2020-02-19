using Ferrum.Core.Utils;

namespace Ferrum.Core.Domain
{
    public class ClientLogin
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        //todo: change to GUID, create new model to use in API
        public int ClientId { get; set; }

        public static ClientLogin CreateNewUser(string loginName)
        {
            var result = new ClientLogin
            {
                LoginName = loginName,
                Password = PasswordUtils.GeneratePassword(),
                Salt = PasswordUtils.GenerateSalt()
            };

            return result;
        }
    }
}
