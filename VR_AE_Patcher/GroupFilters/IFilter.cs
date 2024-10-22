using System;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace VR_AE_Patcher.GroupFilters
{
    public interface IFilter
    {
        bool Filter(FormKey key);
        Type GetterType {get;}
    }
}