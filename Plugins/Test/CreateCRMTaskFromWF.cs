using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ITIDemo.Plugins.Utility;

namespace ITIDemo.Plugins
{


    public sealed class CreateCRMTaskFromWF : CodeActivity
    {
        private Entity _entity;
        private IWorkflowContext _workflowContext;
        private IOrganizationService _service;
        private ITracingService _tracingService;

        [Input("Task Subject")]
        [Default("Enter values here")]
        public InArgument<string> TaskSubject { get; set; }


        [Input("Description")]
        [Default("Enter values here")]
        public InArgument<string> TaskDescription { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            #region ---- Standared Code -----
                InitializeFields(executionContext, ref _entity, ref _workflowContext, ref _service, ref _tracingService);
            #endregion

            Entity task = new Entity("task");

            task["subject"] = TaskSubject.Get<string>(executionContext);
            task["description"] = TaskDescription.Get<string>(executionContext);

            _service.Create(task);
        }
    }

}
