﻿// ----------------------------------------------------------------------------------
//
// Copyright 2012 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ---------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.Storage.Common
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using System.Text;

    /// <summary>
    /// blob management
    /// </summary>
    internal class StorageBlobManagement : IStorageBlobManagement
    {
        private CloudBlobClient blobClient;

        /// <summary>
        /// init blob management
        /// </summary>
        /// <param name="client">a cloud blob object</param>
        public StorageBlobManagement(CloudBlobClient client)
        {
            if (null == client)
            {
                throw new RuntimeException(Resources.CloudBlobClientIsNull);
            }

            blobClient = client;
        }

        /// <summary>
        /// get a list of cloudblobcontainer in azure
        /// </summary>
        /// <param name="prefix">contaienr prefix</param>
        /// <param name="detailsIncluded">container listing details</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>An enumerable collectioni of cloudblobcontainer</returns>
        public IEnumerable<CloudBlobContainer> ListContainers(string prefix, ContainerListingDetails detailsIncluded, BlobRequestOptions options, OperationContext operationContext)
        {
            return blobClient.ListContainers(prefix, detailsIncluded, options, operationContext);
        }

        /// <summary>
        /// get container presssions
        /// </summary>
        /// <param name="container">a cloudblobcontainer object</param>
        /// <param name="accessCondition">access condition</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>the container's permission</returns>
        public BlobContainerPermissions GetContainerPermissions(CloudBlobContainer container, AccessCondition accessCondition, BlobRequestOptions options, OperationContext operationContext)
        {
            return container.GetPermissions(accessCondition, options, operationContext);
        }

        /// <summary>
        /// get an CloudBlobContainer instance in local
        /// </summary>
        /// <param name="name">container name</param>
        /// <returns>a CloudBlobContainer in local memory</returns>
        public CloudBlobContainer GetContainerReference(string name)
        {
            return blobClient.GetContainerReference(name);
        }

        /// <summary>
        /// create the container if not exists
        /// </summary>
        /// <param name="container">a cloudblobcontainer object</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>true if the container did not already exist and was created; otherwise false.</returns>
        public bool CreateContainerIfNotExists(CloudBlobContainer container, BlobRequestOptions requestOptions, OperationContext operationContext)
        {
            return container.CreateIfNotExists(requestOptions, operationContext);
        }

        /// <summary>
        /// delete container
        /// </summary>
        /// <param name="container">a cloudblobcontainer object</param>
        /// <param name="accessCondition">access condition</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        public void DeleteContainer(CloudBlobContainer container, AccessCondition accessCondition, BlobRequestOptions options, OperationContext operationContext)
        {
            container.Delete(accessCondition, options, operationContext);
        }

        /// <summary>
        /// set container permissions
        /// </summary>
        /// <param name="container"></param>
        /// <param name="permissions"></param>
        /// <param name="accessCondition"></param>
        /// <param name="options"></param>
        /// <param name="operationContext"></param>
        public void SetContainerPermissions(CloudBlobContainer container, BlobContainerPermissions permissions, AccessCondition accessCondition, BlobRequestOptions options, OperationContext operationContext)
        {
            container.SetPermissions(permissions, accessCondition, options, operationContext);
        }

        /// <summary>
        /// get blob reference with properties and meta data from server
        /// </summary>
        /// <param name="container">a cloudblobcontainer object</param>
        /// <param name="blobName">blob name</param>
        /// <param name="accessCondition">access condition</param>
        /// <param name="options">blob request options</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>return an ICloudBlob if the specific blob is existing on azure, otherwise return null</returns>
        public ICloudBlob GetBlobReferenceFromServer(CloudBlobContainer container, string blobName, AccessCondition accessCondition, BlobRequestOptions options, OperationContext operationContext)
        {
            try
            {
                ICloudBlob blob = container.GetBlobReferenceFromServer(blobName, accessCondition, options, operationContext);
                return blob;
            }
            catch(StorageException e)
            {
                if (e.IsNotFoundException())
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// list all blobs in sepecific containers
        /// </summary>
        /// <param name="container">a cloudblobcontainer object</param>
        /// <param name="prefix">blob prefix</param>
        /// <param name="useFlatBlobListing">use flat blob listing(whether treat "container/" as directory)</param>
        /// <param name="blobListingDetails">blob listing details</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>an enumerable collection of icloublob</returns>
        public IEnumerable<IListBlobItem> ListBlobs(CloudBlobContainer container, string prefix, bool useFlatBlobListing, BlobListingDetails blobListingDetails, BlobRequestOptions options, OperationContext operationContext)
        {
            return container.ListBlobs(prefix, useFlatBlobListing, blobListingDetails, options, operationContext);
        }

        /// <summary>
        /// whether the container is exists or not
        /// </summary>
        /// <param name="container">a cloudblobcontainer object</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>true if the specific container is existing, otherwise return false</returns>
        public bool IsContainerExists(CloudBlobContainer container, BlobRequestOptions options, OperationContext operationContext)
        {
            if (null == container)
            {
                return false;
            }
            else
            {
                return container.Exists(options, operationContext);
            }
        }

        /// <summary>
        /// whether the blob is exists or not
        /// </summary>
        /// <param name="blob">a icloudblob object</param>
        /// <param name="options">blob request option</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>true if the specific container is existing, otherwise return false</returns>
        public bool IsBlobExists(ICloudBlob blob, BlobRequestOptions options, OperationContext operationContext)
        {
            if (null == blob)
            {
                return false;
            }
            else
            {
                return blob.Exists(options, operationContext);
            }
        }

        /// <summary>
        /// delete azure blob
        /// </summary>
        /// <param name="blob">ICloudblob object</param>
        /// <param name="deleteSnapshotsOption">delete snapshots option</param>
        /// <param name="accessCondition">access condition</param>
        /// <param name="operationContext">operation context</param>
        /// <returns>an enumerable collection of icloublob</returns>
        public void DeleteICloudBlob(ICloudBlob blob, DeleteSnapshotsOption deleteSnapshotsOption, AccessCondition accessCondition, BlobRequestOptions options, OperationContext operationContext)
        {
            blob.Delete(deleteSnapshotsOption, accessCondition, options, operationContext);
        }
    }
}
