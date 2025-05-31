using UnityEngine;

public class ProtecterSkill : PlayerHP
{
    public int barrierHP;
    private int _currentbarrierHp;

    protected override void Start()
    {
        _currentbarrierHp = barrierHP;
        
    }

    public override void TakeDamage(int damage)
    {
        _currentbarrierHp -= (damage);
   
        if (_currentbarrierHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
