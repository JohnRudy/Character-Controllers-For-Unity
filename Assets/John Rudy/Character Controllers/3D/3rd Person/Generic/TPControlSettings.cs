using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JR.Poorman.CharacterControllers.ThirdPerson;
using UnityEngine.InputSystem;

public class TPControlSettings : MonoBehaviour
{
    private static ThirdPersonInputActions tpInputActions;


    [Range ( 0.01f , 2f ), SerializeField] private float mouseStrength = 0.3f;
    [SerializeField] private bool invertHorizontal = false;
    [SerializeField] private bool invertVertical = false;

    private static bool staticInvertHorizontal;
    private static bool staticInvertVertical;
    private static float staticMouseStrength;

    public static float MouseStregth { get { return staticMouseStrength; }  }
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

    private void Awake ( ) => tpInputActions = new ThirdPersonInputActions();

    private void OnEnable ( ) => tpInputActions.Enable ( );
    private void OnDisable ( ) => tpInputActions.Disable ( );
}
