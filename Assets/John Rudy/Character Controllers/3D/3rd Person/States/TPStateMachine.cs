using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

/// <summary>
/// Third Person State Machine
/// Add conditions here to which state to switch to at which times. 
/// Add states as components on the transform
/// </summary>

[RequireComponent ( typeof ( GroundCheck3D ) )]
public class TPStateMachine : StateMachine {
    public override void Update ( ) {
        base.Update ( );
        if ( GroundCheck3D.IsGrounded && GetCurrentState.GetType ( ) != typeof ( TPGrounded ) ) {
            SwitchState<TPGrounded> ( );
        }
        if ( !GroundCheck3D.IsGrounded && GetCurrentState.GetType ( ) != typeof ( TPInAir ) ) {
            SwitchState<TPInAir> ( );
        }
    }
}