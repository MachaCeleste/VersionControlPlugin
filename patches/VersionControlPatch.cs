using VersionControlPlugin;
using HarmonyLib;

[HarmonyPatch]
public class VersionControlPatch
{
    [HarmonyPatch(typeof(VersionsControl), "Configure")]
    class ConfigurePatch
    {
        static void Postfix(VersionsControl __instance)
        {
            int n = Plugin.initialVersions.Value;
            var libs = __instance.GetLastLibs();
            if (n < 1 || libs["metaxploit.so"].GetStrVersion() != "1.0.0") return;
            var rnd = new System.Random();
            for (var i = 0; i < n; i++)
            {
                foreach (var lib in libs)
                {
                    var mem = lib.Value.GetRandomMem(rnd);
                    var vulns = mem.vulnerabs;
                    vulns[rnd.Next(0, vulns.Count)].helperHackResult.vecesUsado = VersionsControl.MAX_USED_EXPLOIT;
                }
                var method = AccessTools.Method(typeof(VersionsControl), "ThreadCheckVersions");
                method.Invoke(__instance, new object[] { libs["metaxploit.so"] });
            }
        }
    }
}