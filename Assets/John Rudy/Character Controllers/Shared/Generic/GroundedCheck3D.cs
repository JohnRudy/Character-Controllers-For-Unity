using UnityEngine;
using System.Linq;

namespace JR.Poorman.CharacterControllers {

    /// <summary>
    /// Ground checking for each 3D character object. 
    /// Using tags because of Layermask bitmask weirdness for collisions. 
    /// </summary>

    public class GroundedCheck3D : MonoBehaviour {

        [Header ( "Raycasting variables" )]
        [Tooltip ( "From what position should the check be started from" )]
        [SerializeField] private Vector3 groundCheckOffset = Vector3.zero;
        [Tooltip ( "What is considered ground" )]
        [SerializeField] private string[] groundTags;
        [SerializeField] private bool hitTriggerColliders;
        [SerializeField] private float groundDistance = 0.1f;

        [Header ( "Coyote Timer settings" )]
        [SerializeField] private float maxSeconds;

        [SerializeField] private float coyoteTimer;

        private bool isColliding;
        private bool isHitDistance;
        private bool isGrounded = false;

        public bool IsGrounded { get { return isGrounded; } }

        [Header ( "Debug" )]
        [SerializeField] private bool debugInInspector;

        // For debugging
        private void OnDrawGizmos ( ) {
            if ( debugInInspector ) {
                Gizmos.color = isGrounded == true ? Color.green : Color.red;
                Gizmos.DrawSphere ( transform.position + groundCheckOffset , 0.2f );
                Gizmos.DrawRay ( transform.position + groundCheckOffset , Vector3.down * groundDistance );
            }
        }

        private void Update ( ) {
            if ( !isHitDistance ) {
                coyoteTimer += Time.deltaTime;
            }
            else { coyoteTimer = 0; }

            if ( isHitDistance && isColliding && coyoteTimer < maxSeconds ) {
                isGrounded = true;
            }
            else { isGrounded = false; }

        }

        private void FixedUpdate ( ) {
            if ( Physics.Raycast ( transform.position + groundCheckOffset , Vector3.down , out RaycastHit hit , Mathf.Infinity ) ) {
                isHitDistance = Vector3.Distance ( hit.point , transform.position + groundCheckOffset ) <= groundDistance + groundCheckOffset.magnitude == true ? true : false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // Note that unity's collision system is atrocious.                                //
        // Kinda like every other Game Engines... This is a failsafe for grounded checks   //
        /////////////////////////////////////////////////////////////////////////////////////
        
        private void OnCollisionEnter ( Collision collision ) => CollisionHandling ( collision, true);
        private void OnCollisionStay ( Collision collision ) => CollisionHandling ( collision , true );
        private void OnCollisionExit ( Collision collision ) => CollisionHandling ( collision , false );

        private void CollisionHandling ( Collision collision, bool enter ) {
            if ( groundTags.Contains(collision.gameObject.tag) ) {
                isColliding = enter;
            }
        }
    }
}