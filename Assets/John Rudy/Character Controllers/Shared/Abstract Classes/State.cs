using UnityEngine;


/// <summary>
/// Base Abstract class for all StateMachine states. 
/// and add the new state on the gameobject as a component
/// </summary>


namespace JR.Poorman.CharacterControllers.StateMachine {

    public abstract class State : MonoBehaviour {

        #region Unity Callbacks

        private void OnValidate ( ) {
            TryGetComponent<StateMachine> ( out machine );
            if ( machine == null ) {
                Debug.LogWarning ( "No state machine on GameObject!" );
            }
        }

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

        //////////////////////////////////////////////////////////
        // Always call base.(method)(); before anything else    //
        // The !enabled return is crucial for operation         //
        //////////////////////////////////////////////////////////

        /// <summary>
        /// void to override once this state has entered a satemachines currentstate.
        /// Acts the same as Unitys OnEnable method.
        /// </summary>
        public virtual void OnStateEnter ( ) { if ( !enabled ) return; }

        /// <summary>
        /// void to override once this state has left a statemachines currentstate.
        /// Acts the same as Unitys OnDisable method.
        /// </summary>
        public virtual void OnStateExit ( ) { if ( !enabled ) return; }

        /// <summary>
        /// void to override for Unity Update method.
        /// </summary>
        public virtual void OnStateUpdate ( ) { if ( !enabled ) return; }

        /// <summary>
        /// void to override for Unity FixedUpdate method.
        /// </summary>
        public virtual void OnStateFixedUpdate ( ) { if ( !enabled ) return; }

        /// <summary>
        /// void to override for Unity LateUpdate method.
        /// </summary>
        public virtual void OnStateLateUpdate ( ) { if ( !enabled ) return; }
        #endregion
    }
}
