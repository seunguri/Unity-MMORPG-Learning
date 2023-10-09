using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] protected int _exp;
    [SerializeField] protected int _gold;

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
            }

            if (level != Level)
            {
                Level = level;
                SetStat(Level);
            }
        }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _moveSpeed = 5.0f;
        _exp = 0;
        _gold = 0;

        SetStat(_level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat _stat = dict[level];

        _hp = _stat.maxHp;
        _maxHp = _stat.maxHp;
        _attack = _stat.attack;
        _defense = _stat.defense;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
