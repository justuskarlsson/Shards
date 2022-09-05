using HarmonyLib;

using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
namespace Shards
{

    [HarmonyPatch]
    public static class ShardPlateMissionPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Agent), "RegisterBlow")]
        private static bool RegisterBlowPrepatch(Blow blow, in AttackCollisionData collisionData, ref Agent __instance) {
            Agent victim = __instance;
            Agent attacker = (blow.OwnerId != -1) ? victim.Mission.FindAgentWithIndex(blow.OwnerId) : victim; // Check SharBlade
            ShardPlate? victimPlate = ShardPlateMissionBehaviour.TryGetShardPlate(victim);
            ShardPlateMissionBehaviour? current = ShardPlateMissionBehaviour.current;

            if (current == null) { return false; }


            if (attacker != victim && victimPlate == null && ShardPlateMissionBehaviour.AgentHasShardBlade(attacker)) {
                current.ShardKillNonPlate(victim, blow);
                return false;
            }
            if (victimPlate != null) {
                if (victimPlate.health < 1f) {
                    return true;
                }
                current.PlateHit(victimPlate, blow);
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Agent), "AddController")]
        public static void AddController(Type type) {
            Debug.Log(type);

        }
    }
}
