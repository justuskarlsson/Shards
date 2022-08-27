using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;

namespace Shards
{
    [HarmonyPatch(typeof(MissionCombatMechanicsHelper))]
    public static class ShardBladePatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("GetAttackCollisionResults")]
        private static void GetAttackCollisionResults(AttackInformation attackInformation,
            ref bool crushedThrough, ref float momentumRemaining, MissionWeapon attackerWeapon,
            ref bool cancelDamage, ref AttackCollisionData attackCollisionData,
            out CombatLogData combatLog, out int speedBonus) {


            if (
                !attackInformation.IsVictimAgentNull &&
                !attackInformation.IsFriendlyFire &&
                attackerWeapon.Item != null &&
                attackerWeapon.GetModifiedItemName().Contains("Shard Blade")
            ) {
                Debug.Log(
                    "---- Pre GetAttackCollisionResults ---- ",
                    attackerWeapon.Item,
                    "VictimAgentAbsorbedDamageRatio: " + attackInformation.VictimAgentAbsorbedDamageRatio,
                    "momentumRemaining: " + momentumRemaining,
                    Debug.DescribeObject(attackCollisionData)
                 );
                momentumRemaining = 100f;
                crushedThrough = true;
                cancelDamage = false;
                attackCollisionData = SetNotBlocked(attackCollisionData);
                attackCollisionData.BaseMagnitude = 500f;
            }

            speedBonus = 0;
            combatLog = new CombatLogData();
        }

        [HarmonyPostfix]
        [HarmonyPatch("GetAttackCollisionResults")]
        private static void GetAttackCollisionResultsPost(AttackInformation attackInformation,
            ref bool crushedThrough, ref float momentumRemaining, MissionWeapon attackerWeapon,
            ref bool cancelDamage, ref AttackCollisionData attackCollisionData,
            ref CombatLogData combatLog, ref int speedBonus) {

            if (
                !attackInformation.IsVictimAgentNull &&
                !attackInformation.IsFriendlyFire &&
                attackerWeapon.Item != null &&
                attackerWeapon.GetModifiedItemName().Contains("Shard Blade")
            ) {
                Debug.Log(
                    "---- Post GetAttackCollisionResults ---- ",
                    attackerWeapon.Item,
                    "VictimAgentAbsorbedDamageRatio: " + attackInformation.VictimAgentAbsorbedDamageRatio,
                    "momentumRemaining: " + momentumRemaining,
                    Debug.DescribeObject(attackCollisionData)
                 );
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("GetDefendCollisionResults")]
        internal static bool GetDefendCollisionResults(
            Agent attackerAgent,
            Agent defenderAgent,
            CombatCollisionResult collisionResult,
            int attackerWeaponSlotIndex,
            bool isAlternativeAttack,
            StrikeType strikeType,
            Agent.UsageDirection attackDirection,
            float collisionDistanceOnWeapon, float attackProgress, bool attackIsParried,
            bool isPassiveUsageHit, bool isHeavyAttack, ref float defenderStunPeriod,
            ref float attackerStunPeriod, ref bool crushedThrough, ref bool chamber) {

            if (attackerAgent.WieldedWeapon.Item.StringId.Contains("shard_blade")) {
                Debug.Log("Pre Defend ColissionResult", "collisions_result: " + collisionResult, "Parried:" + attackIsParried);
                defenderStunPeriod = 0f;
                attackerStunPeriod = 0f;
                crushedThrough = true;
                chamber = false;
                defenderAgent.DropItem(defenderAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand), WeaponClass.Undefined);
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch("DecideAgentShrugOffBlow")]
        public static bool DecideAgentShrugOffBlow(
            Agent victimAgent,
            AttackCollisionData collisionData,
            Blow blow,
            ref bool __result

         ){
            // TODO: Change to check if wearing shardplate
            if (victimAgent != null && victimAgent.IsPlayerControlled) {
                __result = true;
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch("CalculateBaseMeleeBlowMagnitude")]
        public static bool CalculateBaseMeleeBlowMagnitude(
            AttackInformation attackInformation,
            MissionWeapon weapon,
            StrikeType strikeType,
            float progressEffect, float impactPointAsPercent, float exraLinearSpeed, bool doesAttackerHaveMount,
            ref float __result
       ) {
            if (weapon.Item != null && weapon.Item.ToString() == "Shard Blade") {
                //Debug.Log(
                //"---- CalculateBaseMeleeBlowMagnitude ---- ",
                //weapon.Item,
                //"VictimAgentAbsorbedDamageRatio: " + attackInformation.VictimAgentAbsorbedDamageRatio,
                //"progressEffect: " + progressEffect,
                //"impactPointAsPercent: " + impactPointAsPercent,
                //"exraLinearSpeed: ", exraLinearSpeed);
                __result = 100f;
                return false;
            }

            return true;
        }



        public static AttackCollisionData SetNotBlocked(AttackCollisionData a) {
            // s = s.replace("_", "")
            // print("\n".join(["a."+ x[0].upper() + x[1:] for x in s.split(" ")[1::2]]))
            return AttackCollisionData.GetAttackCollisionDataForDebugPurpose(
                false, //a.AttackBlockedWithShield,
                false, // a.CorrectSideShieldBlock,
                a.IsAlternativeAttack,
                a.IsColliderAgent,
                a.CollidedWithShieldOnBack,
                a.IsMissile,
                a.MissileBlockedWithWeapon,
                a.MissileHasPhysics,
                a.EntityExists,
                a.ThrustTipHit,
                a.MissileGoneUnderWater,
                a.MissileGoneOutOfBorder,
                CombatCollisionResult.StrikeAgent,
                a.AffectorWeaponSlotOrMissileIndex,
                a.StrikeType,
                a.DamageType,
                10, //a.CollisionBoneIndex,
                BoneBodyPartType.Chest,//a.VictimHitBodyPart,
                a.AttackBoneIndex,
                a.AttackDirection,
                a.PhysicsMaterialIndex,
                CombatHitResultFlags.NormalHit,
                a.AttackProgress,
                a.CollisionDistanceOnWeapon,
                0.0f, // AttackerStunPeriod
                a.DefenderStunPeriod,
                a.MissileTotalDamage,
                a.MissileStartingBaseSpeed,
                a.ChargeVelocity,
                a.FallSpeed,
                a.WeaponRotUp,
                a.WeaponBlowDir,
                a.CollisionGlobalPosition,
                a.MissileVelocity,
                a.MissileStartingPosition,
                a.VictimAgentCurVelocity,
                a.CollisionGlobalNormal
            );
        }


    }

}