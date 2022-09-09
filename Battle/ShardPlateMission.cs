
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.ObjectSystem;
using TaleWorlds.MountAndBlade;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using HarmonyLib;

using System;
using static TaleWorlds.CampaignSystem.CharacterDevelopment.DefaultPerks;
using System.Reflection;

namespace Shards.Battle
{
    [HarmonyPatch]
    internal class ShardPlateMissionBehaviour : MissionView
    {
        public static ShardPlateMissionBehaviour? current = null;

        public override void AfterStart()
        {
            base.AfterStart();
            current = this;

        }

        public Dictionary<string, ShardPlate> plates = new Dictionary<string, ShardPlate>();

        public override void OnAgentBuild(Agent agent, Banner banner)
        {
            base.OnAgentBuild(agent, banner);

            if (agent.Name == "Joron")
            {
                //agent.SpawnEquipment[EquipmentIndex.Body] = new )
            }

            if (AgentHasShardPlate(agent))
            {
                Debug.Log("Adding ShardPlate object for: " + agent.Name);
                plates.Add(agent.Name, new ShardPlate(agent));
            }
        }

        public static ShardPlate? TryGetShardPlate(Agent agent)
        {
            if (current == null || !current.plates.ContainsKey(agent.Name))
            {
                return null;
            }
            return current.plates[agent.Name];
        }

        public void ShardKillNonPlate(Agent victim, Blow blow)
        {
            MatrixFrame frame = MatrixFrame.Identity;
            //frame.origin.x = 1f;
            // TODO: PSYS; Dark flame on eyes
            victim.AgentVisuals.CreateParticleSystemAttachedToBone("shard_blade_eyes_burning", (sbyte)HumanBone.Head, ref frame);
            // TODO: Play sound of shardblade slicing / burning soul (Tutorial video)
            Random rnd = new Random();
            int idx = rnd.Next(0, 26);
            Debug.Log("Shardblade killInfo:" + idx + " bone: " + blow.BoneIndex);
            Agent.KillInfo killInfo = (Agent.KillInfo)idx;
            victim.Die(blow, killInfo);
        }

        public void PlateHit(ShardPlate plate, Blow blow)
        {
            PlayPlateSound(plate.agent, blow);
            plate.GetHit(blow);
        }

        public void PlayPlateSound(Agent agent, Blow b)
        {
            //int hitSound = b.WeaponRecord.GetHitSound(isOwnerHumanoid, isCriticalBlow, isLowBlow, isNonTipThrust, b.AttackType, b.DamageType);
            int hitSound = b.WeaponRecord.GetHitSound(true, false, false, false, b.AttackType, b.DamageType);
            float soundParameterForArmorType = 0.1f * (float)ArmorComponent.ArmorMaterialTypes.Plate;

            SoundEventParameter parameter = new SoundEventParameter("Armor Type", soundParameterForArmorType);
            Mission.MakeSound(hitSound, b.Position, soundCanBePredicted: false, isReliable: true, b.OwnerId, agent.Index, ref parameter);
            Mission.AddSoundAlarmFactorToAgents(b.OwnerId, b.Position, 15f);

        }

        public static bool AgentHasShardBlade(Agent agent)
        {
            return 
                agent.WieldedWeapon.Item != null &&
                agent.WieldedWeapon.Item.StringId.Contains("shard_blade")
            ;
        }

        public static bool AgentHasShardPlate(Agent agent)
        {
            EquipmentIndex bodyIdx = EquipmentIndex.Body;
            ItemObject bodyArmor = agent.SpawnEquipment[bodyIdx].Item;
            return bodyArmor != null && bodyArmor.StringId.Contains("shard_plate");
        }

        public override void OnMissionTick(float dt)
        {
            base.OnMissionTick(dt);
            foreach (Agent agent in Mission.AllAgents)
            {
                if (plates.ContainsKey(agent.Name))
                {
                    float scale = Math.Max(0.1f, plates[agent.Name].health / 100f);
                    agent.SetMaximumSpeedLimit(ShardPlate.BASE_SPEED * scale, false);
                    Monster monster = agent.Monster;

                    Traverse.Create(monster).Field("JumpAcceleration").SetValue(scale * 10f); // 6.5f
                    Traverse.Create(monster).Field("JumpSpeedLimit").SetValue(scale * 5f); // 3.5f
                }
                else
                {
                    //Debug.Log(agent.Monster.JumpAcceleration, agent.Monster.JumpSpeedLimit);
                    agent.SetMaximumSpeedLimit(4f, false);
                }
            }


        }

        public override void OnRemoveBehavior()
        {
            Debug.Log("Mission Deactivate");
            foreach (ShardPlate plate in plates.Values)
            {
                plate.Destroy();
            }
            plates.Clear();
            base.OnMissionDeactivate();
        }


    }


}
