using System;

namespace VisowFrameWork
{
    /// <summary>
    /// 状态基类
    /// </summary>
    /// <typeparam name="EntityType">实体类</typeparam>
    /// <typeparam name="TransitionId">转换Id</typeparam>
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
        /// 注册状态
        /// </summary>
        /// <param name="entity"></param>
        public void RegisterState(EntityType entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// 获取转换Id
        /// </summary>
        public virtual TransitionId StateID
        {
            get
            {
                throw new ArgumentException("State ID not spicified in child class");
            }
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void Enter()
        {
            _curTime = 0;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public virtual void Execute()
        {
            _curTime += UnityEngine.Time.deltaTime;
        }

        /// <summary>
        /// 离开状态
        /// </summary>
        public virtual void Exit()
        {
            _curTime = 0;
        }
    }
}

