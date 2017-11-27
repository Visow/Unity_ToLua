using UnityEngine;
using VisowFrameWork;

public class IdleState : FSMState<VFCharacter, RoleState>
{
    protected string _aniName = "idle_01";

    public override RoleState StateID
    {
        get
        {
            return RoleState.idle02;
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        entity.m_playerCtrl.DoAnim(_aniName);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

