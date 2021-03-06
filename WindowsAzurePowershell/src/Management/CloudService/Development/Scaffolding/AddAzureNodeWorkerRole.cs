﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.CloudService.Development.Scaffolding
{
    using System.IO;
    using System.Management.Automation;
    using Microsoft.WindowsAzure.Management.Utilities.CloudService;
    using Microsoft.WindowsAzure.Management.Utilities.Common;
    using Microsoft.WindowsAzure.Management.Utilities.Common.XmlSchema.ServiceConfigurationSchema;
    using Microsoft.WindowsAzure.Management.Utilities.Properties;

    /// <summary>
    /// Create scaffolding for a new node worker role, change cscfg file and csdef to include the added worker role
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "AzureNodeWorkerRole"), OutputType(typeof(RoleSettings))]
    public class AddAzureNodeWorkerRoleCommand : AddRole
    {
        public AddAzureNodeWorkerRoleCommand() :
            base(Path.Combine(Resources.NodeScaffolding, RoleType.WorkerRole.ToString()), Resources.AddRoleMessageCreateNode, false)
        {

        }
    }
}