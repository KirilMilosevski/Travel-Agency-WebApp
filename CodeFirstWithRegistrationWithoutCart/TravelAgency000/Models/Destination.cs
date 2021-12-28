using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgency000.Models
{
    public class Destination
    {
        [Key]
        public int ItemId { get; set; }
        [JsonProperty("Location")]
        public string ItemName { get; set; }
        [JsonProperty("Price")]
        public int ItemPrice { get; set; }
        public string Photo{ get; set; }


        public string DetailsVacation { get; set; }
        public Destination()
        {

        }
    }
}
