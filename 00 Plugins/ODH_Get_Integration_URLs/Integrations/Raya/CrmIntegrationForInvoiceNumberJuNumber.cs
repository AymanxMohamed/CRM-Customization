using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations.Raya
{
    public class CrmIntegrationForInvoiceNumberJuNumber : Raya
    {
        public CrmIntegrationForInvoiceNumberJuNumber(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        { }


        
    }
}