using System;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Installs.DI;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Parameters;
using Mutagen.Bethesda.Plugins.Implicit.DI;
using Mutagen.Bethesda.Plugins.Order;
using Mutagen.Bethesda.Plugins.Order.DI;
using Mutagen.Bethesda.Plugins.Records.DI;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings;
using Mutagen.Bethesda.Strings.DI;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Synthesis.States;
using Mutagen.Bethesda.Synthesis.States.DI;
using Noggog;

namespace VR_AE_Patcher.Environment;

public class EnvBuilder
{
    private class Utf8EncodingWrapper : IMutagenEncodingProvider
    {
        public IMutagenEncoding GetEncoding(GameRelease release, Language language)
        {
            return MutagenEncoding._utf8;
        }
    }
    public static ILoadOrder<IModListing<ISkyrimModGetter>> Build(string dataPath, bool useUtf8 = false)
    {
        Warmup.Init();

        var modKey = "patch name";
        var release = GameRelease.SkyrimSEGog;
        var releaseInjection = new GameReleaseInjection(release);
        var dataDir = new DataDirectoryInjection(dataPath);
        var fs = IFileSystemExt.DefaultFilesystem;

    

        var regis = release.ToCategory().ToModRegistration();
        TranslatedString.DefaultLanguage = Language.English;

        // var modListings = Implicits.Get(release).Listings
        //     .Select(x => new ModListing(x, enabled: true, existsOnDisk: true));

        var implicitProvider = new ImplicitListingsProvider(
                            IFileSystemExt.DefaultFilesystem,
                            dataDir,
                            new ImplicitListingModKeyProvider(
                                releaseInjection));

        var orderListings = new OrderListings();

        var categoryContext = new GameCategoryContext(releaseInjection);
        var gameLoc = new GameLocatorLookupCache();
        var ccPathProvider = new CreationClubListingsProvider(
            fs,
            dataDir,
            new CreationClubListingsPathProvider(
                categoryContext,
                new CreationClubEnabledProvider(categoryContext),
                new GameDirectoryProvider(
                    releaseInjection,
                    gameLoc)),
            new CreationClubRawListingsReader());

        PluginListings.TryGetListingsFile(GameRelease.SkyrimSE, out var loadOrderPath);
        var userMods = new StatePluginsListingProvider(
                    loadOrderPath,
                    new PluginRawListingsReader(
                        fs,
                        new PluginListingsParser(
                            new PluginListingCommentTrimmer(),
                            new LoadOrderListingParser(
                                new HasEnabledMarkersProvider(
                                    releaseInjection))))); //.Get().Select(x => x.ToModListing(true));

       var stateLoadOrder = new GetStateLoadOrder(
            implicitProvider,
            orderListings,
            ccPathProvider,
            userMods,
            new EnableImplicitMastersFactory(fs));

        
        var loadOrderListings = stateLoadOrder.GetFinalLoadOrder(GameRelease.SkyrimSE, null, dataPath, false, new PatcherPreferences()
        {
            ExclusionMods = [Mods.ModKeys.AE._ResourcePack],
            InclusionMods = [Mods.ModKeys.Common.Skyrim, Mods.ModKeys.Common.Update, Mods.ModKeys.Common.Dawnguard, Mods.ModKeys.Common.HearthFires, Mods.ModKeys.Common.Dragonborn]
        });

        var stringReadParams = new StringsReadParameters()
        {
            TargetLanguage = Language.English,
            EncodingProvider = useUtf8 ? new Utf8EncodingWrapper() : null
        };


        var loadOrder = new LoadOrderImporter<ISkyrimModGetter>(
            fs,
            dataDir,
            new LoadOrderListingsInjection(loadOrderListings.ProcessedLoadOrder),
            new ModImporter<ISkyrimModGetter>(fs, new GameReleaseInjection(GameRelease.SkyrimSE)),
            new MasterFlagsLookupProvider(new GameReleaseInjection(GameRelease.SkyrimSE), fs, dataDir)
        ).Import();

        return loadOrder;
    }
        
}
