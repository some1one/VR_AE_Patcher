using System;
using System.Collections.Immutable;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace VR_AE_Patcher.Patchers;

public sealed record class SkyrimEnvDiffPatcher(
    Interfaces.IContextualEnvironment<ISkyrimMod, ISkyrimModGetter> EnvLeft,
    Interfaces.IContextualEnvironment<ISkyrimMod, ISkyrimModGetter> EnvRight,
    ISkyrimMod PatchMod,
    params Type[] ExcludedRecordTypes
)
    : EnvDiffPatcher<ISkyrimMod, ISkyrimModGetter>(EnvLeft, EnvRight, PatchMod, ExcludedRecordTypes)
{
    public override ISkyrimMod Patch()
    {
        base.Patch();
        foreach (var diff in diffs)
        {
            var leftContext = diff.LeftRecord.ToLinkGetter<ISkyrimMajorRecordGetter>()
                .ResolveContext<ISkyrimMod, ISkyrimModGetter, ISkyrimMajorRecord, ISkyrimMajorRecordGetter>(EnvLeft.LinkCache)!;
            if (diff.RightRecord == null)
            {
                leftContext.GetOrAddAsOverride(PatchMod);
                continue;
            }

            //todo: modify record for merged stuff (like if name is empty in left, use right)
            // var rightContext = diff.RightRecord.ToLinkGetter<ISkyrimMajorRecordGetter>()
            //     .ResolveContext<ISkyrimMod, ISkyrimModGetter, ISkyrimMajorRecord, ISkyrimMajorRecordGetter>(EnvRight.LinkCache)!;
            //rightContext.GetOrAddAsOverride(PatchMod);
            leftContext.GetOrAddAsOverride(PatchMod);
        }

        return PatchMod;
    }
}
