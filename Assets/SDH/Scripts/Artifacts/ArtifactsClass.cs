using UnityEngine;

public class ArtifactsClass // 아티팩트의 효과들
{
    public class DefenseUp
    {
        public void OnAbility(PlayerMove playerMove)
        {
            
        }
    }
}

public abstract class ArtifactTemplate
{
    public virtual void OnAbility()
    {
        //
    }
}