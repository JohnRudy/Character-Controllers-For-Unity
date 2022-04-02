using UnityEngine;

namespace JR.Poorman.CharacterControllers.FirstPerson {
    public class FirstPersonCameraController : MonoBehaviour {
        [SerializeField] private Camera cam;
        private GeneratedFirstPersonInputActions firstPersonActions;

        [SerializeField] private Vector3 camerOffset;
        [SerializeField] private float upperLimit, lowerLimit;
        [SerializeField] private bool invertVertical, invertHorizontal;
        [SerializeField, Range ( 0.01f , 2f )] private float mouseStrength = 1f;

        private float rotX;
        private float rotY;

        private void Awake ( ) => firstPersonActions = new GeneratedFirstPersonInputActions ( );
        private void OnEnable ( ) => firstPersonActions.Enable ( );
        private void OnDisable ( ) => firstPersonActions.Disable ( );

        private void Update ( ) {
            // firstPersonActions.Player.Look.ReadValue<Vector2> ( );      
        }
    }
}