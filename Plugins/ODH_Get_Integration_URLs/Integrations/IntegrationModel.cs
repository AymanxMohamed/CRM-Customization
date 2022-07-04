using Microsoft.Xrm.Sdk.Query;
using System;

namespace ODH.Integrations.Plugins.Integrations
{
    /// <summary>
    /// A Model That Carry The main Information for the integrations
    /// Author: Ayman Mohamed
    /// Date: 2022-07-04
    /// </summary>
    public class IntegrationModel
    {
        public string IntegrationName { get; set; }
        public Guid ConfigurationRecordId { get; set; }
        public ColumnSet ColumnSet { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
    }
}
