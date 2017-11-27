using UnityEngine;
using System.Collections;
using VisowFrameWork;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class VFCharacter : MonoBehaviour
{
    [Range(1f, 4f)]
    [SerializeField]
    public float m_GravityMultiplier = 2f;

    [SerializeField]
    public float m_MoveSpeedMultiplier = 1f;
    [SerializeField]
    public float m_AnimSpeedMultiplier = 1f;
    [SerializeField]
    public float m_GroundCheckDistance = 0.1f;

    Rigidbody m_Rigidbory;
    Animator m_Animator;
    public playerControl m_playerCtrl;

    FiniteStateMachine<VFCharacter, RoleState> m_fsm;
    Attack1State m_attack1State;
    Attack2State m_attack2State;
    Attack3State m_attack3State;
    BattleWalkBackwardState m_battleWalkBackwardState;
    BattleWalkForwardState m_battleWalkForwardState;
    BattleWalkLeftState m_battleWalkLeftState;
    BattleWalkRightState m_battleWalkRightState;
    DefendState m_defineState;
    DieState m_dieState;
    HitState m_hitState;
    IdleState m_idleState;
    JumpState m_jumpState;
    RunState m_runState;
    WalkState m_walkState;
    TauntState m_tauntState;
    
    // Use this for initialization
    void Start()
    {
        m_Rigidbory = GetComponent<Rigidbody>();
        m_playerCtrl = GameObject.Find("footman").transform.GetComponent<playerControl>();
        m_Animator = m_playerCtrl.anim;
        MakeFSM();
    }

    void MakeFSM()
    {
        m_fsm = new FiniteStateMachine<VFCharacter, RoleState>(this);
        m_attack1State = new Attack1State(); m_fsm.RegisterState(m_attack1State);
        m_attack2State = new Attack2State(); m_fsm.RegisterState(m_attack2State);
        m_attack3State = new Attack3State(); m_fsm.RegisterState(m_attack3State);
        m_battleWalkBackwardState = new BattleWalkBackwardState(); m_fsm.RegisterState(m_battleWalkBackwardState);
        m_battleWalkForwardState = new BattleWalkForwardState(); m_fsm.RegisterState(m_battleWalkForwardState);
        m_battleWalkLeftState = new BattleWalkLeftState(); m_fsm.RegisterState(m_battleWalkLeftState);
        m_battleWalkRightState = new BattleWalkRightState(); m_fsm.RegisterState(m_battleWalkRightState);
        m_defineState = new DefendState(); m_fsm.RegisterState(m_defineState);
        m_dieState = new DieState(); m_fsm.RegisterState(m_dieState);
        m_hitState = new HitState(); m_fsm.RegisterState(m_hitState);
        m_idleState = new IdleState(); m_fsm.RegisterState(m_idleState);
        m_jumpState = new JumpState(); m_fsm.RegisterState(m_jumpState);
        m_runState = new RunState(); m_fsm.RegisterState(m_runState);
        m_walkState = new WalkState(); m_fsm.RegisterState(m_walkState);
        m_tauntState = new TauntState(); m_fsm.RegisterState(m_tauntState);

        m_fsm.ChangeState(RoleState.idle02);
    }

    public void ChangeStatus(RoleState state)
    {
        if (m_fsm.GetCurrentState() == state)
            return;
        m_fsm.ChangeState(state);
    }

    public void Move(Quaternion Dir)
    {

        ChangeStatus(RoleState.run);
    }

    // Update is called once per frame
    void Update()
    {
        m_fsm.FSMUpdate();
    }
}
