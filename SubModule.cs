using HarmonyLib;
using TaleWorlds.MountAndBlade;
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
            Debug.Log("Init Harmony");
            var harmony = new Harmony("com.Shards.jullinator");
            Debug.Log("Patch All.");
            harmony.PatchAll();
            Debug.Log("Success!");
        }
        public override void OnMissionBehaviorInitialize(Mission mission) {
            base.OnMissionBehaviorInitialize(mission);
            mission.AddMissionBehavior(new ShardPlateMissionBehaviour());
        }
    }
}

