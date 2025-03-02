using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.XR.Management;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEditor.XR.Management.Metadata;

namespace VRC.SDK3.ClientSim
{
    public static class ClientSimSteamVRSetup
    {
        private static ClientSimSettings _settings;

        public static void InitializeXRSettings()
        {
            if (Application.isPlaying) return;
            
            _settings = ClientSimSettings.Instance;            
            bool _vrEnabled = _settings._enableVRMode;

            var settings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Standalone);
            settings.InitManagerOnStart = _vrEnabled;

            if (_vrEnabled)
            {
                XRPackageMetadataStore.AssignLoader(settings.Manager, "Unity.XR.OpenVR.OpenVRLoader", BuildTargetGroup.Standalone);

            }
            else
            {
                XRPackageMetadataStore.RemoveLoader(settings.Manager, "Unity.XR.OpenVR.OpenVRLoader", BuildTargetGroup.Standalone);
            }
        }
    }
}