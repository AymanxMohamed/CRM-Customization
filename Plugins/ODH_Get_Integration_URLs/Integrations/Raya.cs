using Microsoft.Xrm.Sdk;

namespace ODH_Integrations.Integrations
{
    public class Raya : Integration
    {
        public Raya(IOrganizationService service)
        {
            IntegrationName = "raya";
            GetIntegrationData(service);
        }

        public override void TestIntegration(IOrganizationService service)
        {
           


            var task = new Entity("task");

            task["subject"] = "Test Get Integration Plugin";
            task["description"] = $"User Name: {Username}, Password: {Password}, Base Url: {BaseUrl}";

            service.Create(task);
        }
    }
}
