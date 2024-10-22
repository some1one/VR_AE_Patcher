using System;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using GameFinder.Common;
using Loqui;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Binary.Translations;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Order;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher.Environment;

public static class ContextualEnvironmentHelpers {
    public static ContextualEnvironment<TModSetter, TModGetter> ToContextualEnvironment<TModSetter, TModGetter>(this IGameEnvironment<TModSetter, TModGetter> env)
        where TModSetter : class, IContextMod<TModSetter, TModGetter>, TModGetter
        where TModGetter : class, IContextGetterMod<TModSetter, TModGetter>
    {
        return new ContextualEnvironment<TModSetter, TModGetter> { Environment = env };
    }
}

public class ContextualEnvironment<TModSetter,TModGetter>() : IContextualEnvironment<TModSetter, TModGetter>, IGameEnvironment<TModSetter, TModGetter>, IDisposable
    where TModSetter : class, IContextMod<TModSetter, TModGetter>, TModGetter
    where TModGetter : class, IContextGetterMod<TModSetter, TModGetter>
{
    private ImmutableArray<Type>? topLevelGroups = null;
    public bool Warm { get; protected set; } = false;
    protected readonly Dictionary<string, object?> context = [];
    [Pure]
    public IDictionary<string, object?> Context => context;
    public required IGameEnvironment<TModSetter, TModGetter> Environment { get; init; }

    public ILoadOrderGetter<IModListingGetter<TModGetter>> LoadOrder => Environment.LoadOrder;

    public ILinkCache<TModSetter, TModGetter> LinkCache => Environment.LinkCache;

    public DirectoryPath DataFolderPath => Environment.DataFolderPath;

    public GameRelease GameRelease => Environment.GameRelease;

    public FilePath LoadOrderFilePath => Environment.LoadOrderFilePath;

    public FilePath? CreationClubListingsFilePath => Environment.CreationClubListingsFilePath;

    ILoadOrderGetter<IModListingGetter<IModGetter>> IGameEnvironment.LoadOrder => ((IGameEnvironment)Environment).LoadOrder;

    ILinkCache IGameEnvironment.LinkCache => ((IGameEnvironment)Environment).LinkCache;

    public ImmutableArray<Type> WarmAndGetTopLevelGroups() {
        if (topLevelGroups.HasValue) return topLevelGroups.Value;
        topLevelGroups = Environment.GetMajorRecordTypes();

        topLevelGroups = TypeHelpers.GetTopLevelGroups<ISkyrimModGetter>();
        return topLevelGroups.Value;
    }

    public void Dispose()
    {
        Environment.Dispose();
        GC.SuppressFinalize(this);
    }
}
