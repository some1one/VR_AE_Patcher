using System;
using Mutagen.Bethesda.Plugins.Records;
using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher.Patchers;

public abstract record class AbstractPatcher<TMod>(TMod PatchMod) : IPatcher<TMod>
    where TMod : IMod
{
    public abstract TMod Patch();
}