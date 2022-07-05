using Microsoft.Xrm.Sdk;
using ODH.Integrations.Plugins.Helper;
using ODH.Integrations.Plugins.Integrations;
using System;

namespace ODH.Integrations.Plugins
{
    /// <summary>
    /// This Plugin Supposed To Be Able to send APi Requests According to the Vendor
    /// Author: Ayman Mohamed
    /// Date: 2022-07-04
    /// </summary>
    public class SendWorkOrderDataOnInvoiceCreation : IPlugin
    { 
        private Entity _entity;
        private IOrganizationService _service;
        private IPluginExecutionContext _context;
        private ITracingService _tracingService;
        private IIntegration integration;

        public void Execute(IServiceProvider serviceProvider)
        {
            Utility.InitializeFields(serviceProvider, ref _entity, ref _context, ref _service, ref _tracingService);
            _tracingService.Trace("line 24");
            if (_entity.LogicalName == "contact")
                integration = new Raya(_service, _tracingService);
            else return;
            integration.Post(_service);
        }
    }
}
