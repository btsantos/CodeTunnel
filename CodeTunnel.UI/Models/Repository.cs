using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace CodeTunnel.UI.Models
{
    public class Repository
    {
        public Repository(string json)
        {
            var jRepository = JObject.Parse(json);
            HasWiki = jRepository.Value<bool>("has_wiki");
            Created = jRepository.Value<DateTime>("created_at");
            Description = jRepository.Value<string>("description");
            OpenIssues = jRepository.Value<int>("open_issues");
            GitUrl = jRepository.Value<string>("git_url");
            Watchers = jRepository.Value<int>("watchers");
            LastPushed = jRepository.Value<DateTime>("pushed_at");
            Url = jRepository.Value<string>("url");
            MirrorUrl = jRepository.Value<string>("mirror_url");
            IsFork = jRepository.Value<bool>("fork");
            Homepage = jRepository.Value<string>("homepage");
            SvnUrl = jRepository.Value<string>("svn_url");
            HasDownloads = jRepository.Value<bool>("has_downloads");
            Size = jRepository.Value<long>("size");
            Private = jRepository.Value<bool>("private");
            LastUpdated = jRepository.Value<DateTime>("updated_at");
            HasIssues = jRepository.Value<bool>("has_issues");
            Name = jRepository.Value<string>("name");
            Forks = jRepository.Value<int>("forks");
            Language = jRepository.Value<string>("language");
            CloneUrl = jRepository.Value<string>("clone_url");
            SshUrl = jRepository.Value<string>("ssh_url");
            HtmlUrl = jRepository.Value<string>("html_url");
            Id = jRepository.Value<long>("id");

            Owner = new GitUser(jRepository["owner"].ToString());
        }


        public bool HasWiki { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public int OpenIssues { get; set; }
        public string GitUrl { get; set; }
        public int Watchers { get; set; }
        public DateTime LastPushed { get; set; }
        public string Url { get; set; }
        public string MirrorUrl { get; set; }
        public bool IsFork { get; set; }
        public string Homepage { get; set; }
        public string SvnUrl { get; set; }
        public bool HasDownloads { get; set; }
        public long Size { get; set; }
        public bool Private { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool HasIssues { get; set; }
        public GitUser Owner { get; set; }
        public string Name { get; set; }
        public int Forks { get; set; }
        public string Language { get; set; }
        public string CloneUrl { get; set; }
        public string SshUrl { get; set; }
        public string HtmlUrl { get; set; }
        public long Id { get; set; }
    }
}