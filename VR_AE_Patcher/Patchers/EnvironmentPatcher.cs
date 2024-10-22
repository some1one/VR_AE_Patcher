using System;
using System.Collections.Immutable;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher.Patchers;

public abstract record EnvironmentPatcher<TMod,TModGetter>(
    IContextualEnvironment<TMod, TModGetter> EnvLeft, 
    IContextualEnvironment<TMod, TModGetter> EnvRight, 
    TMod PatchMod,
    params Type[] ExcludedRecordTypes
) 
    : AbstractPatcher<TMod>(PatchMod)
    where TMod : class, IContextMod<TMod, TModGetter>, IMod, TModGetter
    where TModGetter : class, IContextGetterMod<TMod, TModGetter>, IModGetter
{
    protected ImmutableArray<Type> GetLeftTopLevelGroups() => EnvLeft.WarmAndGetTopLevelGroups();
    protected ImmutableArray<Type> GetRightTopLevelGroups() => EnvRight.WarmAndGetTopLevelGroups();
    protected IEnumerable<IModContext<TMod, TModGetter, IMajorRecord, IMajorRecordGetter>> GetLeftWinners(Type groupType)
        => EnvLeft.LoadOrder.PriorityOrder.WinningContextOverrides<TMod, TModGetter>(EnvLeft.LinkCache, groupType) ;
    protected IEnumerable<IModContext<TMod, TModGetter, IMajorRecord, IMajorRecordGetter>> GetRightWinners(Type groupType)
        => EnvRight.LoadOrder.PriorityOrder.WinningContextOverrides<TMod, TModGetter>(EnvRight.LinkCache, groupType);

    protected virtual IMajorRecordGetter? OnMajorRecordVisit(
        IMajorRecordGetter leftRecord,
        IModContext<TMod, TModGetter, IMajorRecord, IMajorRecordGetter> rightRecord,
        Dictionary<FormKey, IMajorRecordGetter> subResults
    ) {
        throw new NotImplementedException();
    }

    protected virtual IMajorRecordGetter? OnVisitNullOther(IMajorRecordGetter record)
    {
        return record;
    }

    protected virtual IMajorRecordGetter? VisiiMajorRecord(
        IMajorRecordGetter leftRecord,
        IReadOnlyDictionary<FormKey, IModContext<TMod, TModGetter, IMajorRecord, IMajorRecordGetter>> rightRecords
    ) {
        var rightRecord = rightRecords.GetValueOrDefault(leftRecord.FormKey);
        if (rightRecord == null)
        {
            return OnVisitNullOther(leftRecord);
        }

        Dictionary<FormKey, IMajorRecordGetter> subResults = [];
        foreach (var subRecord in leftRecord.EnumerateMajorRecords())
        {
            if (ExcludedRecordTypes.FirstOrDefault(t => subRecord.Type.IsAssignableTo(t)) != null)
                continue;
            var subResult = VisiiMajorRecord(subRecord, rightRecords);
            if (subResult != null)
                subResults.Add(subResult.FormKey, subResult.DeepCopy());
        }

        return OnMajorRecordVisit(leftRecord, rightRecord, subResults);
    }
}
