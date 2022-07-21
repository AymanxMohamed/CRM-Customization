using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations.Raya
{
    public class CrmIntegrationWorkOrderNumberLov : Raya
    {
        public CrmIntegrationWorkOrderNumberLov(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        { }
        
    }
}