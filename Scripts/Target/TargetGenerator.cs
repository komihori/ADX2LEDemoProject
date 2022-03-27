using UnityEngine;

namespace ADX2LEDemo.Scripts.Target
{
    public class TargetGenerator
    {
        private TargetController targetController;
        private readonly TargetController.Delegate defeatedTarget;
        public TargetGenerator(TargetController.Delegate defeatedTarget)
        {
            this.defeatedTarget = defeatedTarget;
        }
        
        public void GenerateTarget(int hp) {
            GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            targetController=target.GetComponent<TargetController>();
            var instancePos = RandomSpownPos();
            targetController.Init(hp,instancePos,defeatedTarget);
        }
        
        private Vector3 RandomSpownPos() {
            float[] xPos = { Random.Range(30f, 50f), Random.Range(90f, 110f) };
            float[] zPos = { Random.Range(30f, 50f), Random.Range(90f, 110f) };
            return new Vector3(xPos[Random.Range(0, 2)], 1f, zPos[Random.Range(0, 2)]);
        }
    }
}