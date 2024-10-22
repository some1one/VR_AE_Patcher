using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher
{
    public record class Settings : ISettings
    {
        public bool DryRun { get; init; } = true;
        public bool Verbose { get; init; } = true;
        public string PatchName { get; init; } = "VR_AE";
        public string? SkyrimAEPath { get; init; } = @"D:\SteamLibrary\steamapps\common\Skyrim Special Edition\Data";
        public string? SkyrimVRPath { get; init; } = @"D:\SteamLibrary\steamapps\common\SkyrimVR\Data";
    }
}