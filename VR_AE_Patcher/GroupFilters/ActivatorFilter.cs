// using System;
// using Mutagen.Bethesda.Plugins;
// using Mutagen.Bethesda.Skyrim;

// namespace VR_AE_Patcher.GroupFilters
// {
//     class ActivatorFilter : IFilter
//     {
//         private readonly ModCache modCache;
//         public Type GetterType => typeof(IActivatorGetter);

//         public ActivatorFilter(ModCache modCache)
//         {
//             this.modCache = modCache;
//         }

//         public bool Filter(FormKey key) {
//             return false;
//             // return modCache.CCbgssse067_daedinv.Activators.ContainsKey(key)
//             // || modCache.CCBGSSSE025_AdvDSGS.Activators.ContainsKey(key);
//         }
//     }
// }