using HarmonyLib;

using TaleWorlds.MountAndBlade;
using System;


namespace Shards.Battle
{

    [HarmonyPatch]
    public static class ShardPlateMissionPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Agent), "RegisterBlow")]
        private static bool RegisterBlowPrepatch(Blow blow, in AttackCollisionData collisionData, ref Agent __instance)
        {
            Agent victim = __instance;
            Agent attacker = blow.OwnerId != -1 ? victim.Mission.FindAgentWithIndex(blow.OwnerId) : victim; // Check SharBlade
            ShardPlate? victimPlate = ShardPlateMissionBehaviour.TryGetShardPlate(victim);
            ShardPlateMissionBehaviour? current = ShardPlateMissionBehaviour.current;

            if (current == null) { return false; }


            if (attacker != victim && victimPlate == null && ShardPlateMissionBehaviour.AgentHasShardBlade(attacker))
            {
                current.ShardKillNonPlate(victim, blow);
                return false;
            }
            if (victimPlate != null)
            {
                if (victimPlate.health < 1f)
                {
                    return true;
                }
                current.PlateHit(victimPlate, blow);
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Agent), "AddController")]
        public static void AddController(Type type)
        {
            Debug.Log(type);

        }
    }
}
