using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Shards
{
    [HarmonyPatch]
    internal static class SliceThrough
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MissionCombatMechanicsHelper), "GetAttackCollisionResults")]
        private static void GetAttackCollisionResults(AttackInformation attackInformation,
            ref bool crushedThrough, ref float momentumRemaining, MissionWeapon attackerWeapon,
            ref bool cancelDamage, ref AttackCollisionData attackCollisionData, 
            ref CombatLogData combatLog, ref int speedBonus) 
        {

            if (attackerWeapon.GetModifiedItemName().Contains("Shard Blade")) {
                momentumRemaining = 10f;
                crushedThrough = true;
                cancelDamage = false;
            }
        }

    }

}