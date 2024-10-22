using System.Collections.Generic;
using System.Linq;

namespace VR_AE_Patcher
{
    static class PatcherInfoExtensions
    {
        public static Dictionary<string, List<string>> GetDisposableInfo(this IEnumerable<PatcherInfo> info) {
            Dictionary<string, List<string>> disposableModsOnPatchComplete = new Dictionary<string, List<string>>();

            foreach(var patcher in info) {
                foreach(var mod in patcher.DependentMods) {
                    var modUsedLater = info.Any(p =>
                        p.Name != patcher.Name
                        && p.DependentMods.Contains(mod));
                    
                    if(!modUsedLater) {
                        if(!disposableModsOnPatchComplete.ContainsKey(patcher.Name))
                            disposableModsOnPatchComplete.Add(patcher.Name, new List<string>() {mod});
                        else
                            disposableModsOnPatchComplete[patcher.Name].Add(mod);
                    }
                }
            }

            return disposableModsOnPatchComplete;
        }
    }
}