using UnityEngine;

namespace JR.Poorman.CharacterControllers {

    public class CharacterGroundCheck : MonoBehaviour {

        [SerializeField] private bool debugInInspector;
        [Tooltip ( "From what position should the check be started from" )]
        [SerializeField] private Vector3 groundCheckOffset = Vector3.zero;
        [Tooltip ( "To what direction should the ground check ray go" )]
        [SerializeField] private Vector3 groundDirection;
        [Tooltip ( "How far the check should reach" )]
        [SerializeField] private float groundDistance = 1f;
        [Tooltip ( "What is considered ground" )]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool hitTriggerColliders;

        protected bool isGrounded = true;
        public bool IsGrounded { get { return isGrounded; } }

        private Vector3 groundHitPos;
        public Vector3 GroundHitPosition { get { return groundHitPos; } }

        private void OnValidate ( ) {
            if ( groundDirection.magnitude > 1 ) {
                groundDirection = groundDirection.normalized;
            }
        }

        private void OnDrawGizmos ( ) {
            if ( debugInInspector ) {
                Gizmos.color = isGrounded == true ? Color.green : Color.red;
                Gizmos.DrawSphere ( transform.position + groundCheckOffset , 0.2f );
                Gizmos.DrawRay ( transform.position + groundCheckOffset , groundDirection.normalized * groundDistance );
            }
        }

        private void FixedUpdate ( ) {
            RaycastHit rayHit = new RaycastHit ( );
            Ray ray = new Ray ( ) {
                origin = transform.position + groundCheckOffset ,
                direction = groundCheckOffset
            };

            if ( Physics.Raycast ( ray , out rayHit , groundDistance , groundMask , hitTriggerColliders == true ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore ) ) {
                isGrounded = true;
            }
            else { isGrounded = false; }
        }
    }
}