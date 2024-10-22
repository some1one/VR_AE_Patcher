using System.Data;
using System.Diagnostics.Contracts;
namespace VR_AE_Patcher.Mods
{
    public readonly struct ModNames
    {
        [Pure]
        public static class Common
        {
            public const string Skyrim = "Skyrim.esm";
            public const string Update = "Update.esm";
            public const string Dawnguard = "Dawnguard.esm";
            public const string HearthFires = "HearthFires.esm";
            public const string Dragonborn = "Dragonborn.esm";
        }

        [Pure]
        public static class VR
        {
            public const string SkyrimVR = "SkyrimVR.esm";
        }

        [Pure]
        public static class AE {
            public const string ccAFDSSE001_DweSanctuary = "ccAFDSSE001-DweSanctuary.esm";
            public const string ccASVSSE001_ALMSIVI = "ccASVSSE001-ALMSIVI.esm";
            public const string ccBGSSSE002_ExoticArrows = "ccBGSSSE002-ExoticArrows.esl";
            public const string ccBGSSSE003_Zombies = "ccBGSSSE003-Zombies.esl";
            public const string ccBGSSSE004_RuinsEdge = "ccBGSSSE004-RuinsEdge.esl";
            public const string ccBGSSSE005_Goldbrand = "ccBGSSSE005-Goldbrand.esl";
            public const string ccBGSSSE006_StendarsHammer = "ccBGSSSE006-StendarsHammer.esl";
            public const string ccBGSSSE007_Chrysamere = "ccBGSSSE007-Chrysamere.esl";
            public const string ccBGSSSE008_Wraithguard = "ccBGSSSE008-Wraithguard.esl";
            public const string ccBGSSSE010_PetDwarvenArmoredMudcrab = "ccBGSSSE010-PetDwarvenArmoredMudcrab.esl";
            public const string ccBGSSSE011_HrsArmrElvn = "ccBGSSSE011-HrsArmrElvn.esl";
            public const string ccBGSSSE012_HrsArmrStl = "ccBGSSSE012-HrsArmrStl.esl";
            public const string ccBGSSSE013_Dawnfang = "ccBGSSSE013-Dawnfang.esl";
            public const string ccBGSSSE014_SpellPack01 = "ccBGSSSE014-SpellPack01.esl";
            public const string ccBGSSSE016_Umbra = "ccBGSSSE016-Umbra.esm";
            public const string ccBGSSSE018_Shadowrend = "ccBGSSSE018-Shadowrend.esl";
            public const string ccBGSSSE019_StaffofSheogorath = "ccBGSSSE019-StaffofSheogorath.esl";
            public const string ccBGSSSE020_GrayCowl = "ccBGSSSE020-GrayCowl.esl";
            public const string ccBGSSSE021_LordsMail = "ccBGSSSE021-LordsMail.esl";
            public const string ccBGSSSE031_AdvCyrus = "ccBGSSSE031-AdvCyrus.esm";
            public const string ccBGSSSE037_Curios = "ccBGSSSE037-Curios.esl";
            public const string ccBGSSSE034_MntUni = "ccBGSSSE034-MntUni.esl";
            public const string ccBGSSSE035_PetNHound = "ccBGSSSE035-PetNHound.esl";
            public const string ccBGSSSE036_PetBWolf = "ccBGSSSE036-PetBWolf.esl";
            public const string ccBGSSSE038_BowofShadows = "ccBGSSSE038-BowofShadows.esl";
            public const string ccBGSSSE040_AdvObGobs = "ccBGSSSE040-AdvObGobs.esl";
            public const string ccBGSSSE041_NetchLeather = "ccBGSSSE041-NetchLeather.esl";
            public const string ccBGSSSE043_CrossElv = "ccBGSSSE043-CrossElv.esl";
            public const string ccBGSSSE045_Hasedoki = "ccBGSSSE045-Hasedoki.esl";
            public const string ccBGSSSE050_BA_Daedric = "ccBGSSSE050-BA_Daedric.esl";
            public const string ccBGSSSE051_BA_DaedricMail = "ccBGSSSE051-BA_DaedricMail.esl";
            public const string ccBGSSSE052_BA_Iron = "ccBGSSSE052-BA_Iron.esl";
            public const string ccBGSSSE053_BA_Leather = "ccBGSSSE053-BA_Leather.esl";
            public const string ccBGSSSE054_BA_Orcish = "ccBGSSSE054-BA_Orcish.esl";
            public const string ccBGSSSE055_BA_OrcishScaled = "ccBGSSSE055-BA_OrcishScaled.esl";
            public const string ccBGSSSE056_BA_Silver = "ccBGSSSE056-BA_Silver.esl";
            public const string ccBGSSSE057_BA_Stalhrim = "ccBGSSSE057-BA_Stalhrim.esl";
            public const string ccBGSSSE058_BA_Steel = "ccBGSSSE058-BA_Steel.esl";
            public const string ccBGSSSE059_BA_Dragonplate = "ccBGSSSE059-BA_Dragonplate.esl";
            public const string ccBGSSSE060_BA_Dragonscale = "ccBGSSSE060-BA_Dragonscale.esl";
            public const string ccBGSSSE061_BA_Dwarven = "ccBGSSSE061-BA_Dwarven.esl";
            public const string ccBGSSSE062_BA_DwarvenMail = "ccBGSSSE062-BA_DwarvenMail.esl";
            public const string ccBGSSSE063_BA_Ebony = "ccBGSSSE063-BA_Ebony.esl";
            public const string ccBGSSSE064_BA_Elven = "ccBGSSSE064-BA_Elven.esl";
            public const string ccBGSSSE066_Staves = "ccBGSSSE066-Staves.esl";
            public const string ccBGSSSE067_DaedInv = "ccBGSSSE067-DaedInv.esm";
            public const string ccBGSSSE068_Bloodfall = "ccBGSSSE068-Bloodfall.esl";
            public const string ccBGSSSE069_Contest = "ccBGSSSE069-Contest.esl";
            public const string ccCBHSSE001_Gaunt = "ccCBHSSE001-Gaunt.esl";
            public const string ccEDHSSE001_NorJewel = "ccEDHSSE001-NorJewel.esl";
            public const string ccEDHSSE002_SplKntSet = "ccEDHSSE002-SplKntSet.esl";
            public const string ccEDHSSE003_Redguard = "ccEDHSSE003-Redguard.esl";
            public const string ccEEJSSE001_Hstead = "ccEEJSSE001-Hstead.esm";
            public const string ccEEJSSE002_Tower = "ccEEJSSE002-Tower.esl";
            public const string ccEEJSSE003_Hollow = "ccEEJSSE003-Hollow.esl";
            public const string ccEEJSSE004_Hall = "ccEEJSSE004-Hall.esl";
            public const string ccEEJSSE005_Cave = "ccEEJSSE005-Cave.esm";
            public const string ccFFBSSE001_ImperialDragon = "ccFFBSSE001-ImperialDragon.esl";
            public const string ccFFBSSE002_CrossbowPack = "ccFFBSSE002-CrossbowPack.esl";
            public const string ccFSVSSE001_Backpacks = "ccFSVSSE001-Backpacks.esl";
            public const string ccKRTSSE001_Altar = "ccKRTSSE001_Altar.esl";
            public const string ccMTYSSE001_KnightsOfTheNine = "ccMTYSSE001-KnightsOfTheNine.esl";
            public const string ccMTYSSE002_VE = "ccMTYSSE002-VE.esl";
            public const string ccPEWSSE002_ArmsOfChaos = "ccPEWSSE002-ArmsOfChaos.esl";
            public const string ccQDRSSE002_Firewood = "ccQDRSSE002-Firewood.esl";
            public const string ccRMSSSE001_NecroHouse = "ccRMSSSE001-NecroHouse.esl";
            public const string ccTWBSSE001_PuzzleDungeon = "ccTWBSSE001-PuzzleDungeon.esm";
            public const string ccVSVSSE001_Winter = "ccVSVSSE001-Winter.esl";
            public const string ccVSVSSE002_Pets = "ccVSVSSE002-Pets.esl";
            public const string ccVSVSSE003_NecroArts = "ccVSVSSE003-NecroArts.esl";
            public const string ccVSVSSE004_BeAFarmer = "ccVSVSSE004-BeAFarmer.esl";
            public const string ccBGSSSE025_AdvDSGS = "ccBGSSSE025-AdvDSGS.esm";
            public const string _ResourcePack = "_ResourcePack.esl";
        }
    }
}