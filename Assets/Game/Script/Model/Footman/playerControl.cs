using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour 
{
	public Animator anim;
	public int attack01;
    public int attack02;
    public int attack03;
    public int battleWalkBackward;
    public int battleWalkForward;
    public int battleWalkLeft;
    public int battleWalkRight;
    public int defend;
    public int die;
    public int getHit;
    public int idle02;
    public int jump;
    public int walk;
    public int taunt;
    public int run;
    public int idle01;

    public int m_cur;
    public int Current
    {
        get{
            return m_cur;
        }
        set{
            m_cur = value;
            anim.SetTrigger(value);
        }
    }

	void Awake () 
	{
		anim = GetComponent<Animator>();
		attack01 = Animator.StringToHash("attack_01");
		attack02 = Animator.StringToHash("attack_02");
		attack03 = Animator.StringToHash("attack_03");
		battleWalkBackward = Animator.StringToHash("walkBattleBackward");
		battleWalkForward = Animator.StringToHash("walkBattleForward");
		battleWalkLeft = Animator.StringToHash("walkBattleLeft");
		battleWalkRight = Animator.StringToHash("walkBattleRight");
		defend = Animator.StringToHash("defend");
		die = Animator.StringToHash("die");
		getHit = Animator.StringToHash("getHit");
		idle02 = Animator.StringToHash("idle_02");
		jump = Animator.StringToHash("jump");
		walk = Animator.StringToHash("walk");
		taunt = Animator.StringToHash("taunt");
		run = Animator.StringToHash("run");
        idle01 = Animator.StringToHash("idle_01");
	}
	

	public void Attack01 ()
	{
        Current = attack01;
		anim.SetTrigger(attack01);
	}

	public void Attack02 ()
	{
        Current = attack02;
		anim.SetTrigger(attack02);
	}

	public void Attack03 ()
	{
        Current = attack03;
		anim.SetTrigger(attack03);
	}

	public void BattleWalkBackward ()
	{
        Current = battleWalkBackward;
		anim.SetTrigger(battleWalkBackward);
	}

	public void BattleWalkForward ()
	{
        Current = battleWalkForward;
		anim.SetTrigger(battleWalkForward);
	}

	public void BattleWalkLeft ()
	{
        Current = battleWalkLeft;
		anim.SetTrigger(battleWalkLeft);
	}

	public void BattleWalkRight ()
	{
        Current = battleWalkRight;
		anim.SetTrigger(battleWalkRight);
	}

	public void Defend ()
	{
        Current = defend;
		anim.SetTrigger(defend);
	}

	public void Die ()
	{
        Current = die;
		anim.SetTrigger(die);
	}

	public void GetHit ()
	{
        Current = getHit;
		anim.SetTrigger(getHit);
	}

	public void Idle02 ()
	{
        Current = idle02;
		anim.SetTrigger(idle02);
	}

    public void Idle01()
    {
        Current = idle01;
        anim.SetTrigger(idle01);
    }

	public void Jump ()
	{
        Current = jump;
		anim.SetTrigger(jump);
	}

	public void Walk ()
	{
        Current = walk;
		anim.SetTrigger(walk);
	}

	public void Taunt ()
	{
        Current = taunt;
		anim.SetTrigger(taunt);
	}

	public void Run ()
	{
        Current = run;
		anim.SetTrigger(run);
	}

    public void DoAnim(string name)
    {
        anim.Play(name);
    }
	
}
