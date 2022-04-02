using System.Collections.Generic;
using UnityEngine;

namespace JR.Poorman.CharacterControllers.StateMachine {
    public class CharacterStateMachine : MonoBehaviour {
        private List<CharacterState> states = new List<CharacterState> ( );
        private CharacterState currentState;

        public CharacterState GetCurrentSate { get { return currentState; } }

        // Updates only the current states update and fixed updates
        private void Update ( ) {
            if ( currentState != null ) {
                currentState.StateUpdate ( );
            }
        }
        private void FixedUpdate ( ) {
            if ( currentState != null ) {
                currentState.StateFixedUpdate ( );
            }
        }

        /// <summary>
        /// Switches the current character state.
        /// </summary>
        /// <param name="state"> The state object to be set as</param>
        /// <returns>Wheter the sate was set</returns>
        public bool SwitchState ( CharacterState state ) {
            // See if we allready have that state in our list.
            foreach ( CharacterState characterState in states ) {
                if ( characterState == state ) {
                    currentState.StateExit ( );
                    currentState = state;
                    currentState.StateEnter ( );
                    return true;
                }
            }

            // If we didn't find the state before.
            // See if the state component is on the gameObject.
            // Add it to the list if so. 
            if ( currentState != state ) {
                var stateComponents = gameObject.GetComponents<CharacterState> ( );
                if ( stateComponents.Length > 0 ) {
                    foreach ( CharacterState characterState in stateComponents ) {
                        if (characterState == state ) {
                            currentState.StateExit ( );
                            currentState = state;
                            currentState.StateEnter ( );
                            states.Add ( characterState );
                            return true;
                        }
                    }
                }
            }

            // if all fails, then the state was not switched
            Debug.LogWarning ( "Stat is not on the gameObject. Make sure you have set the state on the same gameobject." );
            return false;
        }
    }

    [RequireComponent ( typeof( CharacterStateMachine ) )]
    public abstract class CharacterState : MonoBehaviour {
        protected CharacterStateMachine machine;

        public void OnValidate ( ) => machine = GetComponent<CharacterStateMachine> ();

        // State machine calls these methods to enable and disable states
        public void StateEnter ( ) {
            enabled = true;
            OnStateEnter ( );
        }
        public void StateExit ( ) {
            OnStateExit ( );
            enabled = false;
        }

        // Enabled sanity check. User should not switch these. 
        public void OnEnable ( ) {
            if ( this != machine.GetCurrentSate ) {
                enabled = false;
                Debug.LogWarning ( "Only CharacterStateMachine is allowed to enable states!" );
            }
        }
        public void OnDisable ( ) {
            if ( this == machine.GetCurrentSate ) {
                enabled = true;
                Debug.LogWarning ( "Only CharacterStateMachine is allowed to disable states!" );
            }
        }

        // User methods
        public abstract void OnStateEnter ( );
        public abstract void OnStateExit ( );
        public abstract void StateUpdate ( );
        public abstract void StateFixedUpdate ( );
    }
}