using JR.Poorman.CharacterControllers.StateMachine;
using JR.Poorman.CharacterControllers.TDPlatformer;
using UnityEngine;

public class TDPGroundState : State {
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDelta;

    private Vector2 move;

    private Vector2 Position { get { return new Vector2 ( transform.position.x , transform.position.y ); } }
    private Vector2 DesiredPos { get { return Position + move * moveSpeed; } }


    private void Awake ( ) => rb = GetComponent<Rigidbody2D> ( );

    public override void OnStateUpdate ( ) {
        base.OnStateUpdate ( );

        move = new Vector2 ( ) {
            x = TDPControlSettings.TDPlatformerInputs.Player.Move.ReadValue<Vector2> ( ).x ,
            y = 0
        };

        transform.position = Vector2.Lerp ( transform.position , DesiredPos , moveDelta * Time.deltaTime );

        bool jump = TDPControlSettings.TDPlatformerInputs.Player.Jump.WasPressedThisFrame ( );

        if ( jump )
            Jump ( );
    }

    private void Jump ( ) => rb.AddForce ( ( Vector2.up * 300 ) + (move * 200 ));
}
