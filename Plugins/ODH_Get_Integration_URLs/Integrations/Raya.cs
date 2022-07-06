using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using ODH.Integrations.Plugins.Helper;
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
        public Raya(IOrganizationService service, Entity targetEntity, ITracingService tracingService) : base(targetEntity, tracingService)
        {
            TracingService.Trace("Raya Line 16");
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
                    TracingService.Trace(result);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Api Error {ex.Message}" + ex.Message);
            }
            TracingService.Trace("End of Post Method");
        }

        private string GetUrl(IOrganizationService service)
        {
            var quoteId = (Guid)IntegrationModel.TargetEntity.Attributes["quoteid"];

            var quoteEntity = GetEntity(service, "quote", quoteId, new ColumnSet("pro_workorderid", "pro_property", "quotenumber"));
            // This will help you to get the All Columns # Eng Mahmoud // new ColumnSet(true);

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


/*

Attribute Key: statecode, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: msdyn_profitability, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: msdyn_ordertype, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: pro_revised, 
    Attribute Value: False

Attribute Key: pro_paymentstatus, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: createdon, 
    Attribute Value: 7/6/2022 10:01:08 AM

Attribute Key: quoteid, 
    Attribute Value: 3fcb3f8c-12fd-ec11-82e5-000d3adca46c

Attribute Key: pricingerrorcode, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: pro_isintegrationerror, 
    Attribute Value: False

Attribute Key: ownerid, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: totallineitemdiscountamount, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: pro_designconditionstemplate, 
    Attribute Value: 
    - 100 % of the quotation total amount to be paid upon approval.
    - The above mentioned prices are effective for one week from quotation date.
    - Phase 1: Concept & Mood board - highlighting the landscape elements & their locations + Conceptual spaces usage & circulation & indicate the selection of colors, materials, and finishes. (Submission in 7-10 days after quotation payment & approval date).
    - Phase 2: 3D Visualization - Going into details of the concept design phase & prepare 3D renders. (Submission in 10-14 days after approval of Phase 1)
    - Phase 3: Construction drawings - The approved 3D renders are further detailed & translated into technical drawings and BOQs (such as levels, dimensions, lighting, power, softscape, details ... etc) (Submission in 7-10 days after approval of Phase 2)
    - Phase 4: Price Offer - We shall proceed with getting a price offer for the whole job after the client approves Phase 3.
    - Each design phase is allowed 1 revision only.
    - Any extra revisions during any phase are subject to extra charges & time frame which will be sent in a separate quotation.
    - Any changes required in the Concept or 3D Viz phases after moving to the Construction drawings or Price offer phases will be subject to extra charges & time frame which will be sent in a separate quotation.
    - In case the customer fails to reply back on a design submission within 1 month maximum, the case will be considered closed and a new quotation will have to be issued if the client requested the design service again.
    - If the payment will be done through bank transfer; please include the transfer fees & a copy of the transfer confirmation should be sent to our office.

Attribute Key: totaltax, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: quotenumber, 
    Attribute Value: QUO-08547-R2W4N6

Attribute Key: msdyn_estimatedschedule, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: pro_retrieveitemsplugin, 
    Attribute Value: False

Attribute Key: totalamountlessfreight,
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: transactioncurrencyid, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: pro_retrieveitems, 
    Attribute Value: False

Attribute Key: totaldiscountamount, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: modifiedby, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: statuscode, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: modifiedonbehalfby, 
    Attribute Value:

Attribute Key: modifiedon, 
    Attribute Value: 7/6/2022 10:01:08 AM

Attribute Key: pro_createinvoice, 
    Attribute Value: False

Attribute Key: owningbusinessunit, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: totalamount, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: totallineitemamount, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: pro_quotationtemplate, 
    Attribute Value: Payment Installments will be through post-dated cheques as Follows:
        - 60% Down Payment
        - 20% After 30 days from payment confirmation to be paid through post dated cheques
        - 20% After 60 days from payment confirmation to be paid through post dated cheques.
        Payment Installments will be through post-dated cheques as Follows:
        - 50% Down Payment
        - 20% After 30 days from payment confirmation to be paid through post dated cheques.
        - 20% After 60 days from payment confirmation to be paid through post dated cheques.
        - 10% After 90 days from payment confirmation to be paid through post dated cheques.
        Payment Installments will be through post-dated cheques as Follows:
        - 50% Down Payment
        - 15% After 30 days from payment confirmation to be paid through post dated cheques.
        - 15% After 60 days from payment confirmation to be paid through post dated cheques.
        - 10% After 90 days from payment confirmation to be paid through post dated cheques.
        - 10% After ---days from payment confirmation.

Attribute Key: msdyn_competitive, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: pro_getquotationreferencenumber, 
    Attribute Value: False

Attribute Key: msdyn_estimatedbudget, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: pro_revenueallocationodh, 
    Attribute Value: False

Attribute Key: createdby, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: msdyn_feasible, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: processid, 
    Attribute Value: 00000000-0000-0000-0000-000000000000

Attribute Key: customerid, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: owninguser, 
    Attribute Value: Microsoft.Xrm.Sdk.EntityReference

Attribute Key: willcall, 
    Attribute Value: False

Attribute Key: skippricecalculation, 
    Attribute Value: Microsoft.Xrm.Sdk.OptionSetValue

Attribute Key: pro_vatwn, 
    Attribute Value: 0

Attribute Key: revisionnumber, 
    Attribute Value: 0

Attribute Key: exchangerate, 
    Attribute Value: 1.000000000000

Attribute Key: totaltax_base, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: totaldiscountamount_base, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: totalamountlessfreight_base, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: totallineitemamount_base, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: totalamount_base, 
    Attribute Value: Microsoft.Xrm.Sdk.Money

Attribute Key: totallineitemdiscountamount_base, 
    Attribute Value: Microsoft.Xrm.Sdk.Money
 */