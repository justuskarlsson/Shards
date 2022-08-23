using TaleWorlds.ObjectSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using System.Collections.Generic;
using HarmonyLib;
using System.Diagnostics;

namespace Shards
{
    [HarmonyPatch]
    public class SubModule : MBSubModuleBase
    {


        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            //new Harmony("com.illuvatar.shards").PatchAll(Assembly.GetExecutingAssembly());
            //InitialStateOption option = new("Message",
            //    new TextObject("Message", null),
            //    9990,
            //    () => {
            //        InformationManager.DisplayMessage(
            //            new InformationMessage("Hello World.")
            //        );
            //    },
            //    () => {
            //        return (false, null);
            //    }

            //);
            //Module.CurrentModule.AddInitialStateOption(option);

        }

        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(MissionCombatMechanicsHelper), "CalculateBaseMeleeBlowMagnitude")]
        //private static void CalculateBaseMeleeBlowMagnitude(AttackInformation attackInformation, MissionWeapon weapon,
        //    StrikeType strikeType, float progressEffect, ref float impactPointAsPercent,
        //    ref float exraLinearSpeed, bool doesAttackerHaveMount) 
        //{
        //    Harmony.DEBUG = true;
        //    List<string> buffer = FileLog.GetBuffer(true);

        //    buffer.Add("");
        //    buffer.Add("");
        //    buffer.Add("CalculateBaseMeleeBlowMagnitude");
        //    buffer.Add("Weapon item name: " + weapon.GetModifiedItemName());
        //    buffer.Add("Weapon player victim: " + attackInformation.IsVictimPlayer);
        //    buffer.Add("progressEffect: " + progressEffect);
        //    buffer.Add("impactPointAsPercent: " + impactPointAsPercent);
        //    FileLog.LogBuffered(buffer);
        //    FileLog.FlushBuffer();

        //    Harmony.DEBUG = false;
        //    if (weapon.GetModifiedItemName().Contains("Shard Blade")) {
        //        exraLinearSpeed *= 10.0f;
        //        impactPointAsPercent *= 2f;
        //    }
            
        //}

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MissionCombatMechanicsHelper), "GetAttackCollisionResults")]
        private static void GetAttackCollisionResults(AttackInformation attackInformation,
            bool crushedThrough, ref float momentumRemaining, MissionWeapon attackerWeapon,
            ref bool cancelDamage, ref AttackCollisionData attackCollisionData, CombatLogData combatLog, int speedBonus)
        {
            Harmony.DEBUG = true;
            List<string> buffer = FileLog.GetBuffer(true);

            buffer.Add("");
            buffer.Add("");
            buffer.Add("CalculateBaseMeleeBlowMagnitude");
            buffer.Add("Weapon item name: " + attackerWeapon.GetModifiedItemName());
            buffer.Add("Weapon player victim: " + attackInformation.IsVictimPlayer);
            buffer.Add("momentumRemaining: " + momentumRemaining);
            FileLog.LogBuffered(buffer);
            FileLog.FlushBuffer();

            Harmony.DEBUG = false;
            if (attackerWeapon.GetModifiedItemName().Contains("Shard Blade")) {
                momentumRemaining = 1000f;
                cancelDamage = false;
            }
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }


        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            var harmony = new Harmony("com.Shards.jullinator");

            harmony.PatchAll();
        }

        public override void OnMissionBehaviorInitialize(Mission mission) {
            base.OnMissionBehaviorInitialize(mission);
            //mission.AddMissionBehavior(new ShardsMissionBehavior());
        }
    }

    //public class ShardsMissionBehavior : MissionView {
    //    public override void OnMissionScreenInitialize() {
    //        base.OnMissionScreenInitialize();
    //        var itemRoster = MobileParty.MainParty.ItemRoster;
    //        var shardPlate = MBObjectManager.Instance.GetObject<ItemObject>("shard_plate");

    //        //if (itemRoster.)
    //    }


    //    //public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty) {
    //    //    if (affectorAgent.IsMainAgent) {
    //    //        affectedAgent.Die(blow, Agent.KillInfo.Backstabbed);
    //    //    } else {
    //    //        base.OnScoreHit(affectedAgent, affectorAgent, attackerWeapon, isBlocked, isSiegeEngineHit, blow, collisionData, damagedHp, hitDistance, shotDifficulty);
    //    //    }
    //    //}

    //    public override void OnRegisterBlow(Agent attacker, Agent victim, GameEntity realHitEntity, Blow b, ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon) {
    //        if (attacker.IsMainAgent) {
    //            victim.Die(b, Agent.KillInfo.Backstabbed);
    //            //Mission.MainAgent.WieldedWeapon
    //            b.BaseMagnitude = 5000f;
                
    //            collisionData.BaseMagnitude = 5000f;
    //            //b.WeaponRecord.Velocity *= 2000f;
    //            b.SwingDirection *= 100;
    //        } else {
    //            base.OnRegisterBlow(attacker, victim, realHitEntity, b, ref collisionData, attackerWeapon);
    //        }
    //    }

    //}

    public class ShardsCombatBehavior : MissionMainAgentController
    {
    }


    //public class ShardsBehaviour : CampaignBehaviorBase
    //{
    //    public override void RegisterEvents() {
    //        CampaignEvents.BeforeMissionOpenedEvent.AddNonSerializedListener(this, OnMissionOpened);
    //        //CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this,)
    //    }

    //    public void OnMissionOpened() {
    //        Campaign.Current.SkillLevelingManager
    //    }

    //    public override void SyncData(IDataStore dataStore) {
    //        throw new System.NotImplementedException();
    //    }
    //}
}