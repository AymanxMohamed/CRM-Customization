using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations
{
    /// <summary>
    /// This Is The Concrete Implementation Of the Raya vendor 
    /// Integration API That has the required Methods for Communicating with the API
    /// 
    /// Author: Ayman Mohamed
    /// Date: 2022-07-04
    /// </summary>
    public class Raya : Integration
    {
        public Raya(IOrganizationService service)
        {
            IntegrationModel.IntegrationName = "raya";
            InitializeIntegrationData(service);
        }

        public override void Post(IOrganizationService service)
        {
            // : https://odh-lr10okka6dtgld.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/CRM_INTEG_WORK_ORD
            //ER_NUMBE_LOV / 1.0 / crmworkorderNumber /{ workordernumberValue}/{ workorderDescription}/{
            //   EnableFlag}/{ CREATE_UPDATE_DELETE}
            string workOrderNumberValue = "";
            string workOrderDescription = "";
            string enableFlag = "";
            string createUpdateDelete = "";
        
   
            string Url = $"{IntegrationModel.BaseUrl}CRM_INTEG_WORK_ORDER_NUMBE_LOV/1.0/crmworkorderNumber/{workOrderNumberValue}/{workOrderDescription}/{enableFlag}/{createUpdateDelete}";
            /*
            Mandatory parameters to be sent from crm are:
                crmworkorderNumber = Must include the number for crm workorder
                workorderDescription = Must include any description for the crm work order number.
                EnableFlag = Y(IF ACTIVE) or N if (DELETE)
             
            Optional parameter to be sent from crm are:
                CREATE_UPDATE_DELETE it just shows the mode you are sending the workorder number with.
             */
        }

        
        public override void TestIntegration(IOrganizationService service)
        {
            var task = new Entity("task");

            task["subject"] = "Test Get Integration Plugin";
            task["description"] = $"User Name: {IntegrationModel.Username}, Password: {IntegrationModel.Password}, Base Url: {IntegrationModel.BaseUrl}";

            service.Create(task);
        }
    }
}
