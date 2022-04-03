using UnityEngine;

namespace JR.Poorman.CharacterControllers {

    public class GroundedCheck : MonoBehaviour {
        [Header ( "Raycasting variables" )]
        [SerializeField] private bool is2D;
        [Tooltip ( "From what position should the check be started from" )]
        [SerializeField] private Vector3 groundCheckOffset = Vector3.zero;
        [Tooltip ( "How far the check should reach" )]
        [SerializeField] private float groundDistance = 1f;
        [Tooltip ( "What is considered ground" )]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool hitTriggerColliders;
        [Header ( "Coyote Timer settings" )]
        [SerializeField] private float maxSeconds;
        private float coyoteTimer;
        protected bool isGrounded = true;
        public bool IsGrounded { get { return isGrounded; } }

#if UNITY_EDITOR
        [Header ( "Debug" )]
        [SerializeField] private bool debugInInspector;
        private void OnDrawGizmos ( ) {
            if ( debugInInspector ) {
                Gizmos.color = isGrounded == true ? Color.green : Color.red;
                Gizmos.DrawSphere ( transform.position + groundCheckOffset , 0.2f );
                Gizmos.DrawRay ( transform.position + groundCheckOffset , Vector3.down * groundDistance );
            }
        }
#endif

        private void FixedUpdate ( ) {
            bool hit = false;
            if ( is2D ) {
                if ( Physics2D.Raycast ( transform.position + groundCheckOffset , Vector2.down , groundDistance ) ) {
                    hit = true;
                    coyoteTimer = 0;
                }

            }
            else if ( !is2D ) {
                RaycastHit rayHit = new RaycastHit ( );
                Ray ray = new Ray ( ) {
                    origin = transform.position + groundCheckOffset ,
                    direction = Vector3.down
                };

                // Basic raycast ground checking 
                if ( Physics.Raycast ( ray , out rayHit , groundDistance , groundMask , hitTriggerColliders == true ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore ) ) {
                    hit = true;
                    coyoteTimer = 0;
                }
            }

            // Coyote timer. 
            if ( !hit ) {
                coyoteTimer += Time.fixedDeltaTime;
                if ( coyoteTimer >= maxSeconds ) {
                    isGrounded = false;
                }
            }
            else { isGrounded = true; }
        }
    }
}
