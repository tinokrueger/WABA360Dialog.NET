﻿using Newtonsoft.Json;
using WABA360Dialog.ApiClient.Payloads.Models.Converters;

namespace WABA360Dialog.ApiClient.Payloads.Enums
{
    [JsonConverter(typeof(MessageStatusConverter))]
    public enum MessageStatus
    {
        delivered,
        read,
        sent,
        failed,
        deleted,
    }
    
    
}