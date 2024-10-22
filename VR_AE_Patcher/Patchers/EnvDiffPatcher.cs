using System;
using System.Collections.Immutable;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using static VR_AE_Patcher.Masks;

namespace VR_AE_Patcher.Patchers;

public record EnvDiffPatcher<TMod, TModGetter>(
    Interfaces.IContextualEnvironment<TMod, TModGetter> EnvLeft, 
    Interfaces.IContextualEnvironment<TMod, TModGetter> EnvRight, 
    TMod PatchMod,
    params Type[] ExcludedRecordTypes
)
    : EnvironmentPatcher<TMod, TModGetter>(EnvLeft, EnvRight, PatchMod, ExcludedRecordTypes)
    where TMod : class, IContextMod<TMod, TModGetter>, IMod, TModGetter
    where TModGetter : class, IContextGetterMod<TMod, TModGetter>, IModGetter
{
    protected readonly record struct Diff(IMajorRecordGetter LeftRecord, IMajorRecordGetter? RightRecord);
    protected readonly List<Diff> diffs = [];
    public override TMod Patch()
    {
        BuildDiffs();
        //Unfortuantely, we can't use DeepCopyIn here because it's not an extension for TModGetter, only for ISkyrimModGetter
        // foreach (var diff in diffs)
        // {
        //     if (diff.RightRecord == null)
        //     {
        //         PatchMod.DeepCopyIn((TModGetter)diff.LeftRecord);
        //         continue;
        //     }

        //     PatchMod.DeepCopyIn((TModGetter)diff.RightRecord);
        //     PatchMod.DeepCopyIn((TModGetter)diff.LeftRecord);
        // }

        return PatchMod;
    }

    private void BuildDiffs() {
        diffs.Clear();
        var topLevelGroups = GetLeftTopLevelGroups();
        foreach (var group in topLevelGroups)
        {
            if (ExcludedRecordTypes.Contains(group)) continue;
            var leftRecords = GetLeftWinners(group);
            var rightRecords = GetRightWinners(group).ToDictionary(r => r.Record.FormKey);

            foreach (var leftRecord in leftRecords) {
                VisiiMajorRecord(leftRecord.Record, rightRecords);
            }
        }
    }

    protected override IMajorRecordGetter? OnMajorRecordVisit(
        IMajorRecordGetter leftRecord,
        IModContext<TMod, TModGetter, IMajorRecord, IMajorRecordGetter> rightRecord,
        Dictionary<FormKey, IMajorRecordGetter> subRecords
    ) {
        var record = RebuildRecord(rightRecord, subRecords);

        //TODO: exclusions
        //compare fromRecord with usedToForm
        if (!RecordsEquals(leftRecord, record))
        {
            diffs.Add(new Diff(leftRecord, record));
            subRecords.Add(leftRecord.FormKey, leftRecord.DeepCopy());
            return leftRecord;
        }

        return null;
    }

    protected override IMajorRecordGetter? OnVisitNullOther(IMajorRecordGetter record)
    {
        diffs.Add(new Diff(record, null));
        return base.OnVisitNullOther(record);
    }

    protected static bool RecordsEquals(IMajorRecordGetter left, IMajorRecordGetter right) {
        var comparer = new EqualityChecker(left, right, true);
        return comparer.AreEqual();
    }

    protected static IMajorRecordGetter RebuildRecord(
        IModContext<TMod, TModGetter, IMajorRecord, IMajorRecordGetter> record, 
        IReadOnlyDictionary<FormKey, IMajorRecordGetter> subRecords
    ) {
        if (subRecords.Count == 0) return record.Record;
        var tempRecord = record.Record.DeepCopy();
        foreach (var tempSubRecord in tempRecord.AsEnumerable())
        {
            var subRecord = subRecords.GetValueOrDefault(tempSubRecord.FormKey)?.DeepCopy();
            if (subRecord == null) continue;
            tempSubRecord.DeepCopyIn(subRecord);
        }
        return tempRecord;
    }
}
