// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ChromaBroadcast
{
    /// <summary>
    /// Chroma Broadcast Status is triggered when there is a change of Broadcast state
    /// </summary>
    public enum RzChromaBroadcastStatus
    {
        /// <summary>
        /// Live occurs when all expected conditions are met
        /// </summary>
        Live = 1,

        /// <summary>
        /// NotLive occurs when one of the expected conditions is not fulfilled
        /// </summary>
        NotLive = 2
    }
}
