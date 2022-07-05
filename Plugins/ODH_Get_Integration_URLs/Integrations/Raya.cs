using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

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
        public Raya(IOrganizationService service, ITracingService tracingService) : base(tracingService)
        {
            IntegrationModel.IntegrationName = "raya";
            InitializeIntegrationData(service);
        }

        public override void Post(IOrganizationService service)
        {
            // : https://odh-lr10okka6dtgld.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/CRM_INTEG_WORK_ORD
            //ER_NUMBE_LOV / 1.0 / crmworkorderNumber /{ workordernumberValue}/{ workorderDescription}/{
            //   EnableFlag}/{ CREATE_UPDATE_DELETE}
            int workOrderNumberValue =203456;
            string workOrderDescription = "Test_DL";
            string enableFlag = "Y";
            string createUpdateDelete = "CREATE";

            
            string url = $"{IntegrationModel.BaseUrl}CRM_INTEG_WORK_ORDER_NUMBE_LOV/1.0/crmworkorderNumber/{workOrderNumberValue}/{workOrderDescription}/{enableFlag}/{createUpdateDelete}";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Accept = "application/json";

                httpRequest.Headers["Authorization"] = "Basic YWxpYWEuYXRlZkBvcmFzY29taGQuY29tOk9yYXNjb21AMjAyMg==";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    TracingService.Trace(result);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Api Error {ex.Message}" + ex.Message);
            }

            /*
            Mandatory parameters to be sent from crm are:
                crmworkorderNumber = Must include the number for crm workorder
                workorderDescription = Must include any description for the crm work order number.
                EnableFlag = Y(IF ACTIVE) or N if (DELETE)
             
            Optional parameter to be sent from crm are:
                CREATE_UPDATE_DELETE it just shows the mode you are sending the workorder number with.
             */
        }
    }
}
