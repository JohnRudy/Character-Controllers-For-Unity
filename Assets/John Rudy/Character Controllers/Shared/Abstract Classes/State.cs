using UnityEngine;

namespace JR.Poorman.CharacterControllers.StateMachine {

    public abstract class State : MonoBehaviour {

        #region Unity Callbacks

        // No need to compile each OnValidate method.
#if UNITY_EDITOR
        private void OnValidate ( ) {
            TryGetComponent<StateMachine> ( out machine );
            if ( machine == null ) {
                Debug.LogWarning ( "No state machine on GameObject!" );
            }
        }
#endif

        // Only a statemachine should switch states and enable / disable state components
        private void OnEnable ( ) {
            if ( this != machine.GetCurrentState ) {
                enabled = false;
                Debug.LogWarning ( "Only a State Machine is allowed to enable states!" );
            }
        }

        // Only a statemachine should switch states and enable / disable state components
        private void OnDisable ( ) {
            if ( this == machine.GetCurrentState ) {
                enabled = true;
                Debug.LogWarning ( "Only a State Machine is allowed to disable states!" );
            }
        }
        #endregion

        #region State Machine controlled
        private StateMachine machine;
        public void StateEnter ( ) {
            enabled = true;
            OnStateEnter ( );
        }
        public void StateLeave ( ) {
            enabled = false;
            OnStateExit ( );
        }
        public void InitializeState ( StateMachine machine ) => this.machine = machine;

        #endregion

        #region User Methods
        /// <summary>
        /// void to override once this state has entered a satemachines currentstate.
        /// Acts as  Unitys OnEnable method.
        /// </summary>
        public virtual void OnStateEnter ( ) { }

        /// <summary>
        /// void to override once this state has left a statemachines currentstate.
        /// Acts as Unitys OnDisable method.
        /// </summary>
        public virtual void OnStateExit ( ) { }

        /// <summary>
        /// void to override for Unity Update method.
        /// </summary>
        public virtual void OnStateUpdate ( ) { }

        /// <summary>
        /// void to override for Unity FixedUpdate method.
        /// </summary>
        public virtual void OnStateFixedUpdate ( ) { }

        /// <summary>
        /// void to override for Unity LateUpdate method.
        /// </summary>
        public virtual void OnStateLateUpdate ( ) { }
        #endregion
    }
}
