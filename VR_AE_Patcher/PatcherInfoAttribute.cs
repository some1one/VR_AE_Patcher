using System;
namespace VR_AE_Patcher
{
    public class PatcherInfoAttribute : Attribute
    {
        public string[] DependsOnMods { get; }
        public string Name { get; }
        public PatcherInfoAttribute(string name, string[] dependsOnMods)
        {
            Name = name;
            DependsOnMods = dependsOnMods;
        }
    }
}