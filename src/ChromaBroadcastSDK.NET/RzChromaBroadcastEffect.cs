// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;

namespace ChromaBroadcast
{
    /// <summary>
    /// Chroma Broadcast effect structure contains five(5) broadcasted RGB color value from ChromaSDK / Synapse
    /// </summary>
    public struct RzChromaBroadcastEffect
    {
        /// <summary>
        /// ChromaLink 1
        /// </summary>
        public Color ChromaLink1;

        /// <summary>
        /// ChromaLink 2
        /// </summary>
        public Color ChromaLink2;

        /// <summary>
        /// ChromaLink 3
        /// </summary>
        public Color ChromaLink3;

        /// <summary>
        /// ChromaLink 4
        /// </summary>
        public Color ChromaLink4;

        /// <summary>
        /// ChromaLink 5
        /// </summary>
        public Color ChromaLink5;
    }
}
