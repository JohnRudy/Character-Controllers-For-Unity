using JR.Poorman.CharacterControllers;
using JR.Poorman.CharacterControllers.StateMachine;

/// <summary>
/// 2DPlatformer state machine
/// </summary>

public class TDPStateMachine : StateMachine {
    public override void Update ( ) {
        base.Update ( );

        if ( GroundCheck2D.IsGrounded && GetCurrentState.GetType ( ) != typeof ( TDPGroundState ) ) {
            SwitchState<TDPGroundState> ( );
        }
        if ( !GroundCheck2D.IsGrounded && GetCurrentState.GetType ( ) != typeof ( TDPInAirState ) ) {
            SwitchState<TDPInAirState> ( );
        }
    }
}
