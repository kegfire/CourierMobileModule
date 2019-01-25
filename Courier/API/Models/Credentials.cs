using System;

namespace CourierMobile.Core.Core.API.Models
{
    [Serializable]
    public class Credentials
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}