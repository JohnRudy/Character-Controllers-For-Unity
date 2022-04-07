using System.Linq;
using UnityEngine;

/// <summary>
/// Ground checking for each 2D character object. 
/// Using tags because of Layermask bitmask weirdness for collisions. 
/// </summary>

namespace JR.Poorman.CharacterControllers {
    public class GroundCheck2D : MonoBehaviour {
        [Tooltip("Which tags are considered ground")]
        [SerializeField] private string [ ] groundTags;
        [Tooltip("Takes into account offset")]
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private Vector2 groundOffset;

        private bool isColliding = false;
        private bool isGroundDistance = false;
        private static bool isGrounded = false;

        public static bool IsGrounded {
            get { return isGrounded; }
        }

        [SerializeField] private bool debugInInspector;

        private void OnDrawGizmos ( ) {
            if ( debugInInspector ) {
                if ( isGrounded ) { Gizmos.color = Color.green; }
                else { Gizmos.color = Color.red; }
                Vector2 startPos = new Vector2 ( transform.position.x , transform.position.y ) + groundOffset;
                Gizmos.DrawSphere ( startPos , 0.2f );
                Gizmos.DrawLine ( startPos , startPos + ( Vector2.down * ( groundOffset.magnitude + groundDistance ) ) );
            }
        }

        private void Update ( ) {
            isGrounded = isColliding && isGroundDistance;

            Vector2 startPos = new Vector2 ( transform.position.x , transform.position.y ) + groundOffset;

            Ray2D ray = new Ray2D ( ) {
                origin = startPos ,
                direction = Vector2.down
            };

            RaycastHit2D [ ] hits = Physics2D.RaycastAll ( ray.origin , ray.direction , groundOffset.magnitude + groundDistance );
            if ( hits.Length > 0 ) {
                foreach ( RaycastHit2D hit in hits ) {
                    if ( groundTags.Contains ( hit.collider.gameObject.tag ) ) {
                        isGroundDistance = true;
                        break;
                    }
                    else { isGroundDistance = false; }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // Note that unity's collision system is atrocious.                                //
        // Kinda like every other Game Engines... This is a failsafe for grounded checks   //
        /////////////////////////////////////////////////////////////////////////////////////

        private void OnCollisionEnter2D ( Collision2D collision ) => CollisionHandling ( collision , true );
        private void OnCollisionExit2D ( Collision2D collision ) => CollisionHandling ( collision , false );
        private void OnCollisionStay2D ( Collision2D collision ) => CollisionHandling ( collision , true );

        private void CollisionHandling ( Collision2D collision , bool enter ) {
            if ( groundTags.Contains ( collision.gameObject.tag ) )
                isColliding = enter;
        }
    }
}