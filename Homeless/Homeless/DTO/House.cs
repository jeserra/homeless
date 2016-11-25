using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Homeless.DTO
{
    [DataTable("Houses")]
    public class House
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("ShortDescription")]
        public string ShortDescription { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
