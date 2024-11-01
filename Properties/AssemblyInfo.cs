using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(TTOTF_TrueGear.BuildInfo.Description)]
[assembly: AssemblyDescription(TTOTF_TrueGear.BuildInfo.Description)]
[assembly: AssemblyCompany(TTOTF_TrueGear.BuildInfo.Company)]
[assembly: AssemblyProduct(TTOTF_TrueGear.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + TTOTF_TrueGear.BuildInfo.Author)]
[assembly: AssemblyTrademark(TTOTF_TrueGear.BuildInfo.Company)]
[assembly: AssemblyVersion(TTOTF_TrueGear.BuildInfo.Version)]
[assembly: AssemblyFileVersion(TTOTF_TrueGear.BuildInfo.Version)]
[assembly: MelonInfo(typeof(TTOTF_TrueGear.TTOTF_TrueGear), TTOTF_TrueGear.BuildInfo.Name, TTOTF_TrueGear.BuildInfo.Version, TTOTF_TrueGear.BuildInfo.Author, TTOTF_TrueGear.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]