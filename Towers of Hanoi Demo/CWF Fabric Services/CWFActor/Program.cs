﻿//-----------------------------------------------------------------------

// <copyright file="Program.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, December 3, 2018 3:34:35 PM</date>
// </copyright>

//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace CWFActor
{
    internal static class Program
    {
        /// <summary>
        /// Dies ist der Einstiegspunkt des Diensthostprozesses.
        /// </summary>
        private static void Main()
        {
            try
            {
                // Diese Zeile registriert einen Actordienst zum Hosten Ihrer Akteurklasse bei der Service Fabric-Laufzeit.
                // Der Inhalt der Dateien "ServiceManifest.xml" und "ApplicationManifest.xml"
                // wird automatisch aufgefüllt, wenn Sie dieses Projekt erstellen.
                // Weitere Informationen finden Sie unter https://aka.ms/servicefabricactorsplatform.

                ActorRuntime.RegisterActorAsync<CWFActor>(
                   (context, actorType) => new ActorService(context, actorType)).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
