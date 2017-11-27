using UnityEngine;
using VisowFrameWork;

public class Attack1State : FSMState<VFCharacter, RoleState>
{

    public override RoleState StateID
    {
        get
        {
            return RoleState.attack01;
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

