﻿//-----------------------------------------------------------------------

// <copyright file="ActorEventSource.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, December 3, 2018 3:34:35 PM</date>
// </copyright>

//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace ToHActor
{
    [EventSource(Name = "MyCompany-CWF.Fabric.Services-ToHActor")]
    internal sealed class ActorEventSource : EventSource
    {
        public static readonly ActorEventSource Current = new ActorEventSource();

        static ActorEventSource()
        {
            // Eine Problemumgehung für den Fall, dass ETW-Aktivitäten erst nachverfolgt werden, nachdem die Tasksinfrastruktur initialisiert wurde.
            // Dieses Problem wird in .NET Framework 4.6.2 behoben.
            Task.Run(() => { });
        }

        // Der Instanzkonstruktor ist privat, um Singletonsemantik zu erzwingen.
        private ActorEventSource() : base() { }

        #region Schlüsselwörter
        // Ereignisschlüsselwörter können zum Kategorisieren von Ereignissen verwendet werden. 
        // Jedes Schlüsselwort ist eine Bitkennzeichnung. Ein einzelnes Ereignis kann mehreren Schlüsselwörtern (über die Eigenschaft "EventAttribute.Keywords") zugeordnet werden.
        // Schlüsselwörter müssen als eine öffentliche Klasse namens "Keywords" in der "EventSource" definiert werden, die sie verwendet.
        public static class Keywords
        {
            public const EventKeywords HostInitialization = (EventKeywords)0x1L;
        }
        #endregion

        #region Ereignisse
        // Definiert eine Instanzmethode für jedes Ereignis, das Sie aufzeichnen möchten, und wendet ein Attribut [Event] darauf an.
        // Der Methodenname ist der Name des Ereignisses.
        // Übergeben Sie alle Parameter, die Sie mit dem Ereignis aufzeichnen möchten (nur primitive Integertypen, "DateTime", GUID und Zeichenfolgen sind zulässig).
        // Jede Ereignismethodenimplementierung sollte überprüfen, ob die Ereignisquelle aktiviert ist. Wenn dies der Fall ist, sollte die Methode "WriteEvent()" zum Auslösen des Ereignisses aufgerufen werden.
        // Die Anzahl und die Typen der an jede Ereignismethode übergebenen Argumente müssen genau mit den Elementen übereinstimmen, die an "WriteEvent()" übergeben werden.
        // Versehen Sie alle Methoden, die kein Ereignis definieren, mit dem Attribut [NonEvent].
        // Weitere Informationen finden Sie unter https://msdn.microsoft.com/de-de/library/system.diagnostics.tracing.eventsource.aspx.

        [NonEvent]
        public void Message(string message, params object[] args)
        {
            if (this.IsEnabled())
            {
                string finalMessage = string.Format(message, args);
                Message(finalMessage);
            }
        }

        private const int MessageEventId = 1;
        [Event(MessageEventId, Level = EventLevel.Informational, Message = "{0}")]
        public void Message(string message)
        {
            if (this.IsEnabled())
            {
                WriteEvent(MessageEventId, message);
            }
        }

        [NonEvent]
        public void ActorMessage(Actor actor, string message, params object[] args)
        {
            if (this.IsEnabled()
                && actor.Id != null
                && actor.ActorService != null
                && actor.ActorService.Context != null
                && actor.ActorService.Context.CodePackageActivationContext != null)
            {
                string finalMessage = string.Format(message, args);
                ActorMessage(
                    actor.GetType().ToString(),
                    actor.Id.ToString(),
                    actor.ActorService.Context.CodePackageActivationContext.ApplicationTypeName,
                    actor.ActorService.Context.CodePackageActivationContext.ApplicationName,
                    actor.ActorService.Context.ServiceTypeName,
                    actor.ActorService.Context.ServiceName.ToString(),
                    actor.ActorService.Context.PartitionId,
                    actor.ActorService.Context.ReplicaId,
                    actor.ActorService.Context.NodeContext.NodeName,
                    finalMessage);
            }
        }

        // Für sehr häufig auftretende Ereignisse kann es vorteilhaft sein, Ereignisse mithilfe der WriteEventCore-API auszulösen.
        // Dies führt zu einer effizienteren Parameterverarbeitung, erfordert aber die explizite Zuweisung der EventData-Struktur und unsicheren Code.
        // Definieren Sie zum Aktivieren dieses Codepfads das bedingte Kompilierungssymbol UNSAFE, und aktivieren Sie die Unterstützung von unsicherem Code in den Projekteigenschaften.
        private const int ActorMessageEventId = 2;
        [Event(ActorMessageEventId, Level = EventLevel.Informational, Message = "{9}")]
        private
#if UNSAFE
            unsafe
#endif
            void ActorMessage(
            string actorType,
            string actorId,
            string applicationTypeName,
            string applicationName,
            string serviceTypeName,
            string serviceName,
            Guid partitionId,
            long replicaOrInstanceId,
            string nodeName,
            string message)
        {
#if !UNSAFE
            WriteEvent(
                    ActorMessageEventId,
                    actorType,
                    actorId,
                    applicationTypeName,
                    applicationName,
                    serviceTypeName,
                    serviceName,
                    partitionId,
                    replicaOrInstanceId,
                    nodeName,
                    message);
#else
                const int numArgs = 10;
                fixed (char* pActorType = actorType, pActorId = actorId, pApplicationTypeName = applicationTypeName, pApplicationName = applicationName, pServiceTypeName = serviceTypeName, pServiceName = serviceName, pNodeName = nodeName, pMessage = message)
                {
                    EventData* eventData = stackalloc EventData[numArgs];
                    eventData[0] = new EventData { DataPointer = (IntPtr) pActorType, Size = SizeInBytes(actorType) };
                    eventData[1] = new EventData { DataPointer = (IntPtr) pActorId, Size = SizeInBytes(actorId) };
                    eventData[2] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                    eventData[3] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                    eventData[4] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                    eventData[5] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                    eventData[6] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                    eventData[7] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                    eventData[8] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                    eventData[9] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                    WriteEventCore(ActorMessageEventId, numArgs, eventData);
                }
#endif
        }

        private const int ActorHostInitializationFailedEventId = 3;
        [Event(ActorHostInitializationFailedEventId, Level = EventLevel.Error, Message = "Actor host initialization failed", Keywords = Keywords.HostInitialization)]
        public void ActorHostInitializationFailed(string exception)
        {
            WriteEvent(ActorHostInitializationFailedEventId, exception);
        }
        #endregion

        #region Private Methoden
#if UNSAFE
            private int SizeInBytes(string s)
            {
                if (s == null)
                {
                    return 0;
                }
                else
                {
                    return (s.Length + 1) * sizeof(char);
                }
            }
#endif
        #endregion
    }
}
