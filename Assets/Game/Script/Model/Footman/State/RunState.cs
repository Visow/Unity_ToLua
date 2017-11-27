using UnityEngine;
using VisowFrameWork;

public class RunState : FSMState<VFCharacter, RoleState>
{
    protected string _aniName = "run";

    public float fNextDistance { get; set; }
    public Quaternion fNextDir { get; set; }

    public override RoleState StateID
    {
        get
        {
            return RoleState.run;
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        if(fNextDir != null)
        {
            entity.transform.localRotation = fNextDir;
        }
        entity.m_playerCtrl.DoAnim(_aniName);
        entity.transform.Translate(Vector3.forward * entity.m_MoveSpeedMultiplier * 5 * Time.deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

