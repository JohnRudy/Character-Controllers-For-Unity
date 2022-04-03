using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JR.Poorman.CharacterControllers.StateMachine;

public class FPInAirState : State {
    public override void OnStateEnter ( ) {
        base.OnStateEnter ( );
        Debug.Log ( "Character has entered In Air state" );
    }

    public override void OnStateExit ( ) {
        base.OnStateEnter ( );
        Debug.Log ( "Character has left In Air state" );
    }

    public override void OnStateFixedUpdate ( ) {
        return;
    }

    public override void OnStateLateUpdate ( ) {
        return;
    }

    public override void OnStateUpdate ( ) {
        return;
    }
}
