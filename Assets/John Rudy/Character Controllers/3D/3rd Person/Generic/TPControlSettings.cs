using JR.Poorman.CharacterControllers.ThirdPerson;
using UnityEngine;

public class TPControlSettings : MonoBehaviour {
    private static ThirdPersonInputActions tpInputActions;


    [Range ( 0.01f , 2f ), SerializeField] private float mouseStrength = 0.3f;
    [SerializeField] private bool invertHorizontal = false;
    [SerializeField] private bool invertVertical = false;

    private static bool staticInvertHorizontal;
    private static bool staticInvertVertical;
    private static float staticMouseStrength;

    public static float MouseStregth { get { return staticMouseStrength; } }
    public static bool InvertVertical { get { return staticInvertVertical; } }
    public static bool InvertHorizontal { get { return staticInvertHorizontal; } }
    public static ThirdPersonInputActions ThirdPersonInputActions {
        get { return tpInputActions; }
    }

    private void OnValidate ( ) {
        staticInvertVertical = invertVertical;
        staticInvertHorizontal = invertHorizontal;
        staticMouseStrength = mouseStrength;
    }

    private void Awake ( ) => tpInputActions = new ThirdPersonInputActions ( );

    private void OnEnable ( ) => tpInputActions.Enable ( );
    private void OnDisable ( ) => tpInputActions.Disable ( );

    public static Vector2 Look { get { return tpInputActions.Player.Look.ReadValue<Vector2> ( ); } }
    public static Vector2 Move { get { return tpInputActions.Player.Move.ReadValue<Vector2> ( ); } }

    // Storing here for easier handling between states
    private static float rotY;
    public static float RotY { get { return rotY; } }
    private void Update ( ) => rotY += Look.x * MouseStregth * ( InvertHorizontal == true ? -1 : 1 );
}
