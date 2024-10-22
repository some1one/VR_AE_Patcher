using System;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using VR_AE_Patcher.Interfaces;

namespace VR_AE_Patcher.Patchers;

public class RootPatcher
{
    private readonly ISettings settings;
    private readonly IPatcherState<ISkyrimMod, ISkyrimModGetter> state;
    private readonly List<IPatcher> patchers = [];

    public RootPatcher(ISettings settings, IPatcherState<ISkyrimMod, ISkyrimModGetter> state) {
        this.settings = settings;
        this.state = state;

        //configure synthesis pipeline state
        if (!settings.DryRun) {
            state.PatchMod.UsingLocalization = true;
            state.PatchMod.ModHeader.Flags = SkyrimModHeader.HeaderFlag.Localized | SkyrimModHeader.HeaderFlag.Master;
        }
    }

    public void LoadPatchers() {
        Console.WriteLine("Loading Patchers...");

        Attribute.GetCustomAttribute(typeof(Program).Assembly, typeof(PatcherInfoAttribute));

        var patchers = typeof(Program).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.GetInterfaces().Any(i => i == typeof(IPatchCreator)));

        var modInfo = patchers
            .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(PatcherInfoAttribute)))
            .Select(t =>
            {
                var attr = (PatcherInfoAttribute)t.GetCustomAttributes(false).First(a => a.GetType() == typeof(PatcherInfoAttribute));
                return new PatcherInfo(attr.Name, attr.DependsOnMods);
            });
    }
}
