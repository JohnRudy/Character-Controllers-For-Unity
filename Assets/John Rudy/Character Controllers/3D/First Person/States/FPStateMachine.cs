using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

[RequireComponent ( typeof ( GroundedCheck ) )]
public class FPStateMachine : StateMachine {
    private GroundedCheck groundedCheck;

    private void OnEnable ( ) => groundedCheck = GetComponent<GroundedCheck> ( );

    private void Update ( ) {
        if ( groundedCheck.IsGrounded && !currentState.Equals ( typeof ( FPGroundedState ) ) ) {
            SwitchState<FPGroundedState> ( );
        }

        if ( !groundedCheck.IsGrounded && !currentState.Equals ( typeof ( FPGroundedState ) ) ) {
            SwitchState<FPInAirState> ( );
        }
    }
}