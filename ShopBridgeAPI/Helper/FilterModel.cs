using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Model
{
    public class FilterModel
    {
        public String Term { get; set; }
        public DateTime MinDate { get; set; }
        public Boolean IncludeInactive { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }

}
