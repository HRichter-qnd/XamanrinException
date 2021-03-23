using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace XamarinException
{

    [Preserve(AllMembers = true)]
    public class Objects_From_Device
    {
        public List<From_Device> From_Device_List { get; set; } = new List<From_Device>();
    }

    [Preserve(AllMembers = true)]
    public class Lookups
    {
        public List<Charge> ChargenList { get; set; } = new List<Charge>();
    }

    [Preserve(AllMembers = true)]
    public abstract class BaseModel
    {

    }
    [Preserve(AllMembers = true)]
    public class Charge : BaseModel
    {
        [JsonProperty("ch_id")]
        public string CH_ID { get; set; }
        [JsonProperty("ch_bez")]
        public string CH_BEZ { get; set; }
        [JsonProperty("equip_id")]
        public decimal EQUIP_ID { get; set; }
        [JsonProperty("fname")]
        public string FNAME { get; set; }
        [JsonProperty("a_version")]
        public string A_VERSION { get; set; }
        [JsonProperty("start_t")]
        public DateTime START_T { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class From_Device : BaseModel
    {
        public decimal EQUIP_ID { get; set; }
        public string A_VERSION { get; set; }
        public string CH_ID { get; set; }
        public string TESTS_ID { get; set; }
        public decimal OCCURANCE { get; set; }
        public string KLASSE { get; set; }
        public DateTime ZEIT { get; set; }
        public decimal ISTWERT { get; set; }
        public string RESOURCE_ID { get; set; }
    }
}
