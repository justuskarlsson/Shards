
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace Shards
{
    internal class ShardPlate
    {
        public float health = 100f;
        public static float BASE_SPEED = 9f;
        public Agent agent;
        bool leaking = false;
        System.Threading.Timer? psys_thread;

        public ShardPlate(Agent agent) {
            this.agent = agent;
            //MatrixFrame frame = agent.AgentVisuals.GetSkeleton().GetBoneEntitialFrame(10);

        }

        public void GetHit(Blow blow) {
            Debug.Log("Shardplate hit, inflicted damage:" + blow.InflictedDamage);
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
            Debug.Log("Stormlight leak 5s");
            if (this == null || agent == null || agent.Health <= 1f) {
                return;
            }

            int level = 75;
            if (health <= 25) {
                level = 25;
            } else if (health <= 50) {
                level = 50;
            }
            MatrixFrame frame = MatrixFrame.Identity;
            agent.AgentVisuals.CreateParticleSystemAttachedToBone("storm_light_" + level, 10, ref frame);
        }

        public void Destroy() {
            psys_thread?.Dispose();
        }
    }
}
