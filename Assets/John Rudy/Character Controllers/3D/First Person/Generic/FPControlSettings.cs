using JR.Poorman.CharacterControllers.FirstPerson;
using UnityEngine;

/// <summary>
/// The summary of player set settings for character movement
/// </summary>

public class FPControlSettings : MonoBehaviour {
    // Input actions generated as a C# class
    private static FirstPersonInputActions m_FPActions;

    // Mouse Settings
    private static bool m_InvertVertical = false;
    private static bool m_InvertHorizontal = false;
    private static float m_MouseStrength = 0.3f;

    // Inspector Visibility
    // Proper options need a different handling method
    [Header("Mouse Settings")]
    [SerializeField] private bool invertVertical;
    [SerializeField] private bool invertHorizontal;
    [SerializeField] private float mouseStrength;

    // Public accessors
    public static FirstPersonInputActions FPActions { get { return m_FPActions; } }
    public static bool InvertVertical { get { return m_InvertVertical; } }
    public static bool InvertHorizontal { get { return m_InvertHorizontal; } }
    public static float MouseStrength { get { return m_MouseStrength; } }

    // Unity callbacks
    private void OnValidate ( ) {
        m_InvertVertical = invertVertical;
        m_InvertHorizontal = invertHorizontal;
        m_MouseStrength = mouseStrength;
    }
    private void Awake ( ) => m_FPActions = new FirstPersonInputActions ( );
    private void OnEnable ( ) => m_FPActions.Enable ( );
    private void OnDisable ( ) => m_FPActions.Disable ( );
}