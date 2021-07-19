// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ChromaBroadcast
{
    /// <summary>
    /// Return result
    /// </summary>
    public enum RzResult
    {
        /// <summary>
        /// Invalid
        /// </summary>
        Invalid = -1,

        /// <summary>
        /// Success
        /// </summary>
        Success = 0,

        /// <summary>
        /// Access denied
        /// </summary>
        AccessDenied = 5,

        /// <summary>
        /// Invalid handle
        /// </summary>
        InvalidHandle = 6,

        /// <summary>
        /// Invalid access
        /// </summary>
        InvalidAccess = 12,

        /// <summary>
        /// Not supported
        /// </summary>
        NotSupported = 50,

        /// <summary>
        /// Invalid parameter
        /// </summary>
        InvalidParameter = 87,

        /// <summary>
        /// The service does not exist
        /// </summary>
        ServiceNotExist = 1060,

        /// <summary>
        /// The service has not been started
        /// </summary>
        ServiceNotActive = 1062,

        /// <summary>
        /// Cannot start more than one instance of the specified program
        /// </summary>
        SingleInstanceApp = 1152,

        /// <summary>
        /// Device not connected
        /// </summary>
        DeviceNotConnected = 1167,

        /// <summary>
        /// Element not found
        /// </summary>
        NotFound = 1168,

        /// <summary>
        /// Request aborted
        /// </summary>
        RequestAborted = 1235,

        /// <summary>
        /// Requested operation is not perfromed because the user has not been authenticated
        /// </summary>
        NotAuthenticated = 1244,

        /// <summary>
        /// An attempt was made to perform an initialization operation when initialization has already been completed
        /// </summary>
        AlreadyInitialized = 1247,

        /// <summary>
        /// Resource not available or disabled
        /// </summary>
        ResourceDisabled = 4309,

        /// <summary>
        /// Device not available or supported
        /// </summary>
        DeviceNotAvailable = 4319,

        /// <summary>
        /// The group or resource is not in the correct state to perform the requested operation
        /// </summary>
        NotValidState = 5023,

        /// <summary>
        /// Insufficient access rights, administrator privilege required
        /// </summary>
        InsufficientAccessRights = 8344,

        /// <summary>
        /// No more items
        /// </summary>
        NoMoreItems = 259,

        /// <summary>
        /// General failure
        /// </summary>
        Failed = 2147483647
    }
}
