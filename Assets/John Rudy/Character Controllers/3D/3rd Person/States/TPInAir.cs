using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JR.Poorman.CharacterControllers.StateMachine;

public class TPInAir : State
{
    private Rigidbody rb;
    private Vector3 moveDir;
    [SerializeField, Range ( 0.01f , 0.2f )] private float moveSpeed = 0.1f;
    [SerializeField, Range ( 1f , 50f )] private float moveDelta = 10f;

    private void Awake ( ) => rb = GetComponent<Rigidbody>();

    public override void OnStateUpdate ( ) {
        base.OnStateUpdate ( );
        //MovePlayer ( );
    }

    private void MovePlayer ( ) {
        moveDir = new Vector3 ( ) {
            x = Mathf.Lerp ( moveDir.x , TPControlSettings.Move.x, moveDelta * Time.fixedDeltaTime ) ,
            y = 0 ,
            z = Mathf.Lerp ( moveDir.z , TPControlSettings.Move.y , moveDelta * Time.fixedDeltaTime )
        };

        moveDir = Vector3.ClampMagnitude ( moveDir , moveSpeed );
        Vector3 desiredPos = rb.transform.TransformDirection ( moveDir ) + rb.transform.position;
        rb.MovePosition ( desiredPos );
    }
}
