using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations.Raya
{
    public class CrmIntegrationLinesWithWorkorderHeader : Raya
    {
        public CrmIntegrationLinesWithWorkorderHeader(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        { }
        
    }
}