
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.ObjectSystem;
using TaleWorlds.MountAndBlade;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace Shards
{
    internal class ShardPlateMissionBehaviour : MissionView
    {

        internal class ShardPlate
        {
            public float health = 100f;
            Agent agent;
            public ShardPlate(Agent agent) {
                this.agent = agent;
                Debug.Log("ShardPlate constructor");
                //MatrixFrame frame = agent.AgentVisuals.GetSkeleton().GetBoneEntitialFrame(10);
                //MatrixFrame frame = MatrixFrame.Identity;
                //agent.AgentVisuals.CreateParticleSystemAttachedToBone("storm_light_2", 10, ref frame);
                //ParticleSystem.CreateParticleSystemAttachedToBone("psys_game_missile_flame", )
            }

        };

        List<ShardPlate > plates = new List<ShardPlate>();

        public override void OnAgentBuild(Agent agent, Banner banner) {
            base.OnAgentBuild(agent, banner);

            //Debug.Log("---- Shardplate agent created ----",
            //    Debug.DescribeObject(agent)
            //);
            //Debug.Log("Try add plate, agent null:" + agent == null);

            //if (itemRoster.)
            if ( agent.IsMainAgent ) {
                agent.SetMaximumSpeedLimit(agent.GetMaximumSpeedLimit() * 3, false);
            }
        }


        public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData) {
            base.OnAgentHit(affectedAgent, affectorAgent, affectorWeapon, blow, attackCollisionData);
            if (affectedAgent != null && affectedAgent.IsMainAgent) {
                Debug.Log("Agent hit, try add effects");
                MatrixFrame frame = MatrixFrame.Identity;
                affectedAgent.AgentVisuals.CreateParticleSystemAttachedToBone("storm_light_2", 10, ref frame);
                MaybeAddPlate(affectedAgent);
            }
        }

        public override void OnMissionScreenTick(float dt) {
            if (Mission != null && Mission.MainAgent != null ) {
                Debug.Log("Set Speed Limit");
                Mission.MainAgent.SetMaximumSpeedLimit(400f, false);
            }
            base.OnMissionScreenTick(dt);
        }

        //public override void OnRegisterBlow(Agent attacker, Agent victim, GameEntity realHitEntity, Blow b, ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon) {
        //    if (attackerWeapon.Item != null && attackerWeapon.Item.StringId.Contains("shard_blade") && victim.IsHuman) {
        //        Debug.Log("Removing block");
        //        collisionData = ShardBladePatch.SetNotBlocked(collisionData);
        //        //victim.Die(b, Agent.KillInfo.Musket);
        //        //realHitEntity.AddParticleSystemComponent("storm_light");
        //        //realHitEntity.BurstEntityParticle(true);
        //        b.AbsorbedByArmor = 0f;
        //        b.BlowFlag = BlowFlags.CrushThrough;
        //        b.InflictedDamage = 500;
        //        b.BaseMagnitude = 100f;
        //        b.StrikeType = StrikeType.Swing;
        //        b.DamageType = DamageTypes.Cut;
        //    }
        //    base.OnRegisterBlow(attacker, victim, realHitEntity, b, ref collisionData, attackerWeapon);
        //}

        void MaybeAddPlate(Agent agent) {
            //Debug.Log(Debug.DescribeObject(agent.Equipment));
            //Debug.Log(Debug.DescribeObject(agent.Equipment[EquipmentIndex.Body]));
            //if (agent.Equipment[EquipmentIndex.Body].Item.StringId.Contains("shard_plate")) {
            if (plates.IsEmpty()) { 
                Debug.Log("Added Shard plate");
                plates.Add(new ShardPlate(agent));
            }
        }
    }


}
