using JR.Poorman.CharacterControllers.StateMachine;
using UnityEngine;

namespace JR.Poorman.CharacterControllers.FirstPerson {

    [RequireComponent ( typeof ( CapsuleCollider ) , typeof ( Rigidbody ) )]
    [RequireComponent ( typeof ( CharacterGroundCheck ) , typeof ( CharacterStateMachine ) )]
    [RequireComponent ( typeof ( FirstPersonCameraController ) )]

    public class FirstPersonCharacterController : MonoBehaviour {
        private Rigidbody rb;
        private CapsuleCollider capsuleCollider;
        private CharacterGroundCheck characterGroundCheck;
        private CharacterStateMachine characterStateMachine;
        private FirstPersonCameraController firstPersonCameraController;
        private GeneratedFirstPersonInputActions firstPersonInputActions;

        private void Awake ( ) {
            rb = GetComponent<Rigidbody> ( );
            capsuleCollider = GetComponent<CapsuleCollider> ( );
            characterGroundCheck = GetComponent<CharacterGroundCheck> ( );
            characterStateMachine = GetComponent<CharacterStateMachine> ( );
            firstPersonCameraController = GetComponent<FirstPersonCameraController> ( );
            firstPersonInputActions = new GeneratedFirstPersonInputActions ( );
        }

        private void OnEnable ( ) => firstPersonInputActions.Enable ( );
        private void OnDisable ( ) => firstPersonInputActions.Disable ( );

        private void Update ( ) {
            // Read values from firstPersonInputActions
        }
    }
}
