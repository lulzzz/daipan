﻿//-----------------------------------------------------------------------

// <copyright file="Transition.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, November 5, 2018 12:22:54 AM</date>
// </copyright>

//-----------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace CWF.Core
{
    /// <summary>
    /// Transition class
    /// </summary>
    public abstract class Transition
    {
        public Transition(int id, int from, int to, string condition)
        {
            Id = id;
            From = from;
            To = to;
            ConditionText = condition;
        }
        public override string ToString()
        {
            return "Id: " + Id.ToString() + ", From: " + From.ToString() + ", To: " + To.ToString() + ", ConditionText: " + ConditionText;
        }
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string ConditionText { get; set; }
        public abstract void SetMethodInfo(Type stateObjectType, MethodInfo mi);
        public abstract bool CheckCondition(object stateObject);
    }
    public class Transition<T> : Transition
    {
        private Func<T, bool> _conditionCheck;
        public Transition(int id, int from, int to, string condition) : base(id, from, to, condition)
        {
        }
        public override void SetMethodInfo(Type stateObjectType, MethodInfo mi)
        {
            Type genericDelegateType = typeof(Func<,>).MakeGenericType(stateObjectType, typeof(bool));
            _conditionCheck = (Func<T, bool>)Delegate.CreateDelegate(genericDelegateType, mi);
        }
        public bool CheckCondition(T obj)
        {
            return (_conditionCheck == null ? (true) : (_conditionCheck.Invoke(obj)));
        }
        public override bool CheckCondition(object stateObject)
        {
            if (stateObject is T)
                return CheckCondition((T)stateObject);
            else
            {
                var message = $"Tried to check a condition on an unknown type '{stateObject.GetType().FullName}' in a Transition with expected Type {typeof(T).FullName}";
                Logger.Error(message);
                throw new Exception(message);
            }
        }
    }
}
