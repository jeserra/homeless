using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Homeless.Models
{
    public class House
    {
        public House()
        {
            Images = string.Empty; // new List<string>();
        }

        [JsonProperty("Id")]
        public int IdHouse
        {
            get;
            set;
        }
        [JsonProperty("ShortDescription")]
        public string ShortDescription
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        [JsonProperty("Location")]
        public string Location
        {
            get;
            set;
        }

        [JsonProperty("Images")]
        public string Images
        {
            get;
            set;
        }

        [JsonProperty("Price")]
        public Decimal Price
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public bool IsVisited
        {
            get;
            set;
        }

        public bool IsRented
        {
            get;
            set;
        }

        public bool IsCalled
        {
            get;
            set;
        }

        public string NameContact
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Rooms
        {
            get;
            set;
        }

        public string Floor
        {
            get;
            set;
        }

        public string Requisites
        {
            get;
            set;
        }


        [JsonProperty("Comments")]
        public string Comments
        {
            get;
            set;
        }

        public string Rating { get; set; }

        [JsonProperty("Lat")]
        public String Lat { get; set; }

        [JsonProperty("Lon")]
        public String Lon { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
