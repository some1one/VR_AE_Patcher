using System;
using System.Collections.Immutable;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Order;
using Mutagen.Bethesda.Skyrim;

namespace VR_AE_Patcher.Mods;

public static class LoadOrders
{
    public static readonly ModKey[] Common = [ModKeys.Common.Skyrim, ModKeys.Common.Update, ModKeys.Common.Dawnguard, ModKeys.Common.HearthFires, ModKeys.Common.Dragonborn];
    public static readonly ModKey[] SSE = [.. Common];
    public static readonly ModKey[] VR = [.. Common, ModKeys.VR.SkyrimVR];
    public static readonly ModKey[] AE = [.. Common,
        ModKeys.AE.ccASVSSE001_ALMSIVI,
        //ModKeys.AE.ccBGSSSE001-Fish,
        ModKeys.AE.ccBGSSSE002_ExoticArrows,
        ModKeys.AE.ccBGSSSE003_Zombies,
        ModKeys.AE.ccBGSSSE004_RuinsEdge,
        ModKeys.AE.ccBGSSSE005_Goldbrand,
        ModKeys.AE.ccBGSSSE006_StendarsHammer,
        ModKeys.AE.ccBGSSSE007_Chrysamere,
        ModKeys.AE.ccBGSSSE010_PetDwarvenArmoredMudcrab,
        ModKeys.AE.ccBGSSSE011_HrsArmrElvn,
        ModKeys.AE.ccBGSSSE012_HrsArmrStl,
        ModKeys.AE.ccBGSSSE014_SpellPack01,
        ModKeys.AE.ccBGSSSE019_StaffofSheogorath,
        ModKeys.AE.ccBGSSSE020_GrayCowl,
        ModKeys.AE.ccBGSSSE021_LordsMail,
        ModKeys.AE.ccMTYSSE001_KnightsOfTheNine,
        //ModKeys.AE.ccqdrsse001_SurvivalMode,
        ModKeys.AE.ccTWBSSE001_PuzzleDungeon,
        ModKeys.AE.ccEEJSSE001_Hstead,
        ModKeys.AE.ccQDRSSE002_Firewood,
        ModKeys.AE.ccBGSSSE018_Shadowrend,
        ModKeys.AE.ccBGSSSE035_PetNHound,
        ModKeys.AE.ccFSVSSE001_Backpacks,
        ModKeys.AE.ccEEJSSE002_Tower,
        ModKeys.AE.ccEDHSSE001_NorJewel,
        ModKeys.AE.ccVSVSSE002_Pets,
        ModKeys.AE.ccBGSSSE037_Curios,
        ModKeys.AE.ccBGSSSE034_MntUni,
        ModKeys.AE.ccBGSSSE045_Hasedoki,
        ModKeys.AE.ccBGSSSE008_Wraithguard,
        ModKeys.AE.ccBGSSSE036_PetBWolf,
        ModKeys.AE.ccFFBSSE001_ImperialDragon,
        ModKeys.AE.ccMTYSSE002_VE,
        ModKeys.AE.ccBGSSSE043_CrossElv,
        ModKeys.AE.ccVSVSSE001_Winter,
        ModKeys.AE.ccEEJSSE003_Hollow,
        ModKeys.AE.ccBGSSSE016_Umbra,
        ModKeys.AE.ccBGSSSE031_AdvCyrus,
        ModKeys.AE.ccBGSSSE038_BowofShadows,
        ModKeys.AE.ccBGSSSE040_AdvObGobs,
        ModKeys.AE.ccBGSSSE050_BA_Daedric,
        ModKeys.AE.ccBGSSSE052_BA_Iron,
        ModKeys.AE.ccBGSSSE054_BA_Orcish,
        ModKeys.AE.ccBGSSSE058_BA_Steel,
        ModKeys.AE.ccBGSSSE059_BA_Dragonplate,
        ModKeys.AE.ccBGSSSE061_BA_Dwarven,
        ModKeys.AE.ccPEWSSE002_ArmsOfChaos,
        ModKeys.AE.ccBGSSSE041_NetchLeather,
        ModKeys.AE.ccEDHSSE002_SplKntSet,
        ModKeys.AE.ccBGSSSE064_BA_Elven,
        ModKeys.AE.ccBGSSSE063_BA_Ebony,
        ModKeys.AE.ccBGSSSE062_BA_DwarvenMail,
        ModKeys.AE.ccBGSSSE060_BA_Dragonscale,
        ModKeys.AE.ccBGSSSE056_BA_Silver,
        ModKeys.AE.ccBGSSSE055_BA_OrcishScaled,
        ModKeys.AE.ccBGSSSE053_BA_Leather,
        ModKeys.AE.ccBGSSSE051_BA_DaedricMail,
        ModKeys.AE.ccBGSSSE057_BA_Stalhrim,
        ModKeys.AE.ccBGSSSE066_Staves,
        ModKeys.AE.ccBGSSSE067_DaedInv,
        ModKeys.AE.ccBGSSSE068_Bloodfall,
        ModKeys.AE.ccBGSSSE069_Contest,
        ModKeys.AE.ccVSVSSE003_NecroArts,
        ModKeys.AE.ccVSVSSE004_BeAFarmer,
        ModKeys.AE.ccBGSSSE025_AdvDSGS,
        ModKeys.AE.ccFFBSSE002_CrossbowPack,
        ModKeys.AE.ccBGSSSE013_Dawnfang,
        ModKeys.AE.ccRMSSSE001_NecroHouse,
        ModKeys.AE.ccEDHSSE003_Redguard,
        ModKeys.AE.ccEEJSSE004_Hall,
        ModKeys.AE.ccEEJSSE005_Cave,
        ModKeys.AE.ccKRTSSE001_Altar,
        ModKeys.AE.ccCBHSSE001_Gaunt,
        ModKeys.AE.ccAFDSSE001_DweSanctuary,
        ModKeys.AE._ResourcePack
    ];
}
