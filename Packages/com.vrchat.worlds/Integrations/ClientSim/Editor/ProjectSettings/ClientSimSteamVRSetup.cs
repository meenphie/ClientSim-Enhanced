using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.XR.Management;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEditor.XR.Management.Metadata;

[InitializeOnLoad]
[DefaultExecutionOrder(1000)]
public static class ClientSimSteamVRSetup
{
    static ClientSimSteamVRSetup()
    {
        InitializeXRSettings();
    }

    private static void InitializeXRSettings()
    {
        var settings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Standalone);

        settings.InitManagerOnStart = true;
        
        XRPackageMetadataStore.AssignLoader(settings.Manager, "Unity.XR.OpenVR.OpenVRLoader", BuildTargetGroup.Standalone);
    }
}
