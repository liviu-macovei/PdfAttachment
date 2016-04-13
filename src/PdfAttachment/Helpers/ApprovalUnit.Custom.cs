using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PdfAttachment.Helpers
{
     public partial class ApprovalUnit
    {
        private bool isInvoiced = true;

        [JsonProperty(Required = Required.Default)]
        public bool IsInvoiced
        {
            get { return isInvoiced; }
            set { isInvoiced = value; }
        }

    }
}
