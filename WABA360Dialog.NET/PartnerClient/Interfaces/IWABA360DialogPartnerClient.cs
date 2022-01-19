﻿using System.Threading;
using System.Threading.Tasks;
using WABA360Dialog.Common.Enums;
using WABA360Dialog.PartnerClient.Payloads;
using WABA360Dialog.PartnerClient.Payloads.Enums;

namespace WABA360Dialog.PartnerClient.Interfaces
{
    public interface IWABA360DialogPartnerClient
    {
        Task<CreatePartnerWhatsAppBusinessApiTemplateResponse> CreatePartnerWhatsAppBusinessApiTemplateAsync(string whatsAppBusinessApiAccountId, string name, TemplateCategory category, WhatsAppLanguage language, object components, CancellationToken cancellationToken = default);
        Task<GetClientBalanceResponse> GetClientBalanceAsync(string clientId, int fromMonth, int fromYear, CancellationToken cancellationToken = default);
        Task<GetPartnerChannelsResponse> GetPartnerChannelsAsync(int limit = 20, int offset = 0, string sort = null, object filters = null, CancellationToken cancellationToken = default);
        Task<GetPartnerClientsResponse> GetPartnerClientsAsync(int limit = 20, int offset = 0, string sort = null, object filters = null, CancellationToken cancellationToken = default);
        Task<GetPartnerWebhookUrlResponse> GetPartnerWebhookUrlAsync(CancellationToken cancellationToken = default);
        Task<GetPartnerWhatsAppBusinessApiTemplatesResponse> GetPartnerWhatsAppBusinessApiTemplatesAsync(string whatsAppBusinessApiAccountId, int limit = 20, int offset = 0, string sort = null, object filters = null, CancellationToken cancellationToken = default);
        Task<RemovePartnerWhatsAppBusinessApiTemplatesResponse> RemovePartnerWhatsAppBusinessApiTemplatesAsync(string whatsAppBusinessApiAccountId, string templateId, CancellationToken cancellationToken = default);
        Task<SetCancellationRequestOnChannelResponse> SetCancellationRequestOnChannelAsync(string clientId, string channelId, bool enabled, CancellationToken cancellationToken = default);
        Task<SetPartnerWebhookUrlResponse> SetPartnerWebhookUrlAsync(string webhookUrl, CancellationToken cancellationToken = default);
        Task<UpdateClientResponse> UpdateClientAsync(string clientId, string partnerPayload, CancellationToken cancellationToken = default);
        Task<TokenResponse> RequestOAuthTokenAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}