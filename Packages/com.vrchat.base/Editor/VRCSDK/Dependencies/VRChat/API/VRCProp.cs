#if VRC_ENABLE_PROPS
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VRC.Core;

namespace VRC.SDKBase.Editor.Api
{
    public struct VRCProp: IVRCContent
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        
        public string ImageUrl { get; set; }
        public string ThumbnailImageUrl { get; set; }
        
        [JsonProperty("_created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("_updated_at")]
        public DateTime UpdatedAt { get; set; }
        public List<VRCUnityPackage> UnityPackages { get; set; }

        public int SpawnType { get; set; }
        public int WorldPlacementMask { get; set; }
        
        public string GetLatestAssetUrlForPlatform(string platform)
        {
            string assetUrl = null;
            var preferredUnityVersion = new UnityVersion();
            if (this.UnityPackages == null) return null;
            foreach (var unityPackage in this.UnityPackages)
            {
                if (UnityVersion.Parse(unityPackage.UnityVersion).CompareTo(preferredUnityVersion) < 0) continue;
                if (unityPackage.Platform != platform) continue;
                assetUrl = unityPackage.AssetUrl;
                preferredUnityVersion = UnityVersion.Parse(unityPackage.UnityVersion);
            }

            return assetUrl;
        }
    }
    
    // Only a subset of fields is allowed to be changed through the SDK
    public struct VRCPropChanges {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public int SpawnType { get; set; }
        public int WorldPlacementMask { get; set; }
    }
}
#endif