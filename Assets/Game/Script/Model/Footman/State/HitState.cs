﻿using UnityEngine;
using VisowFrameWork;

public class HitState : FSMState<VFCharacter, RoleState>
{
    protected string _aniName;

    public override RoleState StateID
    {
        get
        {
            return RoleState.getHit;
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

