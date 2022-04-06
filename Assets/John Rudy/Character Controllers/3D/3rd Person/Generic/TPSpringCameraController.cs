using System.Linq;
using UnityEngine;


/// <summary>
/// Third person camera controller with "spring arm" type behaviour
/// </summary>

namespace JR.Poorman.CharacterControllers.ThirdPerson {

    public class TPSpringCameraController : MonoBehaviour {
        
        [SerializeField] private Camera cam;
        [SerializeField] private Transform target;
        [Header("Camera follower values")]
        [SerializeField] private Vector3 targetOffset;
        [SerializeField] private Vector3 camDesiredPos;
        [SerializeField] private float maxCamDistance = 2;
        [SerializeField] private string [ ] ignoreTagged;
        [SerializeField] private float upperAngleLimit;
        [SerializeField] private float lowerAngleLimit;

        private float rotX;
        private float rotY;

        private Vector3 look;
        private Vector3 desiredPos;
        private Vector3 targetPos;

        [Header("Debug")]
        [SerializeField] private bool debugInInspector;

        private void OnDrawGizmos ( ) {
            if ( debugInInspector ) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere ( target.position + targetOffset , 0.2f );
            }
        }

        private void OnValidate ( ) {
            if ( cam != null && target != null ) {
                cam.transform.position = target.position + targetOffset + camDesiredPos;
                cam.transform.position = Vector3.ClampMagnitude ( cam.transform.position , maxCamDistance );
            }
        }

        private void Awake ( ) {
            if ( cam == null ) cam = Camera.main;
            if ( target == null ) target = transform;

            // Initial Rotation of X axis so that the start isnt as jerky.
            rotX = cam.transform.localEulerAngles.x;
        }

        private void Update ( ) {
            GetValues ( );
            RotateAroundAndFollowTarget ( );
            RotateCamera ( );
        }

        private void GetValues ( ) {
            look = TPControlSettings.ThirdPersonInputActions.Player.Look.ReadValue<Vector2> ( );
            rotX += look.y * TPControlSettings.MouseStregth * ( TPControlSettings.InvertHorizontal == true ? 1 : -1 );
            rotY += look.x * TPControlSettings.MouseStregth * ( TPControlSettings.InvertHorizontal == true ? -1 : 1 );
            targetPos = target.position + targetOffset;
        }

        private void RotateAroundAndFollowTarget ( ) {
            // The desired position rotation offset 
            desiredPos = Quaternion.AngleAxis ( rotY , Vector3.up ) * camDesiredPos;
            desiredPos = Vector3.ClampMagnitude ( desiredPos , maxCamDistance );
            desiredPos += targetPos;

            //Is location valid. No object in between target and camera
            SpringArm ( );

            cam.transform.position = desiredPos;
        }

        private void SpringArm ( ) {
            bool isHit = false;
            Vector3 newPos = Vector3.zero;

            Vector3 dir = desiredPos - targetPos;
            Ray ray = new Ray ( ) {
                origin = targetPos ,
                direction = dir
            };
            RaycastHit [ ] hits = Physics.RaycastAll ( ray , maxCamDistance );

            foreach ( RaycastHit hit in hits ) {
                if ( !ignoreTagged.Contains ( hit.collider.gameObject.tag ) ) {
                    isHit = true;
                    newPos = hit.point;
                    break;
                }
            }

            if ( isHit ) {  
                // A small magic number to not clip through objects
                desiredPos = newPos.normalized * (newPos.magnitude * 0.9f);
            }
        }

        // Affects only camera localEulerAngles. 
        private void RotateCamera ( ) {
            // Rotate cameras local X and Y to "keep looking at target"
            rotX = Mathf.Clamp ( rotX , lowerAngleLimit , upperAngleLimit );
            cam.transform.localEulerAngles = new Vector3 ( rotX , rotY , 0 );
        }
    }
}