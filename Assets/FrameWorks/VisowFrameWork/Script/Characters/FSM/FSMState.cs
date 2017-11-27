using System;

namespace VisowFrameWork
{
    /// <summary>
    /// ״̬����
    /// </summary>
    /// <typeparam name="EntityType">ʵ����</typeparam>
    /// <typeparam name="TransitionId">ת��Id</typeparam>
    public class FSMState<EntityType, TransitionId>
    {
        protected EntityType entity;
        protected float _curTime;

        public FSMState(FiniteStateMachine<EntityType, TransitionId> fsm)
        {
            fsm.RegisterState(this);
        }

        public FSMState()
        { 
        
        }

        /// <summary>
        /// ע��״̬
        /// </summary>
        /// <param name="entity"></param>
        public void RegisterState(EntityType entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// ��ȡת��Id
        /// </summary>
        public virtual TransitionId StateID
        {
            get
            {
                throw new ArgumentException("State ID not spicified in child class");
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public virtual void Enter()
        {
            _curTime = 0;
        }

        /// <summary>
        /// ִ��
        /// </summary>
        public virtual void Execute()
        {
            _curTime += UnityEngine.Time.deltaTime;
        }

        /// <summary>
        /// �뿪״̬
        /// </summary>
        public virtual void Exit()
        {
            _curTime = 0;
        }
    }
}

