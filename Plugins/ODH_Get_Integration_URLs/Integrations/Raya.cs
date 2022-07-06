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
        public Raya(IOrganizationService service, Entity targetEntity,ITracingService tracingService) : base(targetEntity, tracingService)
        {
            TracingService.Trace("Raya Line 16");
            IntegrationModel.IntegrationName = "raya";
            InitializeIntegrationData(service);
            TracingService.Trace("Raya Line 19");
        }

        public override void Post(IOrganizationService service)
        {
            TracingService.Trace($"Line 26, Class: Raya, Method: Post");
            // : https://odh-lr10okka6dtgld.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/CRM_INTEG_WORK_ORD
            //ER_NUMBE_LOV / 1.0 / crmworkorderNumber /{ workordernumberValue}/{ workorderDescription}/{
            //   EnableFlag}/{ CREATE_UPDATE_DELETE}
            int workOrderNumberValue = 203456;
            string workOrderDescription = "Test_DL";
            string enableFlag = "Y";
            string createUpdateDelete = "CREATE";

            TracingService.Trace($"Post Method Line 35 {workOrderDescription}");
            string url = $"{IntegrationModel.BaseUrl}CRM_INTEG_WORK_ORDER_NUMBE_LOV/1.0/crmworkorderNumber/{workOrderNumberValue}/{workOrderDescription}/{enableFlag}/{createUpdateDelete}";
            try
            {
                //var response = await IntegrationModel.Client.GetAsync(url);
                //var responseString = await response.Content.ReadAsStringAsync();
                //TracingService.Trace($"After Post Request {responseString}");
                //TestIntegration(service, responseString);

                TracingService.Trace("Inside Try");
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                TracingService.Trace("Inside Try line 55");

                httpRequest.Accept = "application/json";
                TracingService.Trace("Inside Try line 58");

                httpRequest.Headers["Authorization"] = "Basic YWxpYWEuYXRlZkBvcmFzY29taGQuY29tOk9yYXNjb21AMjAyMg==";
                TracingService.Trace("Inside Try line 61");


                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                TracingService.Trace("Inside Try line 65");

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    TracingService.Trace(result);
                }
                TracingService.Trace("Inside Try line 72");

                TracingService.Trace(httpResponse.StatusCode.ToString());
                TracingService.Trace("Inside Try line 75");
            }

            catch (Exception ex)
            {

                TracingService.Trace($"Inside Try line 81 {ex.Message}");

                throw new InvalidPluginExecutionException("Api Error {ex.Message}" + ex.Message);
            }

            //var url = "https://odh-lr10okka6dtg-ld.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/CRM_INTEG_WORK_ORDER_NUMBE_LOV/1.0/crmworkorderNumber/10/This_IS/1";


            /*
            Mandatory parameters to be sent from crm are:
                crmworkorderNumber = Must include the number for crm workorder
                workorderDescription = Must include any description for the crm work order number.
                EnableFlag = Y(IF ACTIVE) or N if (DELETE)
             
            Optional parameter to be sent from crm are:
                CREATE_UPDATE_DELETE it just shows the mode you are sending the workorder number with.
             */
            TracingService.Trace("End of Post Method");
        }



    }
}
