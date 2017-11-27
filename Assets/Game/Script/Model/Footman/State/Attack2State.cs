using UnityEngine;
using System.Collections;
using VisowFrameWork;

public class Attack2State : FSMState<VFCharacter, RoleState>
{
    protected string _aniName;

    public override RoleState StateID
    {
        get
        {
            return RoleState.attack02;
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

