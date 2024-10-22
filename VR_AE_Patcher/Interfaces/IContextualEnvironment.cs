using System;
using System.Collections.Immutable;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace VR_AE_Patcher.Interfaces;

public interface IContextualEnvironment<TModSetter, TModGetter> : IGameEnvironment<TModSetter, TModGetter>, IDisposable
    where TModSetter : class, IContextMod<TModSetter, TModGetter>, TModGetter
    where TModGetter : class, IContextGetterMod<TModSetter, TModGetter>
{
    bool Warm { get; }
    IDictionary<string, object?> Context { get; }
    IGameEnvironment<TModSetter, TModGetter> Environment { get; }
    ImmutableArray<Type> WarmAndGetTopLevelGroups();
}
