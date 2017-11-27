using UnityEngine;
using VisowFrameWork;

public class DefendState : FSMState<VFCharacter, RoleState>
{
    protected string _aniName;

    public override RoleState StateID
    {
        get
        {
            return RoleState.defend;
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        entity.m_playerCtrl.Defend();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

