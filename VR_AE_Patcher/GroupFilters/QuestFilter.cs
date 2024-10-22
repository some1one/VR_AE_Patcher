// using System;
// using Mutagen.Bethesda.Plugins;
// using Mutagen.Bethesda.Skyrim;

// namespace VR_AE_Patcher.GroupFilters
// {
//     class QuestFilter : IFilter
//     {
//         private readonly ModCache modCache;
//         public Type GetterType => typeof(IQuestGetter);

//         public QuestFilter(ModCache modCache)
//         {
//             this.modCache = modCache;
//         }

//         public bool Filter(FormKey key)
//         {
//             return false;
//             // return modCache.CCeejsse005_cave.Quests.ContainsKey(key)
//             //     || modCache.CCrmssse001_necrohouse.Quests.ContainsKey(key);
//         }
//     }
// }