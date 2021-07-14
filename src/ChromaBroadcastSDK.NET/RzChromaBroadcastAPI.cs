// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Security;
using System.Runtime.InteropServices;

namespace ChromaBroadcast
{
    /// <summary>
    /// The Razer Chroma Broadcast SDK
    /// </summary>
    public static class RzChromaBroadcastAPI
    {
        /// <summary>
        /// If the architecture of the current process is 32 bit
        /// </summary>
        private static bool Is32Bit => IntPtr.Size == 4;

        /// <summary>
        /// Initialize Chroma Broadcast API (32 bit)
        /// </summary>
        /// <param name="appId">Application Id for authentication</param>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI.dll", EntryPoint = "Init", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult Init32(in Guid appId);

        /// <summary>
        /// Initialize Chroma Broadcast API (64 bit)
        /// </summary>
        /// <param name="appId">Application Id for authentication</param>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI64.dll", EntryPoint = "Init", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult Init64(in Guid appId);

        /// <summary>
        /// UnInitialize Chroma Broadcast API (32 bit)
        /// </summary>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI.dll", EntryPoint = "UnInit", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult UnInit32();

        /// <summary>
        /// UnInitialize Chroma Broadcast API (64 bit)
        /// </summary>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI64.dll", EntryPoint = "UnInit", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult UnInit64();

        /// <summary>
        /// Chroma Broadcast Callback function
        /// </summary>
        /// <param name="type">The callback function's type</param>
        /// <param name="pData">the callback function's data</param>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
        private delegate RzResult RegisterEventNotificationCallback(RzChromaBroadcastType type, IntPtr pData);

        /// <summary>
        /// Chroma Broadcast Callback function
        /// </summary>
        private static RegisterEventNotificationCallback Callback { get; set; }

        /// <summary>
        /// Register Chroma Broadcast API event notification (32 bit)
        /// </summary>
        /// <param name="lpFunc">Callback function</param>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI.dll", EntryPoint = "RegisterEventNotification", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult RegisterEventNotification32(RegisterEventNotificationCallback lpFunc);

        /// <summary>
        /// Register Chroma Broadcast API event notification (64 bit)
        /// </summary>
        /// <param name="lpFunc">Callback function</param>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI64.dll", EntryPoint = "RegisterEventNotification", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult RegisterEventNotification64(RegisterEventNotificationCallback lpFunc);

        /// <summary>
        /// UnRegister Chroma Broadcast API event notification (32 bit)
        /// </summary>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI.dll", EntryPoint = "UnRegisterEventNotification", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult UnRegisterEventNotificationNative32();

        /// <summary>
        /// UnRegister Chroma Broadcast API event notification (64 bit)
        /// </summary>
        /// <returns>Razer result</returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("RzChromaBroadcastAPI64.dll", EntryPoint = "UnRegisterEventNotification", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern RzResult UnRegisterEventNotificationNative64();

        /// <summary>
        /// Initialize Chroma Broadcast API
        /// </summary>
        /// <param name="appId">Application Id for authentication</param>
        /// <returns>Razer result</returns>
        public static RzResult Init(Guid appId)
        {
            try
            {
                return Is32Bit ? Init32(appId) : Init64(appId);
            }
            catch (SEHException)
            {
                return RzResult.Failed;
            }
        }

        /// <summary>
        /// UnInitialize Chroma Broadcast API
        /// </summary>
        /// <returns>Razer result</returns>
        public static RzResult UnInit()
        {
            return Is32Bit ? UnInit32() : UnInit64();
        }

        /// <summary>
        /// Register Chroma Broadcast API event notification
        /// </summary>
        /// <param name="lpFunc">Callback function</param>
        /// <returns>Razer result</returns>
        public static RzResult RegisterEventNotification(Func<RzChromaBroadcastType, RzChromaBroadcastStatus?, RzChromaBroadcastEffect?, RzResult> lpFunc)
        {
            if (Callback == null)
            {
                Callback = new RegisterEventNotificationCallback((type, pData) =>
                {
                    if (pData != IntPtr.Zero)
                    {
                        if (type == RzChromaBroadcastType.BroadcastEffect)
                        {
                            RzChromaBroadcastEffect broadcastEffect = new RzChromaBroadcastEffect();
                            int[] broadcastEffectData = new int[5];

                            Marshal.Copy(pData, broadcastEffectData, 0, 5);

                            broadcastEffect.ChromaLink1 = Color.FromArgb((broadcastEffectData[0] >> 0) & 0xff, (broadcastEffectData[0] >> 8) & 0xff, (broadcastEffectData[0] >> 16) & 0xff);
                            broadcastEffect.ChromaLink2 = Color.FromArgb((broadcastEffectData[1] >> 0) & 0xff, (broadcastEffectData[1] >> 8) & 0xff, (broadcastEffectData[1] >> 16) & 0xff);
                            broadcastEffect.ChromaLink3 = Color.FromArgb((broadcastEffectData[2] >> 0) & 0xff, (broadcastEffectData[2] >> 8) & 0xff, (broadcastEffectData[2] >> 16) & 0xff);
                            broadcastEffect.ChromaLink4 = Color.FromArgb((broadcastEffectData[3] >> 0) & 0xff, (broadcastEffectData[3] >> 8) & 0xff, (broadcastEffectData[3] >> 16) & 0xff);
                            broadcastEffect.ChromaLink5 = Color.FromArgb((broadcastEffectData[4] >> 0) & 0xff, (broadcastEffectData[4] >> 8) & 0xff, (broadcastEffectData[4] >> 16) & 0xff);
                            return lpFunc(type, null, broadcastEffect);
                        }
                        else if (type == RzChromaBroadcastType.BroadcastStatus)
                        {
                            return lpFunc(type, (RzChromaBroadcastStatus)pData.ToInt32(), null);
                        }
                        else
                        {
                            return RzResult.NotSupported;
                        }
                    }
                    else
                    {
                        return RzResult.NotValidState;
                    }
                });
            }

            return Is32Bit ? RegisterEventNotification32(Callback) : RegisterEventNotification64(Callback);
        }

        /// <summary>
        /// UnRegister Chroma Broadcast API event notification
        /// </summary>
        /// <returns>Razer result</returns>
        public static RzResult UnRegisterEventNotification()
        {
            return Is32Bit ? UnRegisterEventNotificationNative32() : UnRegisterEventNotificationNative64();
        }
    }
}
