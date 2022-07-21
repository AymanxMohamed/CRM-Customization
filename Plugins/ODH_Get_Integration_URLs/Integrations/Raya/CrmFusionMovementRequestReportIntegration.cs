using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations.Raya
{
    public class CrmFusionMovementRequestReportIntegration : Raya
    {
        public CrmFusionMovementRequestReportIntegration(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        { }
        
    }
}