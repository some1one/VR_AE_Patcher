using System;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Synthesis;

namespace VR_AE_Patcher.Interfaces;

public interface IPatcher<TModGetter> : Mutagen.Bethesda.Synthesis.IPatcher
    where TModGetter : IModGetter
{
    TModGetter Patch();
}


