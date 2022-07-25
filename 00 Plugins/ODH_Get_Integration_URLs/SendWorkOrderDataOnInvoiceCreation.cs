using Microsoft.Xrm.Sdk;
using ODH.Integrations.Plugins.Helper;
using ODH.Integrations.Plugins.Integrations;
using ODH.Integrations.Plugins.Integrations.Raya;
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
        private Integration _integration;

        public void Execute(IServiceProvider serviceProvider)
        {
            Utility.InitializeFields(serviceProvider, ref _entity, ref _context, ref _service, ref _tracingService);
             _integration = GetIntegration();
            if (_integration == null) return;
            _integration.Post(_service);
        }

        public Integration GetIntegration()
        {
            if (_entity.LogicalName == "quote")
                return new Raya(_service, _entity, _tracingService);
            return null;
        }
    }
}