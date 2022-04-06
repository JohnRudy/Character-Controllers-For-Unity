using JR.Poorman.CharacterControllers.StateMachine;
using JR.Poorman.CharacterControllers.ThirdPerson;
using UnityEngine;

[RequireComponent ( typeof ( Rigidbody ) )]
public class TPGrounded : State {
    private TPSpringCameraController tpspringCameraController;
    private Rigidbody rb;

    private float rotY;
    private Vector2 look;
    private Vector2 move;
    private Vector3 moveDir;

    [SerializeField, Range ( 0.01f , 0.2f )] private float moveSpeed = 0.1f;
    [SerializeField, Range ( 1f , 50f )] private float moveDelta = 2f;
    [SerializeField, Range ( 0.01f, 5f )] private float turnDelta = 3f;

    private void Awake ( ) {
        tpspringCameraController = GetComponent<TPSpringCameraController> ( );
        rb = GetComponent<Rigidbody> ( );
    }

    public override void OnStateUpdate ( ) {
        base.OnStateUpdate ( );
        GetValues ( );
        TurnPlayerTowardsLookDirection ( );
    }

    public override void OnStateFixedUpdate ( ) {
        base.OnStateFixedUpdate ( );
        MovePlayer ( );
    }

    private void GetValues ( ) {
        look = TPControlSettings.ThirdPersonInputActions.Player.Look.ReadValue<Vector2> ( );
        move = TPControlSettings.ThirdPersonInputActions.Player.Move.ReadValue<Vector2> ( );
        rotY += look.x * TPControlSettings.MouseStregth * ( TPControlSettings.InvertHorizontal == true ? -1 : 1 );
    }

    private void MovePlayer ( ) {
        moveDir = new Vector3 ( ) {
            x = Mathf.Lerp ( moveDir.x , move.x , moveDelta * Time.fixedDeltaTime ) ,
            y = 0 ,
            z = Mathf.Lerp ( moveDir.z , move.y , moveDelta * Time.fixedDeltaTime )
        };

        moveDir = Vector3.ClampMagnitude ( moveDir , moveSpeed );
        Vector3 desiredPos = rb.transform.TransformDirection ( moveDir ) + rb.transform.position;
        rb.MovePosition ( desiredPos );
    }

    // Smoothly turn to look direction with the least amount of effort.
    private void TurnPlayerTowardsLookDirection ( ) {
        // To not turn without movement
        if ( move.magnitude == 0 ) return; 
        Vector3 rotDir = new Vector3 ( ) {
            x = 0 ,
            y = rotY ,
            z = 0
        };

        // Quaternion lerp smooths and takes into account multiple rotations. 
        rb.transform.rotation =  Quaternion.Lerp(transform.rotation, Quaternion.Euler ( rotDir ), turnDelta * Time.deltaTime); 
    }
}