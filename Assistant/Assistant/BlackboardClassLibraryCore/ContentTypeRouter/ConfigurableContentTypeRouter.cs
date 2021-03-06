﻿//-----------------------------------------------------------------------

// <copyright file="ConfigurableContentTypeRouter.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, December 3, 2018 3:34:35 PM</date>
// </copyright>

//-----------------------------------------------------------------------

using BreanosConnectors.Kpu.Communication.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackboardClassLibraryCore
{
    class ConfigurableContentTypeRouter : ContentTypeRouterBase
    {
        public ConfigurableContentTypeRouter()
        {
            contentTypeList = new ContentTypeList();
            contentTypeList.ValueSet = new List<ContentType>();

            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Config(RoutingRequest request)
        {               
            foreach (string s in request.ContentTypes)
            {
                ContentType ct = contentTypeList.ValueSet.Find(x => x.Content.CompareTo(s) == 0);
                if (ct == null)
                {                  
                    var newCt = new ContentType() { Content = s };                    
                    newCt.Queues = new Dictionary<string, Value>();

                    contentTypeList.ValueSet.Add(newCt);
                    ct = newCt;

                    logger.Debug($"Content type {ct.Content} will be created.");
                }
                try
                {
                    ct.Queues.Remove("value1");
                    ct.Queues.Add("value1", new Value { QueueId = "1", QueueName = request.Path });
                }
                catch (Exception e)
                {
                    logger.Debug($"{e.Message}");
                }
            }
        }
    }
}
