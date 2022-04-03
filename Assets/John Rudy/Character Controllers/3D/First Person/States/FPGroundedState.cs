using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

public class FPGroundedState : State {

    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintingSpeed;

    public override void OnStateEnter ( ) {
        base.OnStateEnter ( );
        Debug.Log ( "Character has entered Grounded state" );
    }

    public override void OnStateExit ( ) {
        base.OnStateEnter ( );
        Debug.Log ( "Character has left Grounded state" );
    }

    public override void OnStateFixedUpdate ( ) {
        return;
    }
}