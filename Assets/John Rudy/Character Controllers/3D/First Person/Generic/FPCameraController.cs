using UnityEngine;

namespace JR.Poorman.CharacterControllers.FirstPerson {

    public class FPCameraController : MonoBehaviour {

        //////////////////////////////////////////////////////////////
        // Place these somewhere else 
        private FirstPersonInputActions m_FPActions;        // Unitys new input system action maps generated as C# classes
        //////////////////////////////////////////////////////////////
        
        [Header ( "Development Settings" )]
        [Tooltip("Defaults to Main Camera if not set. Camera should not be parented")]
        [SerializeField] private Camera targetCamera;
        [Tooltip("Defaults to transform this component is placed on")]
        [SerializeField] private Transform target;
        [Tooltip("Placement of the camera from target transform")]
        [SerializeField] private Vector3 cameraOffset;
        [Tooltip("How far up can the player look")]
        [SerializeField] private float upperAngleLimit;
        [Tooltip("How far down can the player look")]
        [SerializeField] private float lowerAngleLimit;
        [Tooltip ( "Smoothing camera movement" )]
        [SerializeField] private float cameraSmoothing;

        /////////////////////////////////////////////////////////////////////////////////////////////
        // Place these somewhere else 
        [Header ( "Player set settings" )]
        [SerializeField] private bool invertVertical;
        [SerializeField] private bool invertHorizontal;
        [SerializeField, Range ( 0.01f , 2f )] private float mouseStrength = 1f;
        /////////////////////////////////////////////////////////////////////////////////////////////
        
        private Vector2 look;       // Simple reading of ReadValue of action map
        private float rotX;         // Holds the cameras vertical movement, rotates along the X axis 
        private float rotY;         // Holds the cameras horizontal movement, rotates along the y axis

        // No need to complie OnValidate methods
#if UNITY_EDITOR
        private void OnValidate ( ) {
            if (targetCamera != null && target != null) {
                targetCamera.transform.position = target.transform.position + cameraOffset;
            }      
        }
#endif

        private void Awake ( ) {
            //////////////////////////////////////////////////////////////
            // Place these somewhere else 
            m_FPActions = new FirstPersonInputActions ( );
            //////////////////////////////////////////////////////////////
            
            if ( targetCamera == null ) { targetCamera = Camera.main; }
            if (target == null) { target = transform; }
            targetCamera.transform.parent = null;
            
        }

        //////////////////////////////////////////////////////////////
        // Place these somewhere else 
        private void OnEnable ( ) => m_FPActions.Enable ( );
        private void OnDisable ( ) => m_FPActions.Disable ( );

        //////////////////////////////////////////////////////////////

        private void Update ( ) => CameraLook ( );

        private void CameraLook ( ) {
            look = m_FPActions.Player.Look.ReadValue<Vector2> ( );

            rotX += look.y * mouseStrength * ( invertVertical == true ? 1 : -1 );
            rotY += look.x * mouseStrength * ( invertHorizontal == true ? -1 : 1 );

            rotX = Mathf.Clamp ( rotX , lowerAngleLimit , upperAngleLimit );

            targetCamera.transform.localEulerAngles = new Vector3 ( rotX , rotY , 0 );

            targetCamera.transform.position = Vector3.Lerp ( targetCamera.transform.position , target.position + cameraOffset , cameraSmoothing * Time.deltaTime );
        }
    }
}