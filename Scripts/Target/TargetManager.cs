namespace ADX2LEDemo.Scripts.Target
{
    public class TargetManager
    { 
        //private int diedTargetNum = 0;

        private TargetGenerator targetGenerator;
        
        private const int MaxTargetNum = 4;
        private int nowTargetNum = 0;
        private int weakTarget = 0;

        public void Init(TargetController.Delegate defeatedTarget)
        {
            targetGenerator=new TargetGenerator(defeatedTarget);
        }
        
        private bool CanGenerateTarget => nowTargetNum < MaxTargetNum;
        
        public void AddDestroyTargetNum() {
            nowTargetNum--;
            //diedTargetNum++;
        }
        
        public void TryGenerateTarget()
        {
            if (CanGenerateTarget)
                return;
            
            int nextGenerateTargetHp=0;
            if (weakTarget < 3)
            {
                nextGenerateTargetHp = 1;
                weakTarget++;
            } 
            else 
            {
                nextGenerateTargetHp = 3;
                weakTarget = 0;
            }
            
            targetGenerator.GenerateTarget(nextGenerateTargetHp);
            nowTargetNum++;
        }
        
    }
}