using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace VersionControlPlugin;

[BepInPlugin("com.machaceleste.versioncontrolplugin", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Grey Hack.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    public static ConfigEntry<int> initialVersions;


    private void Awake()
    {
        initialVersions = Config.Bind("Main", "Initial Library Versions", 0, new ConfigDescription("Sets the initial number of library versions for the system to generate.", new AcceptableValueRange<int>(0, 10)));

        Logger = base.Logger;
        Logger.LogInfo($"VersionControlPlugin is loaded!");
        var harmony = new Harmony("com.machaceleste.versioncontrolplugin");
        harmony.PatchAll();
    }
}