using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Abstract class for all Character State machines. 
/// Only change stuff here if you know what you are doing.
/// </summary>

namespace JR.Poorman.CharacterControllers.StateMachine {

    public abstract class StateMachine : MonoBehaviour {
        #region State Machine Variables

        protected List<State> states = new List<State> ( );                 // Used for only enumerating through if a state switch requires it. 
        private State currentState;                                         // Used for state calls
        [SerializeField] private State startingState;                       // Desired Starting state
        public State GetCurrentState { get { return currentState; } }       // public accessor for other classes
        #endregion


        #region Unity Callbacks 
        public virtual void Awake ( ) {
            states.Add ( startingState );
            startingState.InitializeState ( this );
            currentState = startingState;
            currentState.StateEnter ( );
        }

        public virtual void OnDisable ( ) => currentState.StateLeave ( );

        // Calling state Update each frame
        public virtual void Update ( ) {
            if ( currentState ) {
                currentState.OnStateUpdate ( );
            }
        }

        // Calling state fixed update each fixedUpdate
        public virtual void FixedUpdate ( ) {
            if ( currentState ) {
                currentState.OnStateFixedUpdate ( );
            }
        }

        // Calling state LateUpdate each LateUpdate
        public virtual void LateUpdate ( ) {
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
        /// <typeparam name="StateType"> object type to be set as </typeparam>
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
