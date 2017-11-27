using UnityEngine;
using System.Collections.Generic;
using System;

namespace VisowFrameWork
{
    public class FiniteStateMachine<EntityType, TransitionId>
    {
        /// <summary>
        /// 实体类
        /// </summary>
        private EntityType m_Owner;
        /// <summary>
        /// 当前状态
        /// </summary>
        private FSMState<EntityType, TransitionId> m_CurrentState;
        /// <summary>
        /// 上一个状态
        /// </summary>
        private FSMState<EntityType, TransitionId> m_PreviousState;
        /// <summary>
        /// 全局状态,无论何时都可以发生的状态
        /// </summary>
        private FSMState<EntityType, TransitionId> m_GlobalState;

        /// <summary>
        /// 状态字典
        /// </summary>
        private Dictionary<TransitionId, FSMState<EntityType, TransitionId>> m_StateDic;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="owner"></param>
        public FiniteStateMachine(EntityType owner)
        {
            m_CurrentState = null;
            m_PreviousState = null;
            m_GlobalState = null;
            m_Owner = owner;
            m_StateDic = new Dictionary<TransitionId, FSMState<EntityType, TransitionId>>();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void FSMUpdate()
        {
            if (m_GlobalState != null)
            {
                m_GlobalState.Execute();
            }
            if (m_CurrentState != null)
            {
                m_CurrentState.Execute();
            }
        }

        /*进入全局状态*/
        public void GlobalStateEnter()
        {
            m_GlobalState.Enter();
        }

        /*设置全局状态*/
        public void SetGlobalState(FSMState<EntityType, TransitionId> globalState)
        {
            m_GlobalState = globalState;
            m_GlobalState.Enter();
        }

        /*设置当前状态*/
        public void SetCurrentState(FSMState<EntityType, TransitionId> currentState)
        {
            m_CurrentState = currentState;
            m_CurrentState.Enter();
        }

        public TransitionId GetCurrentState()
        {
            return m_CurrentState.StateID;
        }

        public TransitionId GetPreState()
        {
            if (m_PreviousState != null)
            {
                return m_PreviousState.StateID;
            }
            else
            {
                return m_CurrentState.StateID;
            }
        }


        public void ChangeState(FSMState<EntityType, TransitionId> NewState)
        {
            if (m_CurrentState != NewState)
            {
                m_PreviousState = m_CurrentState;

                if (m_CurrentState != null)
                {
                    m_CurrentState.Exit();
                }

                m_CurrentState = NewState;

                if (m_CurrentState != null)
                {
                    m_CurrentState.Enter();
                }
            }
        }

        public void RevertToPreviousState()
        {
            if (m_PreviousState != null)
            {
                ChangeState(m_PreviousState);
            }
        }

        //Changing state via enum
        public FSMState<EntityType, TransitionId> ChangeState(TransitionId stateID)
        {
            try
            {
                FSMState<EntityType, TransitionId> state = m_StateDic[stateID];
                ChangeState(state);
                return state;
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("There is no State assiciated with that definition");
            }
            return null;
        }


        public FSMState<EntityType, TransitionId> GetState(TransitionId stateID)
        {
            try
            {
                return m_StateDic[stateID];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("There is no State assiciated with that definition");
            }
            return null;
        }

        public FSMState<EntityType, TransitionId> RegisterState(FSMState<EntityType, TransitionId> state)
        {
            state.RegisterState(m_Owner);
            m_StateDic.Add(state.StateID, state);
            return state;
        }

        public void UnregisterState(FSMState<EntityType, TransitionId> state)
        {
            m_StateDic.Remove(state.StateID);

        }
    };
}

