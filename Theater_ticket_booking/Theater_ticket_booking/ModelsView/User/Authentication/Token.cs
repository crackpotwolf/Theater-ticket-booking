using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Theater_ticket_booking.ModelsView.UserModels 
{
    public class Token
    {
        public Token(string access_token, string username)
        {
            AccessToken = access_token;
            Username = username;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
