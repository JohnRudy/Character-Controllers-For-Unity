using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

[RequireComponent ( typeof ( GroundedCheck3D ) )]
public class TPStateMachine : StateMachine {
    private GroundedCheck3D groundedCheck;

    public override void Awake ( ) {
        base.Awake ( );
        groundedCheck = GetComponent<GroundedCheck3D> ( );
    }

    public override void Update ( ) {
        base.Update ( );
        if ( groundedCheck.IsGrounded && GetCurrentState.GetType ( ) != typeof ( TPGrounded ) ) {
            SwitchState<TPGrounded> ( );
        }
        if ( groundedCheck.IsGrounded && GetCurrentState.GetType ( ) != typeof ( TPInAir ) ) {
            SwitchState<TPInAir> ( );
        }
    }
}