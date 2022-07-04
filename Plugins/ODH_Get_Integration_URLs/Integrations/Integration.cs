using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace ODH_Integrations.Integrations
{
    public abstract class Integration : IIntegration
    {
        public string IntegrationName { get; set; }
        public Guid ConfigurationRecordId { get; set; }
        public ColumnSet ColumnSet { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }

        public Integration() => ConfigurationRecordId = new Guid("9449B9BB-96FB-EC11-82E5-000D3ADCA46C");


        /// <summary>
        /// This Method Goal is To Initialize The Integration Data.
        /// </summary>
        /// <param name="service"></param>
        protected void InitializeIntegrationData(IOrganizationService service)
        {
            ColumnSet = new ColumnSet($"odh_{IntegrationName}_username", $"odh_{IntegrationName}_password", $"odh_{IntegrationName}_baseurl");
            var integrationData =  service.Retrieve("odh_configurations", ConfigurationRecordId, ColumnSet);

            Username = (string) integrationData[$"odh_{IntegrationName}_username"];
            Password = (string) integrationData[$"odh_{IntegrationName}_password"];
            BaseUrl = (string) integrationData[$"odh_{IntegrationName}_baseurl"];
        }

        public abstract void TestIntegration(IOrganizationService service);
    }
}
