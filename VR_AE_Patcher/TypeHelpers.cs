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

    public static bool ImplementsOrDerives(this Type @this, Type from)
    {
        if (!from.IsGenericType || !from.IsGenericTypeDefinition)
        {
            return from.IsAssignableFrom(@this);
        }
        
        if (from.IsInterface)
        {
            foreach (Type @interface in @this.GetInterfaces())
            {
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == from
                || from == @interface && @interface.IsAssignableFrom(from))
                {
                    return true;
                }
            }
        }

        if (@this.IsGenericType && @this.GetGenericTypeDefinition() == from
        || @this.IsAssignableFrom(from))
        {
            return true;
        }

        if (@this.BaseType == typeof(object) || @this.BaseType == typeof(ValueType))
        {
            //everthing is assignable to object or ValueType
            //stop here since there are no more base types to check
            return false;
        }

        return @this.BaseType?.ImplementsOrDerives(from) ?? false;
    }

    public static TValue? TryFindValueDynamic<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key, out bool found)
       where TKey : notnull
    {
        found = dict.TryGetValue(key, out TValue? value);
        return value;
    }
}
