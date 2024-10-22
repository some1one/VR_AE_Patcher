using System;
using Mutagen.Bethesda.Skyrim;

namespace VR_AE_Patcher.Interfaces;

public interface IModCache
{
    ISkyrimModDisposableGetter? GetAEMod(string name);
    ISkyrimModDisposableGetter? GetVRMod(string name);
}
