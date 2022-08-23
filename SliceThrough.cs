//using HarmonyLib;
//using TaleWorlds.MountAndBlade;

//namespace Shards
//{
//    [HarmonyPatch]
//    internal static class SliceThrough
//    {
//        [HarmonyPrefix]
//        [HarmonyPatch(typeof(MissionCombatMechanicsHelper), "GetAttackCollisionResults")]
//        public static void GetAttackCollisionResults(AttackInformation attackInformation,
//            ref bool crushedThrough, ref float momentumRemaining, MissionWeapon attackerWeapon,
//            bool cancelDamage, ref AttackCollisionData attackCollisionData,
//            out CombatLogData combatLog, out int speedBonus) {
//            combatLog = new CombatLogData();
//            speedBonus = 0;

//            //bool is_shard_blade = attackerWeapon.GetModifiedItemName().Contains("Shard Blade");
//            if (attackInformation.AttackerAgentCharacter.IsHero) {
//                momentumRemaining = 1000.0f;
//                crushedThrough = true;
//                //cancelDamage = false;

//                //BoneBodyPartType hitBodyPart = BoneBodyPartType.Head;
//                //attackCollisionData = new AttackCollisionData(
//                //    attackCollisionData.AttackBlockedWithShield, attackCollisionData.CorrectSideShieldBlock, attackCollisionData.IsAlternativeAttack, attackCollisionData.IsColliderAgent, attackCollisionData.CollidedWithShieldOnBack, attackCollisionData.IsMissile,
//                //    attackCollisionData.MissileBlockedWithWeapon, attackCollisionData.MissileHasPhysics, attackCollisionData.EntityExists, attackCollisionData.ThrustTipHit, attackCollisionData.MissileGoneUnderWater, attackCollisionData.MissileGoneOutOfBorder,
//                //    attackCollisionData.CollisionResult, attackCollisionData.AffectorWeaponSlotOrMissileIndex, attackCollisionData.StrikeType, attackCollisionData.DamageType, attackCollisionData.CollisionBoneIndex, hitBodyPart,
//                //    attackCollisionData.AttackBoneIndex, attackCollisionData.AttackDirection, attackCollisionData.PhysicsMaterialIndex, attackCollisionData.CollisionHitResultFlags, attackCollisionData.AttackProgress, attackCollisionData.CollisionDistanceOnWeapon,
//                //    attackCollisionData.AttackerStunPeriod, attackCollisionData.DefenderStunPeriod, attackCollisionData.MissileTotalDamage, attackCollisionData.MissileStartingBaseSpeed, attackCollisionData.ChargeVelocity, attackCollisionData.FallSpeed,
//                //    attackCollisionData.WeaponRotUp, attackCollisionData.WeaponBlowDir, attackCollisionData.CollisionGlobalPosition,
//                //    attackCollisionData.MissileVelocity, attackCollisionData.MissileStartingPosition, attackCollisionData.VictimAgentCurVelocity, attackCollisionData.CollisionGlobalNormal
//                //    );

//            }
//            //_attackBlockedWithShield, _correctSideShieldBlock, _isAlternativeAttack, _isColliderAgent, _collidedWithShieldOnBack, _isMissile, _isMissileBlockedWithWeapon, _missileHasPhysics,
//            //_entityExists, _thrustTipHit, _missileGoneUnderWater, _missileGoneOutOfBorder, collisionResult, affectorWeaponSlotOrMissileIndex, StrikeType, DamageType, CollisionBoneIndex, VictimHitBodyPart,
//            // AttackBoneIndex, AttackDirection, PhysicsMaterialIndex, CollisionHitResultFlags, AttackProgress, CollisionDistanceOnWeapon, AttackerStunPeriod, DefenderStunPeriod, MissileTotalDamage, MissileInitialSpeed,
//            // ChargeVelocity, FallSpeed, WeaponRotUp, _weaponBlowDir, CollisionGlobalPosition, MissileVelocity, MissileStartingPosition, VictimAgentCurVelocity, GroundNormal);


//        }
//    }

//}