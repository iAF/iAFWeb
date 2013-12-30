﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iAFWebHost.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace iAFWebHost.Models
{
    /// <summary>
    /// Database entity
    /// </summary>
    [DataContract(Name="Url")]
    public class UrlModel
    {

        public UrlModel()
        {
            Users = new List<string>();
            Tags = new List<string>();
            UtcDate = DateTime.UtcNow;
        }

        /// <summary>
        /// ID is a ulong integer generated by Autoincrement operation in Couchbase cluster.
        /// </summary>
        /// <value>
        /// Id
        /// </value>
        [DataMember(Name="Id")]
        public string Id { get; set; }

        /// <summary>
        /// ShortId is a Base58 Encoding of the ulong Id integer.
        /// </summary>
        /// <value>
        /// ShortId
        /// </value>
        [DataMember(Name="ShortId")]
        public string ShortId
        {
            get
            {
                ulong longId;
                if (!String.IsNullOrEmpty(Id) && ulong.TryParse(Id, out longId))
                    return longId.EncodeBase58();
                else
                    return String.Empty;
            }
        }

        /// <summary>
        /// Gets the host of the supplied URL variable base on Uri.TryCreate method.
        /// Return an empty string if we are unable to parse our host value.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        [DataMember(Name = "Host")]
        public string Host
        {
            get
            {
                Uri uri;
                if (Uri.TryCreate(Href, UriKind.Absolute, out uri))
                    return uri.Host;
                else
                    return String.Empty;
            }
            set
            {

            }
        }

        /// <summary>
        /// Main url that is stored in the database and used for redirection purposes.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [DataMember(Name = "Href")]
        [Required(ErrorMessage = "Url is required")]
        [StringLength(2048)]
        public string Href { get; set; }

        [DataMember(Name = "HrefA")]
        [StringLength(2048)]
        public string HrefActual { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response code.Recorded during initial HEAD operation
        /// </summary>
        /// <value>
        /// The HTTP response code.
        /// </value>
        [DataMember]
        public int HttpResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the length of the content as returned by HTTP Head operation.
        /// </summary>
        /// <value>
        /// The length of the content.
        /// </value>
        [DataMember]
        public long HttpContentLength { get; set; }

        /// <summary>
        /// Gets or sets the type of the content as returned by HTTP Head operation.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        [DataMember]
        public string HttpContentType { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response ip.
        /// </summary>
        /// <value>
        /// The HTTP response ip.
        /// </value>
        [DataMember]
        public string HttpResponseIP { get; set; }

        /// <summary>
        /// Gets or sets the HTTP time stamp. Stores the date and time of the initial HTTP HEAD request.
        /// </summary>
        /// <value>
        /// The HTTP time stamp.
        /// </value>
        [DataMember]
        public DateTime HttpTimeStamp { get; set; }

        [DataMember(Name = "Users")]
        public List<string> Users { get; set; }

        [DataMember(Name = "Tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Represent a dynamic (not stored in the database) absolute Url value 
        /// based on a shortId and a root system domain http://i.af
        /// </summary>
        /// <value>
        /// The short href.
        /// </value>
        [DataMember(Name = "ShortHref")]
        public string ShortHref
        {
            get
            {
                if (!String.IsNullOrEmpty(ShortId))
                    return String.Format("http://i.af/{0}", ShortId);
                else
                    return String.Empty;
            }
            set
            {

            }
        }

        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the flag. Internal record status.
        /// Possible Flag values are: 
        /// 0 = new record in inactive state (Inactive)
        /// 1 = retrieved from database (Enabled).
        /// </summary>
        /// <value>
        /// The flag.
        /// </value>
        [DataMember(Name = "Flag")]
        public int Flag { get; set; }

        [DataMember(Name = "HttpFlag")]
        public bool IsSuccessfulHttpRequest { get; set; }

        public DateTime UtcDate { get; set; }
    }
}