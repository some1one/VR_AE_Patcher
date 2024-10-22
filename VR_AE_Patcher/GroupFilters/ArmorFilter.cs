// using System;
// using Mutagen.Bethesda.Plugins;
// using Mutagen.Bethesda.Skyrim;

// namespace VR_AE_Patcher.GroupFilters
// {
//     class ArmorFilter : IFilter
//     {
//         private readonly ModCache modCache;
//         public Type GetterType => typeof(IArmorGetter);

//         public ArmorFilter(ModCache modCache)
//         {
//             this.modCache = modCache;
//         }

//         public bool Filter(FormKey key)
//         {
//             return false;
//             // return modCache.CCbgssse011_hrsarmrelvn.Armors.ContainsKey(key)
//             //     || modCache.CCbgssse012_hrsarmrstl.Armors.ContainsKey(key);
//         }
//     }
// }