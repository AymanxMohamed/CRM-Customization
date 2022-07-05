using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Net.Http;

namespace ODH.Integrations.Plugins.Integrations
{
    /// <summary>
    /// An Abstract Class That Main Goal is to InitializeThe Integration Model
    /// Author: Ayman Mohamed
    /// Date: 2022-07-04
    /// </summary>
    public abstract class Integration : IIntegration
    {
        public ITracingService TracingService { get; set; }
        public IntegrationModel IntegrationModel { get; set; }
        public Integration(ITracingService tracingService)
        {
            TracingService = tracingService;
            IntegrationModel = new IntegrationModel
            {
                Client = new HttpClient(),
                ConfigurationRecordId = new Guid("9449B9BB-96FB-EC11-82E5-000D3ADCA46C")
            };
        }

        public void TestIntegration(IOrganizationService service, string whatToTest)
        {
            var task = new Entity("task");

            task["subject"] = "ODH.Itegrations.Plugins.SendWOrkOrderDataOnInvoiceCreation";
            task["description"] = $"User Name: {IntegrationModel.Username}.\n" +
                $"Password: {IntegrationModel.Password}.\n" +
                $"Base Url: {IntegrationModel.BaseUrl}.\n" +
                $"Test Text: {whatToTest}";

            service.Create(task);
        }

        /// <summary>
        /// This Method Goal is To Initialize The Integration Data.
        /// </summary>
        /// <param name="service"></param>
        protected void InitializeIntegrationData(IOrganizationService service)
        {
            IntegrationModel.ColumnSet = new ColumnSet($"odh_{IntegrationModel.IntegrationName}_username", $"odh_{IntegrationModel.IntegrationName}_password", $"odh_{IntegrationModel.IntegrationName}_baseurl");

            var integrationData =  service.Retrieve("odh_configurations", IntegrationModel.ConfigurationRecordId, IntegrationModel.ColumnSet);

            IntegrationModel.Username = (string) integrationData[$"odh_{IntegrationModel.IntegrationName}_username"];
            IntegrationModel.Password = (string) integrationData[$"odh_{IntegrationModel.IntegrationName}_password"];
            IntegrationModel.BaseUrl = (string) integrationData[$"odh_{IntegrationModel.IntegrationName}_baseurl"];
        }

        public abstract void Post(IOrganizationService service);
    }
}
