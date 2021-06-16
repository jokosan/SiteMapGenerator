﻿using System;

namespace SiteMapGenerator.Bll.Models
{
    public class UrlResult
    {
        public int IdUrlResult { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }
        public long? WebsiteLoadingSpeed { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? PageTestDate { get; set; }
        public int? Elapsed { get; set; }
    }
}