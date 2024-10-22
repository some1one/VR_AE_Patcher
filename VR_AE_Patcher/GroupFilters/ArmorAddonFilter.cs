// using System;
// using Mutagen.Bethesda.Plugins;
// using Mutagen.Bethesda.Skyrim;

// namespace VR_AE_Patcher.GroupFilters
// {
//     class ArmorAddonFilter : IFilter
//     {
//         private readonly ModCache modCache;
//         public Type GetterType => typeof(IArmorAddonGetter);

//         public ArmorAddonFilter(ModCache modCache)
//         {
//             this.modCache = modCache;
//         }

//         public bool Filter(FormKey key)
//         {
//             return false;
//             // return modCache.CCbgssse011_hrsarmrelvn.ArmorAddons.ContainsKey(key)
//             //     || modCache.CCbgssse012_hrsarmrstl.ArmorAddons.ContainsKey(key);
//         }
//     }
// }