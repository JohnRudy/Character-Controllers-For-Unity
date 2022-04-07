using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

/// <summary>
/// Example state when on ground
/// </summary>

[RequireComponent ( typeof ( Rigidbody ) )]
public class FPGroundedState : State {
    private Rigidbody rb;
    private float movementSpeed = 10f;
    private float sprintingSpeed = 20f;
    private bool isSprinting;

    private void Awake ( ) => rb = GetComponent<Rigidbody> ( );

    public override void OnStateUpdate ( ) {
        base.OnStateUpdate ( );
        isSprinting = FPControlSettings.FPActions.Player.Sprint.IsPressed ( );
    }

    public override void OnStateFixedUpdate ( ) {
        base.OnStateFixedUpdate ( );
        MovePlayer ( );
    }

    private void MovePlayer ( ) {
        // Single call to read value and then for reading purposes store to floats.
        var move = FPControlSettings.FPActions.Player.Move.ReadValue<Vector2> ( );
        float strafe = move.x;
        float forward = move.y;
        Vector3 direction = new Vector3 ( strafe , 0 , forward ) * ( isSprinting == true ? sprintingSpeed : movementSpeed ) * Time.fixedDeltaTime;
        rb.MovePosition ( rb.transform.TransformDirection ( direction ) + rb.position );
    }

    // This gets called by the new Input system
    private void OnJump ( ) {
        // This is here because it seems a disabled component still gets calls and activates. 
        // Bug? Feature? Oversight? Eitherway, annoying. 
        if ( enabled ) {
            rb.AddForce ( Vector3.up * 250f );
        }
    }
}