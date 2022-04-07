using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;


/// <summary>
/// Third Person Grounded State
/// </summary>

[RequireComponent ( typeof ( Rigidbody ) )]
public class TPGrounded : State {
    private Rigidbody rb;

    private Vector3 moveDir;
    private Vector3 rotDir;
    [SerializeField, Range ( 0.01f , 0.2f )] private float moveSpeed = 0.1f;
    [SerializeField, Range ( 1f , 50f )] private float moveDelta = 2f;
    [SerializeField, Range ( 0.01f , 5f )] private float turnDelta = 3f;

    private void Awake ( ) => rb = GetComponent<Rigidbody> ( );

    public override void OnStateEnter ( ) {
        base.OnStateEnter ( );
        moveDir = Vector3.zero;
    }

    public override void OnStateUpdate ( ) {
        base.OnStateUpdate ( );
        TurnPlayerTowardsLookDirection ( );

        if ( TPControlSettings.ThirdPersonInputActions.Player.Jump.IsPressed ( ) ) {
            rb.AddForce ( ( Vector3.up + (rb.transform.TransformDirection ( Vector3.forward )/2)).normalized * 50 );
        }
    }

    public override void OnStateFixedUpdate ( ) {
        base.OnStateFixedUpdate ( );
        MovePlayer ( );
    }

    private void MovePlayer ( ) {
        moveDir = new Vector3 ( ) {
            x = Mathf.Lerp ( moveDir.x , TPControlSettings.Move.x , moveDelta * Time.fixedDeltaTime ) ,
            y = 0 ,
            z = Mathf.Lerp ( moveDir.z , TPControlSettings.Move.y , moveDelta * Time.fixedDeltaTime )
        };

        moveDir = Vector3.ClampMagnitude ( moveDir , moveSpeed );
        Vector3 desiredPos = rb.transform.TransformDirection ( moveDir ) + rb.transform.position;
        rb.MovePosition ( desiredPos );
    }

    // Smoothly turn to look direction with the least amount of effort.
    private void TurnPlayerTowardsLookDirection ( ) {
        // To not turn without movement
        if ( TPControlSettings.Move.magnitude == 0 ) return;
        rotDir = new Vector3 ( ) {
            x = 0 ,
            y = TPControlSettings.RotY ,
            z = 0
        };

        // Quaternion lerp smooths and takes into account multiple rotations. 
        rb.transform.rotation = Quaternion.Lerp ( transform.rotation , Quaternion.Euler ( rotDir ) , turnDelta * Time.deltaTime );
    }
}