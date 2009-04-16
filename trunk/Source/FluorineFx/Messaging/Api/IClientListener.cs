/*
	FluorineFx open source library 
	Copyright (C) 2007 Zoltan Csibi, zoltan@TheSilentGroup.com, FluorineFx.com 
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/
using System;

namespace FluorineFx.Messaging.Api
{
    /// <summary>
    /// Interface to be notified when a Client is created or destroyed.
    /// </summary>
	[CLSCompliant(false)]
    public interface IClientListener
    {
        /// <summary>
        /// Notification that a Client instance was created.
        /// </summary>
        /// <param name="messageClient">The Client that was created.</param>
        void ClientCreated(IClient client);
        /// <summary>
        /// Notification that a Client is about to be destroyed.
        /// </summary>
        /// <param name="messageClient">The Client that will be destroyed.</param>
        void ClientDestroyed(IClient client);
    }
}
