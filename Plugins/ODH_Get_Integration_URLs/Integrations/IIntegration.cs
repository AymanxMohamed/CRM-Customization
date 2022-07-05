using Microsoft.Xrm.Sdk;

namespace ODH.Integrations.Plugins.Integrations
{
    /// <summary>
    /// An Interface That has the Common Methods To Communicate With each API Configurations
    /// Author: Ayman Mohamed
    /// Date: 2022-07-04
    /// </summary>
    public interface IIntegration
    {
        void Post(IOrganizationService service);
    }
}
