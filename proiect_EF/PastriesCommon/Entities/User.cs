using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PastriesCommon.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        //public string UserRole { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
