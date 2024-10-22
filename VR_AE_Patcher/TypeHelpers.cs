using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Loqui;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Mapping;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace VR_AE_Patcher;

public static class TypeHelpers
{   
    public static ImmutableArray<Type> GetMajorRecordTypes<TMod, TModGetter>(this IGameEnvironment<TMod, TModGetter> env)
        where TMod : class, IMod, TModGetter, IContextMod<TMod, TModGetter>
        where TModGetter : class, IModGetter, IContextGetterMod<TMod, TModGetter>
        => MajorRecordTypeEnumerator.GetMajorRecordTypesFor(env.GameRelease.ToCategory()).Select(t => t.GetterType).ToImmutableArray();

    private static readonly Dictionary<Type, ImmutableArray<Type>> topLevelGroups = [];

    public static ImmutableArray<Type> GetTopLevelGroups<TModGetterInterface>()
        where TModGetterInterface : IModGetter
    {
        if (topLevelGroups.GetValueOrDefault(typeof(TModGetterInterface)) == null)
            topLevelGroups[typeof(TModGetterInterface)] = typeof(TModGetterInterface)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(p =>
                    p.PropertyType.IsGenericType
                    && p.PropertyType.IsAssignableTo(typeof(IGroupGetter)))
                .Select(p => p.PropertyType.GetGenericArguments()[0])
                .ToImmutableArray();

        return topLevelGroups.GetValueOrDefault(typeof(TModGetterInterface));
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
        => source.Where(x => x != null).Cast<T>();
}
