using UnityEngine;


/// <summary>
/// First Person camera controller
/// Defaults to null parent and main camera and parent transform if not set
/// 
/// NOTE: 
/// The script also rotates the target transform with the camera for 1:1 rotations
/// Easier like this rather than dividing the script to separate classes
/// </summary>

namespace JR.Poorman.CharacterControllers.FirstPerson {

    public class FPCameraController : MonoBehaviour {
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
        
        private Vector2 look;       // Simple reading of ReadValue of action map
        private float rotX;         // Holds the cameras vertical movement, rotates along the X axis 
        private float rotY;         // Holds the cameras horizontal movement, rotates along the y axis

        // For debugging here. 
        private void OnValidate ( ) {
            if (targetCamera != null && target != null) {
                targetCamera.transform.position = target.transform.position + cameraOffset;
            }      
        }

        private void Awake ( ) {           
            if ( targetCamera == null ) { targetCamera = Camera.main; }
            if (target == null) { target = transform; }
            targetCamera.transform.parent = null;
        }

        private void Update ( ) => CameraLook ( );

        private void CameraLook ( ) {
            look = FPControlSettings.FPActions.Player.Look.ReadValue<Vector2> ( );

            rotX += look.y * FPControlSettings.MouseStrength * ( FPControlSettings.InvertVertical == true ? 1 : -1 );
            rotY += look.x * FPControlSettings.MouseStrength * ( FPControlSettings.InvertHorizontal == true ? -1 : 1 );

            rotX = Mathf.Clamp ( rotX , lowerAngleLimit , upperAngleLimit );

            // Rotating the target transform along the Y axis here as well for better Y rotation with camera and target
            target.transform.localEulerAngles = new Vector3 ( 0 , rotY , 0 );
            
            // Rotation and lerping. 
            // Lerping is smoothed because it's less "jittery" and nicer to look at. 
            targetCamera.transform.localEulerAngles = new Vector3 ( rotX , rotY , 0 );
            targetCamera.transform.position = Vector3.Lerp ( targetCamera.transform.position , target.position + cameraOffset , cameraSmoothing * Time.deltaTime );
        }
    }
}