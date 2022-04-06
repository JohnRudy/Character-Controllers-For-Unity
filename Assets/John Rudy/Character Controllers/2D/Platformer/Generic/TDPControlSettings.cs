using UnityEngine;

namespace JR.Poorman.CharacterControllers.TDPlatformer {

    public class TDPControlSettings : MonoBehaviour {
        private static TDPlatformerInputActions tdPlatformerInputs;
        public static TDPlatformerInputActions TDPlatformerInputs { get { return tdPlatformerInputs; } }

        private void Awake ( ) => tdPlatformerInputs = new TDPlatformerInputActions ( );
        private void OnEnable ( ) => tdPlatformerInputs.Enable ( );
        private void OnDisable ( ) => tdPlatformerInputs.Disable ( );
    }
}