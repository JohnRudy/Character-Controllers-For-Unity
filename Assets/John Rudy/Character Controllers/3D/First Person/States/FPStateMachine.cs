using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

/// <summary>
/// First Person State Machine
/// Add conditions here to which state to switch to at which times. 
/// Add states as components on the transform
/// </summary>

[RequireComponent ( typeof ( GroundCheck3D ) )]
public class FPStateMachine : StateMachine {
    public override void Update ( ) {
        base.Update ( );

        if ( GroundCheck3D.IsGrounded && GetCurrentState.GetType ( ) != typeof ( FPGroundedState ) ) {
            SwitchState<FPGroundedState> ( );
        }
        if ( !GroundCheck3D.IsGrounded && GetCurrentState.GetType ( ) != typeof ( FPInAirState ) ) {
            SwitchState<FPInAirState> ( );
        }
    }
}