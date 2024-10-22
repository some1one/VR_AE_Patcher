using System.Data;
using System.Diagnostics.Contracts;
using Mutagen.Bethesda.Plugins;
using VR_AE_Patcher.Interfaces;
namespace VR_AE_Patcher.Mods
{
    public static class ModKeys
    {
        [Pure]
        public static class Common
        {
            public static readonly ModKey Skyrim = ModKey.FromFileName(ModNames.Common.Skyrim);
            public static readonly ModKey Update = ModKey.FromFileName(ModNames.Common.Update);
            public static readonly ModKey Dawnguard = ModKey.FromFileName(ModNames.Common.Dawnguard);
            public static readonly ModKey HearthFires = ModKey.FromFileName(ModNames.Common.HearthFires);
            public static readonly ModKey Dragonborn = ModKey.FromFileName(ModNames.Common.Dragonborn);
        }

        [Pure]
        public static class VR
        {
            public static readonly ModKey SkyrimVR = ModKey.FromFileName(ModNames.VR.SkyrimVR);
        }

        [Pure]
        public static class AE {
            public static readonly ModKey ccAFDSSE001_DweSanctuary = ModKey.FromFileName(ModNames.AE.ccAFDSSE001_DweSanctuary);
            public static readonly ModKey ccASVSSE001_ALMSIVI = ModKey.FromFileName(ModNames.AE.ccASVSSE001_ALMSIVI);
            public static readonly ModKey ccBGSSSE002_ExoticArrows = ModKey.FromFileName(ModNames.AE.ccBGSSSE002_ExoticArrows);
            public static readonly ModKey ccBGSSSE003_Zombies = ModKey.FromFileName(ModNames.AE.ccBGSSSE003_Zombies);
            public static readonly ModKey ccBGSSSE004_RuinsEdge = ModKey.FromFileName(ModNames.AE.ccBGSSSE004_RuinsEdge);
            public static readonly ModKey ccBGSSSE005_Goldbrand = ModKey.FromFileName(ModNames.AE.ccBGSSSE005_Goldbrand);
            public static readonly ModKey ccBGSSSE006_StendarsHammer = ModKey.FromFileName(ModNames.AE.ccBGSSSE006_StendarsHammer);
            public static readonly ModKey ccBGSSSE007_Chrysamere = ModKey.FromFileName(ModNames.AE.ccBGSSSE007_Chrysamere);
            public static readonly ModKey ccBGSSSE008_Wraithguard = ModKey.FromFileName(ModNames.AE.ccBGSSSE008_Wraithguard);
            public static readonly ModKey ccBGSSSE010_PetDwarvenArmoredMudcrab = ModKey.FromFileName(ModNames.AE.ccBGSSSE010_PetDwarvenArmoredMudcrab);
            public static readonly ModKey ccBGSSSE011_HrsArmrElvn = ModKey.FromFileName(ModNames.AE.ccBGSSSE011_HrsArmrElvn);
            public static readonly ModKey ccBGSSSE012_HrsArmrStl = ModKey.FromFileName(ModNames.AE.ccBGSSSE012_HrsArmrStl);
            public static readonly ModKey ccBGSSSE013_Dawnfang = ModKey.FromFileName(ModNames.AE.ccBGSSSE013_Dawnfang);
            public static readonly ModKey ccBGSSSE014_SpellPack01 = ModKey.FromFileName(ModNames.AE.ccBGSSSE014_SpellPack01);
            public static readonly ModKey ccBGSSSE016_Umbra = ModKey.FromFileName(ModNames.AE.ccBGSSSE016_Umbra);
            public static readonly ModKey ccBGSSSE018_Shadowrend = ModKey.FromFileName(ModNames.AE.ccBGSSSE018_Shadowrend);
            public static readonly ModKey ccBGSSSE019_StaffofSheogorath = ModKey.FromFileName(ModNames.AE.ccBGSSSE019_StaffofSheogorath);
            public static readonly ModKey ccBGSSSE020_GrayCowl = ModKey.FromFileName(ModNames.AE.ccBGSSSE020_GrayCowl);
            public static readonly ModKey ccBGSSSE021_LordsMail = ModKey.FromFileName(ModNames.AE.ccBGSSSE021_LordsMail);
            public static readonly ModKey ccBGSSSE031_AdvCyrus = ModKey.FromFileName(ModNames.AE.ccBGSSSE031_AdvCyrus);
            public static readonly ModKey ccBGSSSE037_Curios = ModKey.FromFileName(ModNames.AE.ccBGSSSE037_Curios);
            public static readonly ModKey ccBGSSSE034_MntUni = ModKey.FromFileName(ModNames.AE.ccBGSSSE034_MntUni);
            public static readonly ModKey ccBGSSSE035_PetNHound = ModKey.FromFileName(ModNames.AE.ccBGSSSE035_PetNHound);
            public static readonly ModKey ccBGSSSE036_PetBWolf = ModKey.FromFileName(ModNames.AE.ccBGSSSE036_PetBWolf);
            public static readonly ModKey ccBGSSSE038_BowofShadows = ModKey.FromFileName(ModNames.AE.ccBGSSSE038_BowofShadows);
            public static readonly ModKey ccBGSSSE040_AdvObGobs = ModKey.FromFileName(ModNames.AE.ccBGSSSE040_AdvObGobs);
            public static readonly ModKey ccBGSSSE041_NetchLeather = ModKey.FromFileName(ModNames.AE.ccBGSSSE041_NetchLeather);
            public static readonly ModKey ccBGSSSE043_CrossElv = ModKey.FromFileName(ModNames.AE.ccBGSSSE043_CrossElv);
            public static readonly ModKey ccBGSSSE045_Hasedoki = ModKey.FromFileName(ModNames.AE.ccBGSSSE045_Hasedoki);
            public static readonly ModKey ccBGSSSE050_BA_Daedric = ModKey.FromFileName(ModNames.AE.ccBGSSSE050_BA_Daedric);
            public static readonly ModKey ccBGSSSE051_BA_DaedricMail = ModKey.FromFileName(ModNames.AE.ccBGSSSE051_BA_DaedricMail);
            public static readonly ModKey ccBGSSSE052_BA_Iron = ModKey.FromFileName(ModNames.AE.ccBGSSSE052_BA_Iron);
            public static readonly ModKey ccBGSSSE053_BA_Leather = ModKey.FromFileName(ModNames.AE.ccBGSSSE053_BA_Leather);
            public static readonly ModKey ccBGSSSE054_BA_Orcish = ModKey.FromFileName(ModNames.AE.ccBGSSSE054_BA_Orcish);
            public static readonly ModKey ccBGSSSE055_BA_OrcishScaled = ModKey.FromFileName(ModNames.AE.ccBGSSSE055_BA_OrcishScaled);
            public static readonly ModKey ccBGSSSE056_BA_Silver = ModKey.FromFileName(ModNames.AE.ccBGSSSE056_BA_Silver);
            public static readonly ModKey ccBGSSSE057_BA_Stalhrim = ModKey.FromFileName(ModNames.AE.ccBGSSSE057_BA_Stalhrim);
            public static readonly ModKey ccBGSSSE058_BA_Steel = ModKey.FromFileName(ModNames.AE.ccBGSSSE058_BA_Steel);
            public static readonly ModKey ccBGSSSE059_BA_Dragonplate = ModKey.FromFileName(ModNames.AE.ccBGSSSE059_BA_Dragonplate);
            public static readonly ModKey ccBGSSSE060_BA_Dragonscale = ModKey.FromFileName(ModNames.AE.ccBGSSSE060_BA_Dragonscale);
            public static readonly ModKey ccBGSSSE061_BA_Dwarven = ModKey.FromFileName(ModNames.AE.ccBGSSSE061_BA_Dwarven);
            public static readonly ModKey ccBGSSSE062_BA_DwarvenMail = ModKey.FromFileName(ModNames.AE.ccBGSSSE062_BA_DwarvenMail);
            public static readonly ModKey ccBGSSSE063_BA_Ebony = ModKey.FromFileName(ModNames.AE.ccBGSSSE063_BA_Ebony);
            public static readonly ModKey ccBGSSSE064_BA_Elven = ModKey.FromFileName(ModNames.AE.ccBGSSSE064_BA_Elven);
            public static readonly ModKey ccBGSSSE066_Staves = ModKey.FromFileName(ModNames.AE.ccBGSSSE066_Staves);
            public static readonly ModKey ccBGSSSE067_DaedInv = ModKey.FromFileName(ModNames.AE.ccBGSSSE067_DaedInv);
            public static readonly ModKey ccBGSSSE068_Bloodfall = ModKey.FromFileName(ModNames.AE.ccBGSSSE068_Bloodfall);
            public static readonly ModKey ccBGSSSE069_Contest = ModKey.FromFileName(ModNames.AE.ccBGSSSE069_Contest);
            public static readonly ModKey ccCBHSSE001_Gaunt = ModKey.FromFileName(ModNames.AE.ccCBHSSE001_Gaunt);
            public static readonly ModKey ccEDHSSE001_NorJewel = ModKey.FromFileName(ModNames.AE.ccEDHSSE001_NorJewel);
            public static readonly ModKey ccEDHSSE002_SplKntSet = ModKey.FromFileName(ModNames.AE.ccEDHSSE002_SplKntSet);
            public static readonly ModKey ccEDHSSE003_Redguard = ModKey.FromFileName(ModNames.AE.ccEDHSSE003_Redguard);
            public static readonly ModKey ccEEJSSE001_Hstead = ModKey.FromFileName(ModNames.AE.ccEEJSSE001_Hstead);
            public static readonly ModKey ccEEJSSE002_Tower = ModKey.FromFileName(ModNames.AE.ccEEJSSE002_Tower);
            public static readonly ModKey ccEEJSSE003_Hollow = ModKey.FromFileName(ModNames.AE.ccEEJSSE003_Hollow);
            public static readonly ModKey ccEEJSSE004_Hall = ModKey.FromFileName(ModNames.AE.ccEEJSSE004_Hall);
            public static readonly ModKey ccEEJSSE005_Cave = ModKey.FromFileName(ModNames.AE.ccEEJSSE005_Cave);
            public static readonly ModKey ccFFBSSE001_ImperialDragon = ModKey.FromFileName(ModNames.AE.ccFFBSSE001_ImperialDragon);
            public static readonly ModKey ccFFBSSE002_CrossbowPack = ModKey.FromFileName(ModNames.AE.ccFFBSSE002_CrossbowPack);
            public static readonly ModKey ccFSVSSE001_Backpacks = ModKey.FromFileName(ModNames.AE.ccFSVSSE001_Backpacks);
            public static readonly ModKey ccKRTSSE001_Altar = ModKey.FromFileName(ModNames.AE.ccKRTSSE001_Altar);
            public static readonly ModKey ccMTYSSE001_KnightsOfTheNine = ModKey.FromFileName(ModNames.AE.ccMTYSSE001_KnightsOfTheNine);
            public static readonly ModKey ccMTYSSE002_VE = ModKey.FromFileName(ModNames.AE.ccMTYSSE002_VE);
            public static readonly ModKey ccPEWSSE002_ArmsOfChaos = ModKey.FromFileName(ModNames.AE.ccPEWSSE002_ArmsOfChaos);
            public static readonly ModKey ccQDRSSE002_Firewood = ModKey.FromFileName(ModNames.AE.ccQDRSSE002_Firewood);
            public static readonly ModKey ccRMSSSE001_NecroHouse = ModKey.FromFileName(ModNames.AE.ccRMSSSE001_NecroHouse);
            public static readonly ModKey ccTWBSSE001_PuzzleDungeon = ModKey.FromFileName(ModNames.AE.ccTWBSSE001_PuzzleDungeon);
            public static readonly ModKey ccVSVSSE001_Winter = ModKey.FromFileName(ModNames.AE.ccVSVSSE001_Winter);
            public static readonly ModKey ccVSVSSE002_Pets = ModKey.FromFileName(ModNames.AE.ccVSVSSE002_Pets);
            public static readonly ModKey ccVSVSSE003_NecroArts = ModKey.FromFileName(ModNames.AE.ccVSVSSE003_NecroArts);
            public static readonly ModKey ccVSVSSE004_BeAFarmer = ModKey.FromFileName(ModNames.AE.ccVSVSSE004_BeAFarmer);
            public static readonly ModKey ccBGSSSE025_AdvDSGS = ModKey.FromFileName(ModNames.AE.ccBGSSSE025_AdvDSGS);
            public static readonly ModKey _ResourcePack = ModKey.FromFileName(ModNames.AE._ResourcePack);
        }
    }
}