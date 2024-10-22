using System.Diagnostics;
using GameFinder.Common;
using Loqui;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Mapping;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings;
using Noggog;
using Activator = Mutagen.Bethesda.Skyrim.Activator;

namespace VR_AE_Patcher;

public static class Masks
{

    public static SkyrimMajorRecord.TranslationMask CreateTranslationMask(this IMajorRecordGetter record, bool defaultOn = true, bool onOverall = true) {
        GetterTypeMapping.Instance.TryGetGetterType(record.GetType(), out var getterType);
        var recordTypes = MajorRecordTypeEnumerator.GetMajorRecordTypesFor(Mutagen.Bethesda.GameCategory.Skyrim)
            .First(t => t.GetterType == getterType);
        var maskType = recordTypes.ClassType.GetNestedType("TranslationMask");
        if (maskType != null) {
            if (System.Activator.CreateInstance(maskType, [defaultOn, onOverall]) is SkyrimMajorRecord.TranslationMask mask)
            {
                mask.Version2 = false;
                mask.VersionControl = false;
                mask.FormVersion = false;

                if (mask is Activator.TranslationMask activatorMask)
                {
                    activatorMask.VirtualMachineAdapter = new VirtualMachineAdapter.TranslationMask(true)
                    {
                        Scripts = new ScriptEntry.TranslationMask(true)
                        {
                            Properties = false
                        }
                    };
                }

                if (mask is Ammunition.TranslationMask ammunitionMask)
                {
                    ammunitionMask.DATADataTypeState = false; // doesn't exist in VR
                }

                if (mask is Quest.TranslationMask questMask)
                {
                    questMask.VirtualMachineAdapter = new QuestAdapter.TranslationMask(true)
                    {
                        Scripts = new ScriptEntry.TranslationMask(true)
                        {
                            Properties = false
                        }
                    };
                }

                if (mask is Worldspace.TranslationMask worldspaceMask)
                {
                    worldspaceMask.MaxHeight = false;
                    worldspaceMask.Water = false;
                }

                return mask;
            }
        }

        return new SkyrimMajorRecord.TranslationMask(defaultOn, onOverall)
        {
            Version2 = false,
            VersionControl = false,
            FormVersion = false,
        };
    }

    public class EqualityChecker(dynamic left, dynamic right, bool localized = false)
    {
        public bool AreEqual()
        {
            var type = (Type)left.GetType();
            if (type != right.GetType()) return false;

            SkyrimMajorRecord.TranslationMask mask = CreateTranslationMask(left);
            var maskType = mask.GetType();

            if (!localized)
                return left.Equals(right, mask);
            
            //check for fields that are translatable strings
            var translatableFields = type.GetProperties()
                .Where(p => p.PropertyType.IsAssignableTo(typeof(ITranslatedStringGetter)));

            //GameSettings and Globals have subtypes for different value types
            //the only one we need to interpret is the GameSettingString
            if (type.IsAssignableTo(typeof(IGameSettingGetter)) || type.IsAssignableTo(typeof(IGlobalGetter))) {
                if (left is IGameSettingStringGetter leftString && right is IGameSettingStringGetter rightString)
                {
                    left = leftString;
                    right = rightString;
                    mask = new GameSettingString.TranslationMask(true) {
                        Version2 = false,
                        VersionControl = false,
                        FormVersion = false,
                        Data = true
                    };
                }
                else if (type.IsAssignableTo(typeof(IGameSettingGetter)))
                {
                    return ((IGameSettingGetter)left).Equals((IGameSettingGetter)right, mask);
                }
                else
                {
                    return ((IGlobalGetter)left).Equals((IGlobalGetter)right, mask);
                }
            }

            foreach (var p in translatableFields) {
                var leftString = p.GetValue(left) as ITranslatedStringGetter;
                var rightString = p.GetValue(right) as ITranslatedStringGetter;;

                if(leftString == null && rightString == null) continue;
                if(leftString == null || rightString == null) return false;

                //check that the there are the same number of langueages, with a value
                foreach(Language language in (Language[])Enum.GetValues(typeof(Language))) {
                    //skip chinese and japanese, those will be handled as a non-localized mod with utf-8 embedded strings
                    //if a they are selected as a translation option
                    if(language == Language.Chinese || language == Language.Japanese) continue;
                    leftString.TryLookup(language, out var leftVal);
                    rightString.TryLookup(language, out var rightVal);
                    var leftIsNullOrEmpty = string.IsNullOrEmpty(leftVal);
                    var rightIsNullOrEmpty = string.IsNullOrEmpty(rightVal);
                    if(string.IsNullOrEmpty(leftVal) && string.IsNullOrEmpty(rightVal)) continue;
                    if(string.IsNullOrEmpty(leftVal) || string.IsNullOrEmpty(rightVal) || leftVal != rightVal) return false;
                }

                //strings are equal so lets see if the fields are available in the mask
                var maskField = maskType.GetField(p.Name);
                if (maskField != null)
                {
                    maskField.SetValue(mask, false);
                }
                else
                {
                    //the field is not available in the mask, log for later debug
                    Console.WriteLine($"Field {p.Name} is not available in the mask for {type}");
                }
            }

            var formLinkFields = type.GetProperties()
                .Where(p => p.PropertyType.IsAssignableTo(typeof(IFormLinkGetter)));

            foreach (var p in formLinkFields) {
                var leftFormLink = p.GetValue(left) as IFormLinkGetter;
                var rightFormLink = p.GetValue(right) as IFormLinkGetter;

                var leftIsNull = leftFormLink == null || leftFormLink.IsNull;
                var rightIsNull = rightFormLink == null || rightFormLink.IsNull;

                if((!leftIsNull && !rightIsNull && leftFormLink!.FormKey != rightFormLink!.FormKey)
                    || (leftIsNull && !rightIsNull) || (!leftIsNull && rightIsNull)) 
                    return false;

                //form links are equal so lets see if the fields are available in the mask
                var maskField = maskType.GetField(p.Name);
                if (maskField != null) {
                    maskField.SetValue(mask, false);
                }
                else
                {
                    //the field is not available in the mask, log for later debug
                    Console.WriteLine($"Field {p.Name} is not available in the mask for {type}");
                }
            }

            //strings are equal and mask is updated, check equality
            return ((IMajorRecordGetter)left).Equals((IMajorRecordGetter)right, mask);
        }
    }
}
