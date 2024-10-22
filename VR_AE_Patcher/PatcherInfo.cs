using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

class PatcherInfo {
    public string Name { get; }
    public IReadOnlySet<string> DependentMods { get; }
    internal Action? OnComplete { get; set; }
    public PatcherInfo(string name, IEnumerable<string> dependentMods) {
        Name = name;
        DependentMods = dependentMods.ToHashSet();
    }
}