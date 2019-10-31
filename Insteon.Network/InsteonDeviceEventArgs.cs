﻿// <copyright company="INSTEON">
// Copyright (c) 2012 All Right Reserved, http://www.insteon.net
//
// This source is subject to the Common Development and Distribution License (CDDL). 
// Please see the LICENSE.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>
// <author>Dave Templin</author>
// <email>info@insteon.net</email>

using System;
using System.Collections.Generic;
using System.Text;

namespace Insteon.Network
{
    /// <summary>
    /// Provides notification when there is a change of status in an INSTEON device.
    /// </summary>
    public class InsteonDeviceEventArgs : EventArgs
    {
        /// <summary>
        /// Gets an object that represents the INSTEON device that has changed status.
        /// </summary>
        public InsteonDevice Device { get; private set; }
        internal InsteonDeviceEventArgs(InsteonDevice device)
        {
            this.Device = device;
        }
    }
    /// <summary>
    /// Represents the method that handles a status changed event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="data">An object that contains the event data.</param>
    public delegate void InsteonDeviceEventHandler(object sender, InsteonDeviceEventArgs data);
}
