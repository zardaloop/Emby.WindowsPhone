﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaBrowser.ApiInteraction;
using MediaBrowser.Model.ApiClient;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Session;
using MediaBrowser.WindowsPhone.Model.Security;

namespace MediaBrowser.WindowsPhone.Model
{
    public class ExtendedApiClient : ApiClient
    {
        public ExtendedApiClient(ILogger logger, string serverHostName, string clientName, IDevice device, string appVersion, ClientCapabilities capabilities)
            : base(logger, serverHostName, clientName, device, appVersion, new CryptographyProvider())
        {
        }

        /// <summary>
        /// Registers the device for push notifications.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="sendTileUpdate">The send tile update.</param>
        /// <param name="sendToastUpdate">The send toast update.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">deviceType
        /// or
        /// deviceId</exception>
        public Task RegisterDeviceAsync(string deviceId, string uri, bool? sendTileUpdate = null, bool? sendToastUpdate = null)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("deviceId");
            }

            var dict = new QueryStringDictionary
                           {
                               {"deviceid", deviceId},
                               {"url", uri},
                               {"devicetype", "WindowsPhone8"}
                           };

            if (sendTileUpdate.HasValue)
                dict.Add("sendlivetile", sendTileUpdate.Value);
            if (sendToastUpdate.HasValue)
                dict.Add("sendtoast", sendToastUpdate.Value);

            var url = GetApiUrl("PushNotification/Devices", dict);

            return PostAsync<EmptyRequestResult>(url, new Dictionary<string, string>());
        }

        /// <summary>
        /// Deletes the device async.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">deviceId</exception>
        public Task DeleteDeviceAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("deviceId");
            }

            var url = GetApiUrl("PushNotification/Devices/" + deviceId);

            //return HttpClient.DeleteAsync(url, CancellationToken.None);
            return null;
        }

        /// <summary>
        /// Updates the device async.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <param name="sendTileUpdate">The send tile update.</param>
        /// <param name="sendToastUpdate">The send toast update.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">deviceId</exception>
        public Task UpdateDeviceAsync(string deviceId, bool? sendTileUpdate = null, bool? sendToastUpdate = null)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("deviceId");
            }

            var dict = new QueryStringDictionary();

            if (sendTileUpdate.HasValue)
                dict.Add("sendlivetile", sendTileUpdate.Value);
            if (sendToastUpdate.HasValue)
                dict.Add("sendtoast", sendToastUpdate.Value);

            var url = GetApiUrl("PushNotification/Devices/" + deviceId, dict);

            return PostAsync<EmptyRequestResult>(url, new Dictionary<string, string>());
        }

        /// <summary>
        /// Pushes the heartbeat async.
        /// </summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">deviceId</exception>
        public Task PushHeartbeatAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("deviceId");
            }

            var url = GetApiUrl("PushNotification/Devices/" + deviceId + "/Heartbeats");

            return PostAsync<EmptyRequestResult>(url, new Dictionary<string, string>());
        }
    }
}
