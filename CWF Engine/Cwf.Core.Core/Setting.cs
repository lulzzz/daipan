﻿//-----------------------------------------------------------------------

// <copyright file="Setting.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, November 5, 2018 12:22:54 AM</date>
// </copyright>

//-----------------------------------------------------------------------


namespace CWF.Core
{
    /// <summary>
    /// Setting.
    /// </summary>
    public class Setting
    {        
        /// <summary>
        /// Setting name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Settings value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Setting attributes.
        /// </summary>
        public Attribute[] Attributes { get; set; }

        /// <summary>
        /// Creates a new setting.
        /// </summary>
        /// <param name="name">Setting name.</param>
        /// <param name="value">Setting value.</param>
        /// <param name="attributes">Setting attributes.</param>
        public Setting(string name, string value, Attribute[] attributes)
        {
            Name = name;
            Value = value;
            Attributes = attributes;
        }
    }
}
