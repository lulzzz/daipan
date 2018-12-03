﻿//-----------------------------------------------------------------------

// <copyright file="ObjectBase.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, December 3, 2018 3:34:35 PM</date>
// </copyright>

//-----------------------------------------------------------------------

///////////////////////////////////////////////////////////
//  ObjectBase.cs
//  Implementation of the Class ObjectBase
//  Generated by Enterprise Architect
//  Created on:      02-Feb-2018 10:26:53
//  Original author: bezdedeanu
///////////////////////////////////////////////////////////
namespace BlackboardClassLibrary.Commands
{
    /// <summary>
    /// Base class for Blackboard command objects
    /// </summary>
    public class ObjectBase : IObject
    {

        /// <summary>
        /// Public ctor
        /// </summary>
        public ObjectBase()
        {

        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ObjectBase()
        {

        }

        /// <summary>
        /// Clone method for ObjectBase
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// ProcessingStage for the Blackboard command
        /// </summary>
        public virtual ProcessingStage Stage { get; set; } = ProcessingStage.Initialized;

        /// <summary>
        /// Type identifier for the Blackboard command
        /// </summary>
        public virtual ObjectType Type { get; set; } = ObjectType.Unknown;

    }//end ObjectBase
}
