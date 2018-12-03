﻿//-----------------------------------------------------------------------

// <copyright file="IExternalCommunicationService.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, December 3, 2018 3:34:35 PM</date>
// </copyright>

//-----------------------------------------------------------------------

using BreanosConnectors.Kpu.Communication.Common;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace AssistantInternalInterfaces
{
    /// <summary>
    /// ExternalCommunication interface for use with internal services (Core, Presenter, ...)
    /// </summary>
    public interface IExternalCommunicationService : IAssistantService
    {
        /// <summary>
        /// Forwards a collection of model updates to a collection of connectionIds
        /// </summary>
        /// <param name="connectionIds"></param>
        /// <param name="updates"></param>
        /// <returns></returns>
        Task OnModelBatchUpdate(string[] connectionIds, ModelUpdate[] updates);
        /// <summary>
        /// Forwards a Kpu's MVVM package as a string like the ones created by
        /// BreanosConnector.SerializationHelper.Pack() or BreanosConnector.SerializationHelper.Serialize()
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="kpuId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SendKpuPackage(string connectionId, string kpuId, string data);
    }
}
