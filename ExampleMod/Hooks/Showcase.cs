using ExampleMod.Util;
using HarmonyLib;

namespace ExampleMod.Hooks
{
    internal static class Showcase
    {
        internal static Harmony HarmonyInstance;

        /// <summary>
        /// Show that you can use either monomod hooks, or harmony patches for hooking game methods.
        /// </summary>
        internal static void Init()
        {
            EnableMonoModHooks();

            EnableHarmonyPatches();
        }

        /// <summary>
        /// Disable the enabled hooks.
        /// </summary>
        internal static void Disable()
        {
            DisableMonoModHooks();

            DisableHarmonyPatches();
        }

        private static void EnableMonoModHooks()
        {
            On.FejdStartup.Awake += OnFejdStartupAwakeMonoModHookShowcase;
        }

        internal static void DisableMonoModHooks()
        {
            On.FejdStartup.Awake -= OnFejdStartupAwakeMonoModHookShowcase;
        }

        private static void EnableHarmonyPatches()
        {
            HarmonyInstance = new Harmony(ExampleMod.ModGuid);
            HarmonyInstance.PatchAll();
        }

        private static void DisableHarmonyPatches()
        {
            HarmonyInstance.UnpatchSelf();
        }

        private static void OnFejdStartupAwakeMonoModHookShowcase(On.FejdStartup.orig_Awake orig, FejdStartup self)
        {
            CoroutineExtensions.DelayedMethod(2, () =>
            {
                Log.LogInfo("Hello from a monomod hook, this method is fired after a 2 seconds delay !");
            });

            // calling the original method
            orig(self);

            Log.LogInfo("Hello from a monomod hook, this method is fired after the original method is called : " + self.m_betaText);
        }
    }

    [HarmonyPatch(typeof(FejdStartup))]
    [HarmonyPatch("Awake")]
    internal class ShowcaseHarmonyPatch
    {
        static bool Prefix(FejdStartup __instance)
        {
            CoroutineExtensions.DelayedMethod(2, () =>
            {
                Log.LogInfo("Hello from Patch.Prefix() , this method is fired after a 2 seconds delay !");
            });

            // if we return false in a Prefix
            // we skip all other potential hooks
            // from other mods hooking this same method !
            return true; 
        }

        static void Postfix(FejdStartup __instance)
        {
            Log.LogInfo("Hello from Patch.Postfix(), this method is fired after the original method is called : " + __instance.m_betaText);
        }
    }
}
