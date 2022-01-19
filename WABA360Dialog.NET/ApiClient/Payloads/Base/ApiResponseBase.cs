﻿using System.Collections.Generic;
using Newtonsoft.Json;
using WABA360Dialog.ApiClient.Payloads.Models;
using WABA360Dialog.ApiClient.Payloads.Models.Common;
using WABA360Dialog.Common.Interfaces;

namespace WABA360Dialog.ApiClient.Payloads.Base
{
    public abstract class ClientApiResponseBase : IClientApiResponse
    {
        [JsonProperty("errors")]
        public IEnumerable<ErrorObject> Error { get; set; }

        [JsonProperty("meta")]
        public ClientApiMeta Meta { get; set; }
    }
}