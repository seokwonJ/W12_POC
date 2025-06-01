using UnityEngine;

public class ProtecterSkill : PlayerHP
{
    private int _currentbarrierHp = 100;

    protected override void Start()
    {
    }

    public void Init(int barrierHp)
    {
        _currentbarrierHp = barrierHp;
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
