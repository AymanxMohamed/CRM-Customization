using Microsoft.Xrm.Sdk;

namespace ODH_Integrations.Integrations
{
    public interface IIntegration
    {
        void TestIntegration(IOrganizationService service);
    }
}
