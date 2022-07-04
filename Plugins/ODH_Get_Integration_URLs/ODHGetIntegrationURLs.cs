using Microsoft.Xrm.Sdk;
using ODH_Integrations.Integrations;
using System;

using static ODH_Integrations.Utility;

namespace ODH_Integrations
{
    public class ODHGetIntegrationURLs : IPlugin
    { 
        private Entity _entity;
        private IOrganizationService _service;
        private IPluginExecutionContext _context;
        private ITracingService _tracingService;
        private IIntegration integration;

        public void Execute(IServiceProvider serviceProvider)
        {
            #region ---- Standared Code -----
            InitializeFields(serviceProvider, ref _entity, ref _context, ref _service, ref _tracingService);
            #endregion

            if (_entity.LogicalName == "contact")
                integration = new Raya(_service);
            else return;
            integration.TestIntegration(_service);
        }
    }
}
