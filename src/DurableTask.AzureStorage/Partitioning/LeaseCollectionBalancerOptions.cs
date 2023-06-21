﻿//  ----------------------------------------------------------------------------------
//  Copyright Microsoft Corporation
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  ----------------------------------------------------------------------------------

namespace DurableTask.AzureStorage.Partitioning
{
    using System;

    /// <summary>
    /// Options to control various aspects of partition distribution happening within the host instance.
    /// </summary> 
    class LeaseCollectionBalancerOptions
    {
        /// <summary>
        /// Renew interval for all leases for partitions currently held by this instance.
        /// </summary>
        public TimeSpan RenewInterval { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Interval when this instance kicks off a task to compute if partitions are distributed evenly
        /// among known host instances. 
        /// </summary>
        /// <remarks>
        /// This determines the frequency at which a worker reads the table in the table partition manager.
        /// </remarks>
        public TimeSpan AcquireInterval { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Interval for which the lease is taken on Azure Blob representing an EventHub partition.  If the lease is not renewed within this 
        /// interval, it will cause it to expire and ownership of the partition will move to another instance.
        /// </summary>
        /// <remarks>
        /// The interval by which a worker extends the expiration time for each partition renewal in table partition manager.
        /// </remarks>
        public TimeSpan LeaseInterval { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Determines whether or not this set of leases should utilize lease stealing logic for rebalancing
        /// during scale-out operations.
        /// </summary>
        public bool ShouldStealLeases { get; set;} = true;

        /// <summary>
        /// Creates an instance of <see cref="LeaseCollectionBalancerOptions"/> with following default values:
        ///     a) RenewInterval = 10 seconds
        ///     b) AcquireInterval = 10 seconds
        ///     c) DefaultLeaseInterval = 30 seconds
        ///     d) MaxReceiveClients = 16,
        /// </summary>
        public static LeaseCollectionBalancerOptions DefaultOptions
        {
            get
            {
                return new LeaseCollectionBalancerOptions();
            }
        }
    }
}
