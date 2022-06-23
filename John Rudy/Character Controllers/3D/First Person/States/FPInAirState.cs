using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;


/// <summary>
/// Example In air state. 
/// Slows the player down once the player is in air and limits movement to forward and backward. 
/// </summary>

[RequireComponent ( typeof ( Rigidbody ) )]
public class FPInAirState : State {

    private Rigidbody rb;
    private float inAirMovement = 1f;

    private void Awake ( ) => rb = GetComponent<Rigidbody> ( );

    public override void OnStateFixedUpdate ( ) {
        base.OnStateFixedUpdate ( );
        InAirMovement ( );
    }

    private void InAirMovement ( ) {
        var move = FPControlSettings.FPActions.Player.Move.ReadValue<Vector2> ( );
        float forward = move.y;
        // Kinda a long and winded way to lerp. It works. 
        Vector3 direction = Vector3.Lerp ( rb.position , rb.position + rb.transform.TransformDirection ( new Vector3 ( 0 , 0 , forward ) ) , inAirMovement * Time.fixedDeltaTime );
        rb.MovePosition ( direction );
    }
}