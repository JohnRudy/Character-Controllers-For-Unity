using System.Collections.Generic;
using UnityEngine;

namespace JR.Poorman.CharacterControllers.StateMachine {

    public abstract class StateMachine : MonoBehaviour {
        #region State Machine Variables

        protected List<State> states = new List<State> ( );                 // Used for only enumerating through if a state switch requires it. 
        protected State currentState;                                       // Used for state calls
        [SerializeField] private State startingState;                       // Desired Starting state
        public State GetCurrentState { get { return currentState; } }       // public accessor for other classes
        #endregion


        #region Unity Callbacks 
        private void Start ( ) {
            startingState.InitializeState ( this );
            currentState = startingState;
        }

        // Calling state Update each frame
        private void Update ( ) {
            if ( currentState ) {
                currentState.OnStateUpdate ( );
            }
        }

        // Calling current state each fixed update
        private void FixedUpdate ( ) {
            if ( currentState ) {
                currentState.OnStateFixedUpdate ( );
            }
        }

        // Calling current state each late update
        private void LateUpdate ( ) {
            if ( currentState ) {
                currentState.OnStateLateUpdate ( );
            }
        }
        #endregion

        #region State Switching
        /// <summary>
        /// Takes a given state object and assigns it to be the current state if one is present on the gameobject
        /// </summary>
        /// <param name="state"> State to switch to </param>
        /// <returns> returns true if switch was successfull </returns>
        public bool SwitchState ( State state ) {
            if ( state == currentState ) return true;

            foreach ( State characterState in states ) {
                if ( characterState == state ) {

                    State oldState = currentState;
                    currentState = state;

                    currentState.StateEnter ( );
                    oldState.StateLeave ( );
                    return true;
                }
            }

            if ( currentState != state ) {
                var objStates = gameObject.GetComponents<State> ( );
                if ( states != null && objStates.Length > 0 ) {
                    foreach ( State newState in objStates ) {
                        if ( newState == state ) {
                            states.Add ( state );
                            State oldState = currentState;
                            currentState = state;

                            currentState.StateEnter ( );
                            oldState.StateLeave ( );

                            return true;
                        }
                    }
                }
            }

            // if all fails, then the state was not switched
            Debug.LogWarning ( "State is not on the gameObject. Make sure you have set the state on the same gameobject." );
            return false;
        }

        /// <summary>
        /// Sets the given state type to current state.
        /// </summary>
        /// <typeparam name="StateType"> objecttype to be set as </typeparam>
        /// <returns>returns true once state has been switched </returns>
        public bool SwitchState<StateType> ( ) where StateType : State {
            // enumerating through a list if the state is present 
            foreach ( State state in states ) {
                if ( state is StateType ) {
                    return SwitchState ( state );
                }
            }

            // Finding statetype if one is not in the list
            State c_state = GetComponent<StateType> ( );
            if ( c_state ) {
                c_state.InitializeState ( this );
                return SwitchState ( c_state );
            }

            return false;
        }
        #endregion
    }
}
