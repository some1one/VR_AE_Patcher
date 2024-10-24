using System.Diagnostics;
using System.Reflection;
using GameFinder.Common;
using Loqui;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Mapping;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings;
using Noggog;
using Noggog.StructuredStrings;
using Noggog.StructuredStrings.CSharp;
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

    public class EqualityChecker
    {
        private readonly dynamic left;
        private readonly dynamic right;
        private readonly bool localized;
        private readonly Type type;
        private SkyrimMajorRecord.TranslationMask mask;
        private readonly Type maskType;
        
        public EqualityChecker(dynamic left, dynamic right, bool localized = false)
        {
            this.left = left;
            this.right = right;
            this.localized = localized;
            type = (Type)left.GetType();
            mask = CreateTranslationMask(left);
            maskType = mask.GetType();
        }

        public bool TryGlobalsGameSettingsEquals(out bool equal) {
            equal = true;
            //GameSettings and Globals have subtypes for different value types
            //the only one we need to interpret is the GameSettingString
            var interfaceType = typeof(IGameSettingGetter);
            if (type.IsAssignableTo(interfaceType) || type.IsAssignableTo(typeof(IGlobalGetter)))
            {
                if (left is IGameSettingStringGetter && right is IGameSettingStringGetter)
                {
                    mask = new GameSettingString.TranslationMask(true)
                    {
                        Version2 = false,
                        VersionControl = false,
                        FormVersion = false,
                        Data = true
                    };
                }
                else if (type.IsAssignableTo(interfaceType))
                {
                    equal = ((IGameSettingGetter)left).Equals((IGameSettingGetter)right, mask);
                    return false;
                }
                else
                {
                    equal = ((IGlobalGetter)left).Equals((IGlobalGetter)right, mask);
                    return false;
                }
            }

            return true;
        }

        private bool TranslationEqual(ITranslatedStringGetter? left, ITranslatedStringGetter? right) {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            foreach (Language language in (Language[])Enum.GetValues(typeof(Language)))
            {
                //skip chinese and japanese, those will be handled as a non-localized mod with utf-8 embedded strings
                //if a they are selected as a translation option
                if (language == Language.Chinese || language == Language.Japanese) continue;
                left.TryLookup(language, out var leftVal);
                right.TryLookup(language, out var rightVal);
                var leftIsNullOrEmpty = string.IsNullOrEmpty(leftVal);
                var rightIsNullOrEmpty = string.IsNullOrEmpty(rightVal);
                if (leftIsNullOrEmpty && rightIsNullOrEmpty) continue;
                if (leftIsNullOrEmpty || rightIsNullOrEmpty || leftVal != rightVal)
                    return false;
            }

            return true;
        }

        private bool FormLinkEqual(IFormLinkGetter? leftFormLink, IFormLinkGetter? rightFormLink) {
            var leftIsNull = leftFormLink == null || leftFormLink.IsNull;
            var rightIsNull = rightFormLink == null || rightFormLink.IsNull;

            if ((!leftIsNull && !rightIsNull && leftFormLink!.FormKey != rightFormLink!.FormKey)
                || (leftIsNull && !rightIsNull) || (!leftIsNull && rightIsNull))
                return false;

            return true;
        }

        private bool GenderedItemEqual(dynamic? leftGenderedItem, dynamic? rightGenderedItem) {
            if (leftGenderedItem == null && rightGenderedItem == null) return true;
            if (leftGenderedItem == null || rightGenderedItem == null) return false;

            //todo: complex generics throw exception when calling leftMaleValue cause they are cast as object instead of the correct dynamic type
            //we can get around this by using reflection to get the property getter and invoke it
            var MaleGetter = ((Type)leftGenderedItem!.GetType()).GetProperty("Male")!.GetMethod!;
            var FemaleGetter = ((Type)leftGenderedItem!.GetType()).GetProperty("Female")!.GetMethod!;
            var leftMaleValue = MaleGetter.Invoke(leftGenderedItem, null);
            var rightMaleValue = MaleGetter.Invoke(rightGenderedItem, null);
            var leftFemaleValue = FemaleGetter.Invoke(leftGenderedItem, null);
            var rightFemaleValue = FemaleGetter.Invoke(rightGenderedItem, null);
            try {
                if (leftMaleValue == null && rightMaleValue == null 
                && leftFemaleValue == null && rightFemaleValue == null) return true;
            } catch (Exception e) {
                var test = "debug";
                return false;
            }

            if (leftMaleValue == null || rightMaleValue == null || leftFemaleValue == null || rightFemaleValue == null) return false;

            var hasMyEqual = TryFieldMyEquals(leftMaleValue, rightMaleValue, out bool maleEqual);
            if (!maleEqual) return false;
            TryFieldMyEquals(leftFemaleValue, rightFemaleValue, out bool femaleEqual);
            if (!femaleEqual) return false;

            if (hasMyEqual) return true;

            return leftMaleValue!.Equals(rightMaleValue)
                && leftFemaleValue!.Equals(rightFemaleValue); //todo: figure out how to used typed equal extension with masks  or what else should i do?
        }

        private bool ListFieldEqual<T>(IReadOnlyList<T>? leftList, IReadOnlyList<T>? rightList) {
            if (leftList == null && rightList == null) return true;
            if (leftList == null || rightList == null) return false;
            if (leftList.Count != rightList.Count) return false;
            if (leftList.Count == 0) return true;

            for (int i = 0; i < leftList.Count; i++)
            {
                var left = leftList[i];
                var right = rightList[i];
                var usedMyCompare = TryFieldMyEquals(left, right, out var equal);
                if (!equal) return false;
                if (!usedMyCompare) return left?.Equals(right) ?? false; //todo: figure out how to used typed equal extension with masks  or what else should i do?
            }

            return true;
        }

        private bool DictionaryFieldEqual(dynamic? leftDict, dynamic? rightDict) {
            if (leftDict == null && rightDict == null) return true;
            if (leftDict == null || rightDict == null) return false;
            if (leftDict!.Count != rightDict!.Count) return false;
            if (leftDict.Count == 0) return true;

            foreach (var key in leftDict.Keys)
            {
                //TODO: advanced generics crash at runtime saying the type parameters can't be infered
                //like dictionary of APackageData
                var rightValue = TypeHelpers.TryFindValueDynamic(rightDict, key, out bool found);
                if (!found) return false;
                var leftValue = leftDict[key];
                var usedMyCompare = TryFieldMyEquals(leftValue, rightValue, out bool equal);
                if (!equal) return false;
                if (!usedMyCompare) return left?.Equals(right) ?? false; //todo: figure out how to used typed equal extension with masks  or what else should i do?
            }

            return true;
        }

        private bool TryFieldMyEquals(dynamic? left, dynamic? right, out bool equal) {
            equal = true;
            try {
                if (left == null && right == null) return true;
            }
            catch (Exception e) {
                var test = "debug";
                return false;
            }
            equal = left != null && right != null; //if both are not null, flag as equal for now so we can do more checking
            if (!equal) return true;

            switch (left)
            {
                //no need to check value types
                case ValueType leftValue when right is ValueType rightValue:
                    equal = leftValue.Equals(rightValue);
                    return true;
                //treat strings as a value type
                case string leftString when right is string rightString:
                    equal = leftString == rightString;
                    return true;
                case IFormLinkGetter leftFormLink when right is IFormLinkGetter rightFormLink:
                    equal = FormLinkEqual(leftFormLink, rightFormLink);
                    return true;
                case ITranslatedStringGetter leftString when right is ITranslatedStringGetter rightString:
                    equal = TranslationEqual(leftString, rightString);
                    return true;
            };

            //todo: handle APackageData

            //can't call extension methods on dynamic types
            var type = left?.GetType() as Type;

            //can't put generic types in the switch statement
            if(type?.ImplementsOrDerives(typeof(IReadOnlyList<>)) == true) {
                equal = ListFieldEqual(left, right);
                return true;
            }

            if (type?.ImplementsOrDerives(typeof(IReadOnlyDictionary<,>)) == true) {
                equal = DictionaryFieldEqual(left, right);
                return true;
            }

            if (type?.ImplementsOrDerives(typeof(IGenderedItemGetter<>)) == true) {
                equal = GenderedItemEqual(left, right);
                return true;
            }

            //TODO: for VirtualMachineAdapter fields we need to recurse deeper

           // if (left != null && right != null) //no MyEquals check found
            return false;

            //return true;
        }

        public bool TryMaskMyEqualsField(PropertyInfo field, out bool equal) {
            equal = true;

            // if(field.DeclaringType?.IsAssignableTo(typeof(IRaceGetter)) == true) {
            //     Debugger.Break();
            // }

            if(maskType.GetField(field.Name)?.GetValue(mask) != null) return true;


            var leftValue = field.GetValue(left);
            var rightValue = field.GetValue(right);

            var hadMyEqual = TryFieldMyEquals(leftValue, rightValue, out equal);
            if(!equal) return true;
            if(!hadMyEqual) {
                //lets try a normal equality just to see if we get true
                //TODO: how to know if using an Equals overide or object.Equals?
                //TODO: how to find Equals extensions?
                var normalEqual = leftValue?.Equals(rightValue) ?? false;
                if(!normalEqual) {
                    //how to check for special field types like Decal or ObjectBounds?
                    var test = "debug";
                }
                //if we get true we should continue to mask it
            }

            //field is equal so lets see if the field is available in the mask
            var maskField = maskType.GetField(field.Name);
            if (maskField != null)
            {
                var maskFieldType = maskField.FieldType;

                //first check if mask field already has a value from the default instance
                if (maskField.GetValue(mask) != null) return true;

                //if field uses a translation mask, we need to check if it can use the default mask type or if it needs a custom mask
                if(maskFieldType.IsAssignableFrom(typeof(SkyrimMajorRecord.TranslationMask))) {
                    var subMask = new SkyrimMajorRecord.TranslationMask(false, false);

                    //TODO: cases where default mask won't work, probably should just be handled by CreateTranslationMask

                    maskField.SetValue(mask, subMask);
                    return true;
                }

                if (maskField.FieldType.IsAssignableTo(typeof(ITranslationMask))) {
                    //create new mask using fieldMaskType
                    var subMask = (ITranslationMask)System.Activator.CreateInstance(maskFieldType, false, false)!;
                    maskField.SetValue(mask, subMask);
                    return true;
                }

                //handle gendered item masks
                if (maskField.FieldType.IsAssignableTo(typeof(IGenderedItem<bool>))) {
                    var subMask = new GenderedItem<bool>(false, false);
                    maskField.SetValue(mask, subMask);
                    return true;
                } //handle generic gendered item masks
                else if (maskField.FieldType.ImplementsOrDerives(typeof(IGenderedItem<>))) {
                    var genderedItemMaskType = maskField.FieldType.GetGenericArguments()[0];
                    //TODO: just like type equals, make methods for handling types to re-use during sub types
                    var subMaskMale = System.Activator.CreateInstance(genderedItemMaskType, false, false)!;
                    var subMaskFemale = System.Activator.CreateInstance(genderedItemMaskType, false, false)!;
                    var subMask = System.Activator.CreateInstance(maskField.FieldType, subMaskMale, subMaskFemale)!;
                    maskField.SetValue(mask, subMask);
                    return true;
                }


                //wtf does this mean if we get here?
            }

            return false;
        }

        public bool MaskMyEqualsFields() {
            var fields = type.GetProperties();

            if(((ISkyrimMajorRecordGetter)left).EditorID == "Siblings") {
                Debugger.Break();
            }

            foreach (var p in fields)
            {
                //no mask has IsCompressed, IsDeleted, or MahorFlags
                //race includes some fields that should be skipped
                //we should also skip type info fields
                //also skip the location bool
                string[] skip = [
                    "IsCompressed", 
                    "IsDeleted", 
                    "MajorFlags", 
                    "LinkType", 
                    "Registration", 
                    "Type", 
                    "SubtypeName", 
                    "localized", 
                    "StaticRegistration", 
                    "ExportingExtraNam2", 
                    "ExportingExtraNam3",
                    "BodyTemplate_IsSet"
                ];
                if(skip.Contains(p.Name)) continue;


                //we should also skip MemorySlices
                if(p.PropertyType.ImplementsOrDerives(typeof(ReadOnlyMemorySlice<>))
                    || (p.PropertyType.ImplementsOrDerives(typeof(Nullable<>))
                        && p.PropertyType.GetGenericArguments()[0].ImplementsOrDerives(typeof(ReadOnlyMemorySlice<>)))
                    || (p.PropertyType.ImplementsOrDerives(typeof(IReadOnlyList<>))
                        && p.PropertyType.GetGenericArguments()[0].ImplementsOrDerives(typeof(ReadOnlyMemorySlice<>)))
                )
                    continue;
                
                //and skip RecordType fields
                if(p.PropertyType.IsAssignableTo(typeof(RecordType))
                 || p.PropertyType.IsAssignableTo(typeof(RecordType?)))
                    continue;


                //TODO: skip until functionality is added
                if(p.PropertyType.IsAssignableTo(typeof(IAPackageDataGetter))
                    || p.PropertyType.IsAssignableTo(typeof(IReadOnlyDictionary<sbyte,IAPackageDataGetter>)))
                        continue;

                var didMask = TryMaskMyEqualsField(p, out var fieldEqual);
                if(!fieldEqual) {
                    return false;
                }

                if (!didMask)
                {
                    Console.WriteLine($"Field {p.Name} was not masked for {type}");
                }
            }

            return true;
        }

        public bool AreEqual()
        {
            if (type != right.GetType()) return false;

            if(!TryGlobalsGameSettingsEquals(out var equal))
                return equal;
            
            if(!MaskMyEqualsFields())
                return false;
            
            //todo: some masks use GenderedItem, these do not work correctly
            return ((IMajorRecordGetter)left).Equals((IMajorRecordGetter)right, mask);
        }
    }
}
