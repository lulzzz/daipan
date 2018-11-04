﻿//-----------------------------------------------------------------------

// <copyright file="CwfTimer.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, November 5, 2018 12:22:54 AM</date>
// </copyright>

//-----------------------------------------------------------------------


using System;
using System.Threading;
using System.Diagnostics;

namespace CWF.Core
{
    /// <summary>
    /// Cwf timer.
    /// </summary>
    public class CwfTimer
    {
        /// <summary>
        /// Timer callback.
        /// </summary>
        public TimerCallback TimerCallback { get; set; }
        /// <summary>
        /// State.
        /// </summary>
        public object State { get; set; }
        /// <summary>
        /// Period.
        /// </summary>
        public TimeSpan Period { get; set; }

		private bool _doWork;

        /// <summary>
        /// Creates a new timer.
        /// </summary>
        /// <param name="timerCallback">Timer callback.</param>
        /// <param name="state">State.</param>
        /// <param name="period">Period.</param>
        public CwfTimer(TimerCallback timerCallback, object state, TimeSpan period)
        {
            TimerCallback = timerCallback;
            State = state;
            Period = period;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
			_doWork = true;

            var thread = new Thread(() =>
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    while(_doWork)
                    {
                        if (stopwatch.ElapsedMilliseconds >= Period.TotalMilliseconds)
                        {
                            stopwatch.Reset();
                            stopwatch.Start();
                            TimerCallback.Invoke(State);
                        }
                        Thread.Sleep(100);
                    }
                });

            thread.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
		public void Stop()
		{
			_doWork = false;
		}
    }
}
