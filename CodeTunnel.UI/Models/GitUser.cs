using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace CodeTunnel.UI.Models
{
    public class GitUser
    {
        public GitUser(string json)
        {
            var jGitUser = JObject.Parse(json);
            GravatarId = Guid.Parse(jGitUser.Value<string>("gravatar_id"));
            ApiUrl = jGitUser.Value<string>("url");
            AvatarUrl = jGitUser.Value<string>("avatar_url");
            Id = jGitUser.Value<long>("id");
        }

        public Guid GravatarId { get; set; }
        public string ApiUrl { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public long Id { get; set; }
    }
}