using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

/// <summary>
/// First Person State Machine
/// Add conditions here to which state to switch to at which times. 
/// Add states as components on the transform
/// </summary>

[RequireComponent ( typeof ( GroundedCheck3D ) )]
public class FPStateMachine : StateMachine {
    private GroundedCheck3D groundedCheck;

    private void Start ( ) => groundedCheck = GetComponent<GroundedCheck3D> ( );

    public override void Update ( ) {
        base.Update ( );

        if ( groundedCheck.IsGrounded && GetCurrentState.GetType ( ) != typeof ( FPGroundedState ) ) {
            SwitchState<FPGroundedState> ( );
        }
        if ( !groundedCheck.IsGrounded && GetCurrentState.GetType ( ) != typeof ( FPInAirState ) ) {
            SwitchState<FPInAirState> ( );
        }
    }
}