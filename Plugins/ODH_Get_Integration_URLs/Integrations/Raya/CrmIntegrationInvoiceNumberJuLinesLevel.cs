using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations.Raya
{
    public class CrmIntegrationInvoiceNumberJuLinesLevel : Raya
    {
        public CrmIntegrationInvoiceNumberJuLinesLevel(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        { }
        
    }
}