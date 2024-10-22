// using System;
// using Mutagen.Bethesda.Plugins;
// using Mutagen.Bethesda.Skyrim;

// namespace VR_AE_Patcher.GroupFilters
// {
//     class PackageFilter : IFilter
//     {
//         private readonly ModCache modCache;
//         public Type GetterType => typeof(IPackageGetter);

//         public PackageFilter(ModCache modCache)
//         {
//             this.modCache = modCache;
//         }

//         public bool Filter(FormKey key)
//         {
//             return false;
//             // return modCache.CCeejsse005_cave.Packages.ContainsKey(key)
//             //     || modCache.CCrmssse001_necrohouse.Packages.ContainsKey(key)
//             //     || modCache.CCeejsse001_hstead.Packages.ContainsKey(key)
//             //     || modCache.CCeejsse002_tower.Packages.ContainsKey(key)
//             //     || modCache.CCeejsse003_hollow.Packages.ContainsKey(key)
//             //     || modCache.CCafdsse001_dwesanctuary.Packages.ContainsKey(key)
//             //     || modCache.CCeejsse004_hall.Packages.ContainsKey(key)
//             //     || modCache.CCbgssse031_advcyrus.Packages.ContainsKey(key)
//             //     || modCache.CCvsvsse004_beafarmer.Packages.ContainsKey(key) ;
//         }
//     }
// }