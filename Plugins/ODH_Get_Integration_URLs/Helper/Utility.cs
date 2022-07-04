using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace ODH.Integrations.Plugins.Helper
{
    /// <summary>
    /// This Utility Class Has the Standared Methods that are used In Each Plugin Or Work flow
    /// Author: Ayman Mohamed
    /// Date: 2022-06-29
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// This Method Get the reference Of the Current Entity And Checks if it's Registered Correctly Or not
        /// </summary>
        /// <param name="context">The Organization Context</param>
        private static void GetTargetEntity(IPluginExecutionContext context, ref Entity _entity)
        {
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity)
                _entity = entity;
        }

        /// <summary>
        /// This Method Get the reference Of the Current Entity And Checks if it's Registered Correctly Or not
        /// </summary>
        /// <param name="context">The Organization Context</param>
        private static void GetTargetEntity(IWorkflowContext context, ref Entity _entity)
        {
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity)
                _entity = entity;
        }

        /// <summary>
        /// Initializes common parameters to set the IPluginExecutionContext and IOrganizationServiceFactory
        /// </summary>
        /// <param name="serviceProvider">Object Of IServiceProvider</param>
        public static void InitializeFields(
            IServiceProvider serviceProvider,
            ref Entity currentEntity,
            ref IPluginExecutionContext context,
            ref IOrganizationService service,
            ref ITracingService tracingService
            )
        {
            // Obtain the execution context from the service provider 
            context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the organization service reference.
            service = ((IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(context.UserId);

            // Extract the tracing service for use in debugging sandboxed plug-ins.
            tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            GetTargetEntity(context, ref currentEntity);
        }

        /// <summary>
        /// Initializes common parameters to set the IPluginExecutionContext and IOrganizationServiceFactory
        /// </summary>
        /// <param name="serviceProvider">Object Of IServiceProvider</param>
        public static void InitializeFields(
            CodeActivityContext executionContext,
            ref Entity currentEntity,
            ref IWorkflowContext workflowContext,
            ref IOrganizationService service,
            ref ITracingService tracingService
            )
        {
            workflowContext = executionContext.GetExtension<IWorkflowContext>();

            service = executionContext.GetExtension<IOrganizationServiceFactory>().CreateOrganizationService(workflowContext.UserId);

            tracingService = executionContext.GetExtension<ITracingService>();

            GetTargetEntity(workflowContext, ref currentEntity);
        }

        public static bool IsRegisteredCorrectly(ref Entity currentEntity, string entityLogicalName) => currentEntity.LogicalName != entityLogicalName;
    }
}
