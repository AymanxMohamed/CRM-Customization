using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using ODH.Integrations.Plugins.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ODH.Integrations.Plugins.Integrations.Raya
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
        public Raya(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(service, targetEntity, tracingService)
        {
            
            IntegrationModel.IntegrationName = "raya";
            InitializeIntegrationData(service);
        }

        public override void Post(IOrganizationService service)
        {
            var url = GetUrl(service);
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = "Basic YWxpYWEuYXRlZkBvcmFzY29taGQuY29tOk9yYXNjb21AMjAyMg==";
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Api Error {ex.Message}" + ex.Message);
            }
        }

        /// <summary>
        /// This Method Purpose is to Get The Data From the database Then Generate the URL
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private string GetUrl(IOrganizationService service)
        {
            var quoteId = (Guid)IntegrationModel.TargetEntity.Attributes["quoteid"];

            var quoteEntity = GetEntity(service, "quote", quoteId, new ColumnSet("pro_workorderid", "pro_property", "quotenumber"));

            var quoteNumber = (string)quoteEntity["quotenumber"];
            var workOrderReference = (EntityReference)quoteEntity["pro_workorderid"];
            var propertyReference = (EntityReference)quoteEntity["pro_property"];

            var propertyEntiy = GetEntity(service, "product", propertyReference.Id, new ColumnSet("name"));
            string propertyName = (string)propertyEntiy["name"];

            var workOrderEntity = GetEntity(service, "msdyn_workorder", workOrderReference.Id, new ColumnSet("msdyn_name"));
            var workOrderNumber = (string)workOrderEntity["msdyn_name"];
            string workOrderNumber_PropertyName = $"{workOrderNumber}_{propertyName}";

            return $"{IntegrationModel.BaseUrl}CRM_INTEG_WORK_ORDER_NUMBE_LOV/1.0/crmworkorderNumber/{quoteNumber}/{workOrderNumber_PropertyName}/Y/CREATE";
        }
    }
}
