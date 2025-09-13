#if VRC_ENABLE_PROPS

namespace VRC.SDKBase.Validation
{
    // ReSharper disable once PartialTypeWithSinglePart
    public static partial class PropValidation
    {
        public static readonly string[] ComponentTypeWhiteListCommon = {
            "UnityEngine.Light",
            "UnityEngine.BoxCollider",
            "UnityEngine.SphereCollider",
            "UnityEngine.CapsuleCollider",
            "UnityEngine.MeshCollider",
            "UnityEngine.Rigidbody",
            "UnityEngine.Joint",
            "UnityEngine.AudioSource",
            #if !VRC_CLIENT
            "VRC.Core.PipelineSaver",
            #endif
            "VRC.Core.PipelineManager",
            "UnityEngine.Transform",
            "UnityEngine.Animator",
            "UnityEngine.SkinnedMeshRenderer",
            "UnityEngine.MeshFilter",
            "UnityEngine.MeshRenderer",
            "UnityEngine.ParticleSystem",
            "UnityEngine.ParticleSystemRenderer",
            "UnityEngine.TrailRenderer",
            "UnityEngine.LineRenderer",
        };

        public static readonly string[] ComponentTypeWhiteListSdk3 = {
            "VRC.SDK3.VRCTestMarker",
            "VRC.SDK3.Components.VRCPickup",
            "VRC.SDK3.Components.VRCObjectSync",
            "VRC.SDK3.Components.VRCStation",
            "VRC.SDK3.Components.VRCSpatialAudioSource",
            "VRC.SDK3.Props.Components.VRCPropDescriptor",
            "VRC.Udon.UdonBehaviour",
            "VRC.Udon.AbstractUdonBehaviourEventProxy",

            // TODO: Props and Worlds don't initialize Dynamics properly yet.
            // "VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBone",
            // "VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBoneCollider",
            // "VRC.SDK3.Dynamics.Constraint.Components.VRCAimConstraint",
            // "VRC.SDK3.Dynamics.Constraint.Components.VRCLookAtConstraint",
            // "VRC.SDK3.Dynamics.Constraint.Components.VRCParentConstraint",
            // "VRC.SDK3.Dynamics.Constraint.Components.VRCPositionConstraint",
            // "VRC.SDK3.Dynamics.Constraint.Components.VRCRotationConstraint",
            // "VRC.SDK3.Dynamics.Constraint.Components.VRCScaleConstraint",
            // "VRC.SDK3.Dynamics.Contact.Components.VRCContactSender",
            // "VRC.SDK3.Dynamics.Contact.Components.VRCContactReceiver",
        };
    }
}
#endif
