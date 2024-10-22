using System;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using VR_AE_Patcher.Mods;

namespace VR_AE_Patcher;

public static class EnvironmentHelper
{
    public static IGameEnvironment<ISkyrimMod, ISkyrimModGetter> BuildVREnvironment(this GameEnvironment env, string dataPath)
    {
        return env
            .Builder<ISkyrimMod, ISkyrimModGetter>(GameRelease.SkyrimSEGog)
            .WithTargetDataFolder(dataPath)
            .WithLoadOrder(LoadOrders.VR)
            .Build();
    }

    public static IGameEnvironment<ISkyrimMod, ISkyrimModGetter> BuildSEEnvironment(this GameEnvironment env, string dataPath)
    {
        return env
            .Builder<ISkyrimMod, ISkyrimModGetter>(GameRelease.SkyrimSEGog)
            .WithTargetDataFolder(dataPath)
            .WithLoadOrder(LoadOrders.Common)
            .Build();
    }

    public static IGameEnvironment<ISkyrimMod, ISkyrimModGetter> BuildAEEnvironment(this GameEnvironment env, string dataPath) {
        return env
            .Builder<ISkyrimMod, ISkyrimModGetter>(GameRelease.SkyrimSEGog)
            .WithTargetDataFolder(dataPath)
            .WithLoadOrder(LoadOrders.AE)
            .Build();
    }
}
