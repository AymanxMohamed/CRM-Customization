using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations.Raya
{
    public class CrmIntegrationLinesUsingQoutationOnly : Raya
    {
        public CrmIntegrationLinesUsingQoutationOnly(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        { }
        
    }
}