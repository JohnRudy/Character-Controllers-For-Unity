Hi! Thanks for looking into this package on character controllers.
This is a collection of three character controllers that I frequently keep wirting over and over; 
so I decided to make it into a library for myself for quick prototypes and speed up the process.
Main reason for this package is to store the state machine and states for it and have a quick reference
on how to use them.

Here's a quick overview.
-----------------------------------------------------------------------------
First and foremost. 

!!!!!!!!!!! USE THE NEW INPUT SYSTEM PACKAGE. !!!!!!!! 
!!! The state machines and states work without it. !!! 
!!! But the character prefabs work only with it.  !!!!

These character controllers use a state machine to work. 
2D, Third person and FPS controllers are included. 
Each prefab inside their respective folders. 
-----------------------------------------------------------------------------
To use a state machine and its states,
use the library from the Shared>Abstract Classes folder

namespace JR.Poorman.CharacterControllers.StateMachine
and inherit from StateMachine / State

Both classes inherit from Monobehaviour so you should have these on your main object with a rigidbody.
Infact, this entire machine works as a component based system. 
So all states and StateMachines need to be on the same object.

To switch states on a inherited StateMachine you can call
SwitchState(State state)
There is a overload method 
SwitchState<StateType>()

Both return a bool if a state switch was successful
Both will see if a state of that type is present on the gameobject then make GetCurrentState to that state.
Easy. 

See the abstract classes and child scripts to see how it works. 

States provide override methods
        public virtual void OnStateEnter ( )  <- When a state is activated. Happens only once
        public virtual void OnStateExit ( ) <- When a state is left. Happens only once
        public virtual void OnStateUpdate ( ) <- Each frame when the state is active
        public virtual void OnStateFixedUpdate ( ) <- Each FixedUpdate when the state is active
        public virtual void OnStateLateUpdate ( ) <- Each LateUpdate when the state is active

ALWAYS USE base.(method name)() when using any of these. 

Example 
public override void OnStateEnter(){
	base.OnStateEnter();
	// Your code here
}
---------------------------------------------------------------------------
On State machines. When using unity's callbacks (Update, FixedUpdate, etc)
use the override method like above and include base.method();

currently these methods require overriding and base inclusion
Awake
OnDisable
Update
FixedUpdate
LateUpdate

See any of the character controller state machines on how to use them
------------------------------------------------------------------------------------
Generic classes are independant of state machines and a quick class to reference for
inputs and camera controllers for that character controller type
------------------------------------------------------------------------------------
GroundCheck2D and GroundCheck3D use a double safety system to determine if a
rigidbody with a collider is grounded by collision and raycasting. 
Set the values as wished and read the tooltips! 
Both have a public static bool IsGrounded that the class itself checks if it's true. 
------------------------------------------------------------------------------------