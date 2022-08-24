using TaleWorlds.MountAndBlade;
using HarmonyLib;
using System.Reflection;

namespace Shards
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad() {
            base.OnSubModuleLoad();
        }

        protected override void OnSubModuleUnloaded() {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot() {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            var harmony = new Harmony("com.Shards.jullinator");

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}

