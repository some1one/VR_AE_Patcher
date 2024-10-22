using System;

namespace VR_AE_Patcher.Interfaces;

public interface ISettings
{
    bool DryRun { get; }
    bool Verbose { get; }
    string PatchName { get; }
    string? SkyrimAEPath { get; }
    string? SkyrimVRPath { get; }
}
