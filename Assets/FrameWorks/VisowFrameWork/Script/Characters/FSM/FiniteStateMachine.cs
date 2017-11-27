using UnityEngine;
using System.Collections.Generic;
using System;

namespace VisowFrameWork
{
    public class FiniteStateMachine<EntityType, TransitionId>
    {
        /// <summary>
        /// ʵ����
        /// </summary>
        private EntityType m_Owner;
        /// <summary>
        /// ��ǰ״̬
        /// </summary>
        private FSMState<EntityType, TransitionId> m_CurrentState;
        /// <summary>
        /// ��һ��״̬
        /// </summary>
        private FSMState<EntityType, TransitionId> m_PreviousState;
        /// <summary>
        /// ȫ��״̬,���ۺ�ʱ�����Է�����״̬
        /// </summary>
        private FSMState<EntityType, TransitionId> m_GlobalState;

        /// <summary>
        /// ״̬�ֵ�
        /// </summary>
        private Dictionary<TransitionId, FSMState<EntityType, TransitionId>> m_StateDic;

        /// <summary>
        /// ������
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
        /// ����
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

        /*����ȫ��״̬*/
        public void GlobalStateEnter()
        {
            m_GlobalState.Enter();
        }

        /*����ȫ��״̬*/
        public void SetGlobalState(FSMState<EntityType, TransitionId> globalState)
        {
            m_GlobalState = globalState;
            m_GlobalState.Enter();
        }

        /*���õ�ǰ״̬*/
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

