
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
            bool leaking = false;
            Agent agent;
            System.Threading.Timer? psys_thread;

            public ShardPlate(Agent agent) {
                this.agent = agent;
                Debug.Log("ShardPlate constructor");
                //MatrixFrame frame = agent.AgentVisuals.GetSkeleton().GetBoneEntitialFrame(10);

            }

            public void GetHit(Blow blow) {
                MatrixFrame frame = MatrixFrame.Identity;
                agent.AgentVisuals.CreateParticleSystemAttachedToBone("storm_light_hit", blow.BoneIndex, ref frame);
                health -= 5f;
                if (!leaking && health < 75f) {
                    leaking = true;
                    psys_thread = new System.Threading.Timer((x) => {
                        NewParticleSystem();
                    }, null, 0, 5000);
                }
            }

            void NewParticleSystem() {
                if (agent == null) {
                    return;
                }
                if (agent.Health <= 1f) {
                    return;
                }
                Debug.Log("ShardPlate 5s interval callback");
                int level = 75;
                if (health <= 25) {
                    level = 25;
                } else if (health <= 50) {
                    level = 50;
                }
                MatrixFrame frame = MatrixFrame.Identity;
                agent.AgentVisuals.CreateParticleSystemAttachedToBone("storm_light_" + level, 10, ref frame);
            }
        };


        Dictionary<string, ShardPlate> plates = new Dictionary<string, ShardPlate>();

        public override void OnAgentBuild(Agent agent, Banner banner) {
            base.OnAgentBuild(agent, banner);

            EquipmentIndex bodyIdx = EquipmentIndex.Body;
            ItemObject bodyArmor = agent.SpawnEquipment[bodyIdx].Item;
            if ( bodyArmor != null && bodyArmor.StringId.Contains("shard_plate")) {
                Debug.Log("Adding ShardPlate object");
                plates.Add(agent.Name, new ShardPlate(agent));
            }
        }


        public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData) {
            base.OnAgentHit(affectedAgent, affectorAgent, affectorWeapon, blow, attackCollisionData);
            if (affectedAgent != null && plates.ContainsKey(affectedAgent.Name)) {
                Debug.Log("Agent hit, try add effects");
                plates[affectedAgent.Name].GetHit(blow);
            }
        }

        public override void OnMissionScreenTick(float dt) {
            if (Mission != null && Mission.MainAgent != null ) {
                //Debug.Log("Set Speed Limit");
                //Mission.MainAgent.SetMaximumSpeedLimit(100f, false);
                //Mission.MainAgent.SetMinimumSpeed(40f);
                //Mission.MainAgent.SetCurrentActionSpeed()
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


    }


}
