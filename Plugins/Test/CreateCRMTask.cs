using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ITIDemo.Plugins.Utility;

namespace ITIDemo.Plugins
{
    public class CreateCRMTask : IPlugin
    {
        private readonly string _entityLogicalName = "contact";
        private Entity _entity;
        private IOrganizationService _service;
        private IPluginExecutionContext _context;
        private ITracingService _tracingService;


        public void Execute(IServiceProvider serviceProvider)
        {
            #region ---- Standared Code -----
                InitializeFields(serviceProvider, ref _entity, ref _context, ref _service, ref _tracingService);
                if (!IsRegisteredCorrectly(ref _entity, _entityLogicalName)) return;
            #endregion

            Entity task = new Entity("task");

            task["subject"] = "Welcome to ITI Plugin";
            task["description"] = "This is the task description";

            _service.Create(task);
        }

    }
}
