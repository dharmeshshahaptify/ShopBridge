using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Helper
{
    public class SampleFilterModel : FilterModelBase
    {
        public string Term { get; set; }

        public SampleFilterModel() : base()
        {
            this.Limit = 3;
        }


        public override object Clone()
        {
            var jsonString = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject(jsonString, this.GetType());
        }
    }
}
