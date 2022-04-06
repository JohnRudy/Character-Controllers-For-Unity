using System.Linq;
using UnityEngine;

namespace JR.Poorman.CharacterControllers {

    /// <summary>
    /// Ground checking for each 3D character object. 
    /// Using tags because of Layermask bitmask weirdness for collisions. 
    /// </summary>

    public class GroundedCheck3D : MonoBehaviour {

        [Header ( "Raycasting variables" )]
        [Tooltip ( "From what position should the check be started from" )]
        [SerializeField] private Vector3 groundCheckOffset = Vector3.one;
        [Tooltip ( "Which tags are ground" )]
        [SerializeField] private string [ ] groundTags;
        [SerializeField] private bool hitTriggerColliders;
        [Tooltip("Takes into account ground offset")]
        [SerializeField] private float groundDistance = 0.1f;

        [Header ( "Coyote Timer settings" )]
        [SerializeField] private float maxSeconds = 0.15f;
        
        private float coyoteTimer;
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
                Gizmos.DrawRay ( transform.position + groundCheckOffset , Vector3.down * (groundDistance + groundCheckOffset.magnitude));
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
            Ray ray = new Ray ( ) {
                origin = transform.position + groundCheckOffset ,
                direction = Vector3.down
            };

            RaycastHit [ ] hits = Physics.RaycastAll ( ray , (groundDistance + groundCheckOffset.magnitude ));

            if ( hits.Length > 0 ) {
                foreach ( RaycastHit hit in hits ) {
                    if ( groundTags.Contains ( hit.collider.gameObject.tag ) ) {
                        isHitDistance = Vector3.Distance ( hit.point , transform.position + groundCheckOffset ) <= (groundDistance + groundCheckOffset.magnitude) == true ? true : false;
                        break;
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // Note that unity's collision system is atrocious.                                //
        // Kinda like every other Game Engines... This is a failsafe for grounded checks   //
        /////////////////////////////////////////////////////////////////////////////////////

        private void OnCollisionEnter ( Collision collision ) => CollisionHandling ( collision , true );
        private void OnCollisionStay ( Collision collision ) => CollisionHandling ( collision , true );
        private void OnCollisionExit ( Collision collision ) => CollisionHandling ( collision , false );

        private void CollisionHandling ( Collision collision , bool enter ) {
            if ( groundTags.Contains ( collision.gameObject.tag ) ) {
                isColliding = enter;
            }
        }
    }
}