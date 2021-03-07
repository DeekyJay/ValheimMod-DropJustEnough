namespace DropJustEnough
{
    using BepInEx;
    using HarmonyLib;

    [BepInPlugin("io.deek.plugins.dropjustenough", "DropJustEnough", "1.0.1.0")]
    [BepInProcess("valheim.exe")]
    public class DropJustEnoughPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("io.deek.plugins.dropjustenough");
            harmony.PatchAll();
        }
    }
}
