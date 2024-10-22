using System;
using System.Collections.Immutable;
using Mutagen.Bethesda.Plugins.Records;
using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher.Patchers;

public abstract record MajorRecordPatcher<TMod, TMajorRecordGetter>(TMod PatchMod)
    : MajorRecordPatcher<TMod>(typeof(TMajorRecordGetter), PatchMod), IPatcher<TMod>
    where TMod : IMod
    where TMajorRecordGetter : IMajorRecordGetter;

public abstract record MajorRecordPatcher<TMod>(Type RecordType, TMod PatchMod) : AbstractPatcher<TMod>(PatchMod), IPatcher<TMod>
    where TMod : IMod
{
    //todo validate is valid type and convert record type to getter type
    protected readonly Type getterType = RecordType;
}


