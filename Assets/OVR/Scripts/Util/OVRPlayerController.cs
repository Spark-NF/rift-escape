/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls the player's movement in virtual reality.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class OVRPlayerController : MonoBehaviour
{
	#region Movement dubois_d
	public bool ovrMovement = true;                              // Enable move player by moving head on X and Z axis
	public bool ovrJump = false;                                  // Enable player jumps by moving head on Y axis upwards
	public Vector3 ovrRotationMinimum = new Vector3 (60, 60, 0); // Sensitivity to trigger rotation movement
	public Vector3 ovrControlSensitivity = new Vector3(10f, 0.05f, 2f);  // Multiplier of positiona tracking move/jump actions
	public Vector3 ovrControlMinimum = new Vector3(0.15f, 0.05f, 0.05f);      // Min distance of head from centre to move/jump
	public enum OvrXAxisAction { Strafe = 0, Rotate = 1 }
	public OvrXAxisAction ovrXAxisAction = OvrXAxisAction.Strafe; // Whether x axis positional tracking performs strafing or rotation
	public bool softCrounching = false;
	public float upDetection = .15f;

	// OVR positional tracking, currently works via tilting head
	private Vector3? initPosTrackDir = null;
	private Vector3 curPosTrackDir;
	private Vector3 diffPosTrackDir;

	// Variables for bending
	private float InitialHeight;
	public float crouchHeight = 0.8f;
	private bool crounched = false;
	#endregion

	/// <summary>
	/// The rate acceleration during movement.
	/// </summary>
	public float Acceleration = 0.1f;

	/// <summary>
	/// The rate of damping on movement.
	/// </summary>
	public float Damping = 0.3f;

	/// <summary>
	/// The rate of additional damping when moving sideways or backwards.
	/// </summary>
	public float BackAndSideDampen = 0.5f;

	/// <summary>
	/// The force applied to the character when jumping.
	/// </summary>
	public float JumpForce = 0.3f;

	/// <summary>
	/// The rate of rotation when using a gamepad.
	/// </summary>
	public float RotationAmount = 1.5f;

	/// <summary>
	/// The rate of rotation when using the keyboard.
	/// </summary>
	public float RotationRatchet = 2.0f;

	/// <summary>
	/// The player's current rotation about the Y axis.
	/// </summary>
	private float YRotation = 0.0f;

	/// <summary>
	/// If true, tracking data from a child OVRCameraRig will update the direction of movement.
	/// </summary>
	public bool HmdRotatesY = true;

	/// <summary>
	/// Modifies the strength of gravity.
	/// </summary>
	public float GravityModifier = 0.379f;

	private float MoveScale = 1.0f;
	private Vector3 MoveThrottle = Vector3.zero;
	private float FallSpeed = 0.0f;	
	private OVRPose? InitialPose;
	
	/// <summary>
	/// If true, each OVRPlayerController will use the player's physical height.
	/// </summary>
	public bool useProfileHeight = true;

	protected CharacterController Controller = null;
	protected OVRCameraRig CameraController = null;

	private float MoveScaleMultiplier = 1.0f;
	private float RotationScaleMultiplier = 1.0f;
	private bool  SkipMouseRotation = false;
	private bool  HaltUpdateMovement = false;
	private bool prevHatLeft = false;
	private bool prevHatRight = false;
	private float SimulationRate = 60f;

	void Awake()
	{
		Controller = gameObject.GetComponent<CharacterController>();

		if(Controller == null)
			Debug.LogWarning("OVRPlayerController: No CharacterController attached.");

		// We use OVRCameraRig to set rotations to cameras,
		// and to be influenced by rotation
		OVRCameraRig[] CameraControllers;
		CameraControllers = gameObject.GetComponentsInChildren<OVRCameraRig>();

		if(CameraControllers.Length == 0)
			Debug.LogWarning("OVRPlayerController: No OVRCameraRig attached.");
		else if (CameraControllers.Length > 1)
			Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraRig attached.");
		else
			CameraController = CameraControllers[0];

		YRotation = transform.rotation.eulerAngles.y;
		// dubois_d : init for default camera position
		/*if (CameraController != null)
			initPosTrackDir = CameraController.transform.localPosition; //.centerEyeAnchor.localPosition;
		else
			initPosTrackDir = Vector3.zero;
		*/
		if (Controller != null)
			InitialHeight = Controller.height;
		// end dubois_d

#if UNITY_ANDROID && !UNITY_EDITOR
		OVRManager.display.RecenteredPose += ResetOrientation;
#endif
	}

	protected virtual void Update()
	{
		if (useProfileHeight) {
						if (InitialPose == null) {
								InitialPose = new OVRPose () {
					position = CameraController.transform.localPosition,
					orientation = CameraController.transform.localRotation
				};
						}

						var p = CameraController.transform.localPosition;
						p.y = OVRManager.profile.eyeHeight /*dubois_d adding a scale for crounching*/ * Controller.height / InitialHeight - 0.5f * Controller.height;
						p.z = OVRManager.profile.eyeDepth;
						CameraController.transform.localPosition = p;
				} else if (InitialPose != null) {
						CameraController.transform.localPosition = InitialPose.Value.position;
						CameraController.transform.localRotation = InitialPose.Value.orientation;
						InitialPose = null;
				}

		UpdateMovement();

		Vector3 moveDirection = Vector3.zero;

		float motorDamp = (1.0f + (Damping * SimulationRate * Time.deltaTime));

		MoveThrottle.x /= motorDamp;
		MoveThrottle.y = (MoveThrottle.y > 0.0f) ? (MoveThrottle.y / motorDamp) : MoveThrottle.y;
		MoveThrottle.z /= motorDamp;

		moveDirection += MoveThrottle * SimulationRate * Time.deltaTime;

		// Gravity
		if (Controller.isGrounded && FallSpeed <= 0)
			FallSpeed = ((Physics.gravity.y * (GravityModifier * 0.002f)));
		else
			FallSpeed += ((Physics.gravity.y * (GravityModifier * 0.002f)) * SimulationRate * Time.deltaTime);

		moveDirection.y += FallSpeed * SimulationRate * Time.deltaTime;

		// Offset correction for uneven ground
		float bumpUpOffset = 0.0f;

		if (Controller.isGrounded && MoveThrottle.y <= 0.001f)
		{
			bumpUpOffset = Mathf.Max(Controller.stepOffset, new Vector3(moveDirection.x, 0, moveDirection.z).magnitude);
			moveDirection -= bumpUpOffset * Vector3.up;
		}

		Vector3 predictedXZ = Vector3.Scale((Controller.transform.localPosition + moveDirection), new Vector3(1, 0, 1));

		// Move contoller
		Controller.Move(moveDirection);

		Vector3 actualXZ = Vector3.Scale(Controller.transform.localPosition, new Vector3(1, 0, 1));

		if (predictedXZ != actualXZ)
			MoveThrottle += (actualXZ - predictedXZ) / (SimulationRate * Time.deltaTime);
	}

	public virtual void UpdateMovement()
	{
		if (HaltUpdateMovement)
			return;

		MoveScale = 1.0f;
		bool moveForward = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow);
		bool moveLeft = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow);
		bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
		bool moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
		bool jump = Input.GetKey(KeyCode.Space);

		// dubois_d  Get the input vector from OVR positional tracking and add bending / standing up

		bool moveUp = Input.GetKey(KeyCode.R);
		bool moveDown = Input.GetKey(KeyCode.F);
		bool Reinit_movepos = Input.GetKey (KeyCode.RightControl) || Input.GetKey (KeyCode.LeftControl);

		if (initPosTrackDir == null) {
			initPosTrackDir = CameraController.centerEyeAnchor.transform.localPosition;
		}
		if (Reinit_movepos) {
			OVRManager.display.RecenterPose();
			initPosTrackDir = CameraController.centerEyeAnchor.transform.localPosition;
				}

		if (ovrMovement || ovrJump) {
			curPosTrackDir = CameraController.centerEyeAnchor.transform.localPosition;
			diffPosTrackDir = curPosTrackDir - initPosTrackDir.GetValueOrDefault();
		}
		// mounting the movement vetor (movement have to be greater that sensitivity)
		Vector3 directionVector = Vector3.zero;
		if (ovrMovement) {
			/*if (diffPosTrackDir.x <= -ovrControlMinimum.x)
				moveBack = true;
			if (diffPosTrackDir.x >= ovrControlMinimum.x)
				moveForward = true;
			if (diffPosTrackDir.y <= -ovrControlMinimum.y)
				moveLeft = true;
			if (diffPosTrackDir.y >= ovrControlMinimum.y)
				moveRight = true;*/

			if (diffPosTrackDir.x <= -ovrControlMinimum.x){
				diffPosTrackDir.x += ovrControlMinimum.x;
				if (ovrXAxisAction == OvrXAxisAction.Strafe) {
					diffPosTrackDir.x *= ovrControlSensitivity.x;
				} else {
					transform.Rotate(0, diffPosTrackDir.x * ovrControlSensitivity.x, 0);
					diffPosTrackDir.x = 0;
				}
			}
			else if (diffPosTrackDir.x >= ovrControlMinimum.x) {
				diffPosTrackDir.x -= ovrControlMinimum.x;
				if (ovrXAxisAction == OvrXAxisAction.Strafe) {
					diffPosTrackDir.x *= ovrControlSensitivity.x;
				} else {
					transform.Rotate(0, diffPosTrackDir.x * ovrControlSensitivity.x, 0);
					diffPosTrackDir.x = 0;
				}
			} else {
				diffPosTrackDir.x = 0;
			}

			if (diffPosTrackDir.z <= -ovrControlMinimum.z){
				diffPosTrackDir.z += ovrControlMinimum.z;
				diffPosTrackDir.z *= ovrControlSensitivity.z;
			}
			else if(diffPosTrackDir.z >= ovrControlMinimum.z) {
				diffPosTrackDir.z -= ovrControlMinimum.z;
				diffPosTrackDir.z *= ovrControlSensitivity.z;
			} else {
				diffPosTrackDir.z = 0;
			}
			directionVector = new Vector3(diffPosTrackDir.x, 0, diffPosTrackDir.z);
		}
		
		// juming when head is high
		/*if (ovrJump && diffPosTrackDir.y > ovrControlMinimum.y)
			Jump();*/
		// crounching if oculus is low enough

		if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + Controller.height / 2, transform.position.z),
		                    transform.TransformDirection (Vector3.up), upDetection)) {
			// nearly colliding with top => moving down
			moveDown = true;
			//Debug.Log("There is something in front of the object!");
		}
		if (softCrounching) {
			if ((moveDown || diffPosTrackDir.y <= -ovrControlMinimum.y) && Controller.height > crouchHeight)
				Controller.height -= ovrControlSensitivity.y;
			if ((moveUp || diffPosTrackDir.y >= ovrControlMinimum.y) && Controller.height < InitialHeight)
				Controller.height += ovrControlSensitivity.y;
		} else {
			if (moveUp || diffPosTrackDir.y >= ovrControlMinimum.y)
				crounched = false;
			if (moveDown || diffPosTrackDir.y <= -ovrControlMinimum.y)
				crounched = true;
			if (!crounched && Controller.height < InitialHeight)
				Controller.height += ovrControlSensitivity.y;
			if (crounched && Controller.height > crouchHeight)
				Controller.height -= ovrControlSensitivity.y;
		}

		// end dubois_d

		bool dpad_move = false;

		if (OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.Up))
		{
			moveForward = true;
			dpad_move   = true;

		}
		if (OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.Down))
		{
			moveBack  = true;
			dpad_move = true;
		}


		if ( (moveForward && moveLeft) || (moveForward && moveRight) ||
			 (moveBack && moveLeft)    || (moveBack && moveRight) )
			MoveScale *= 0.70710678f;

		// No positional movement if we are in the air
		if (!Controller.isGrounded)
			MoveScale = 0.0f;

		MoveScale *= SimulationRate * Time.deltaTime;

		// Compute this for key movement
		float moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;

		// Run!
		if (dpad_move || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			moveInfluence *= 2.0f;

		Quaternion ort = (HmdRotatesY) ? CameraController.centerEyeAnchor.rotation : transform.rotation;
		Vector3 ortEuler = ort.eulerAngles;
		ortEuler.z = ortEuler.x = 0f;
		ort = Quaternion.Euler(ortEuler);

		if (moveForward)
			MoveThrottle += ort * (transform.lossyScale.z * moveInfluence * Vector3.forward);
		if (moveBack)
			MoveThrottle += ort * (transform.lossyScale.z * moveInfluence * BackAndSideDampen * Vector3.back);
		if (moveLeft)
			MoveThrottle += ort * (transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.left);
		if (moveRight)
			MoveThrottle += ort * (transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.right);
		// dubois_d ovr movement
		MoveThrottle += ort * (moveInfluence * new Vector3(directionVector.x * transform.lossyScale.x * BackAndSideDampen * ovrControlSensitivity.x,
		                                                   0, directionVector.z * transform.lossyScale.z * ovrControlSensitivity.z
		                                                   * (directionVector.z < 0? BackAndSideDampen : 1))); // performs actual movement
		// end dubois_d
		//if (jump)
		//	Jump ();

		bool curHatLeft = OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.LeftShoulder);

		Vector3 euler = transform.rotation.eulerAngles;

		if (curHatLeft && !prevHatLeft)
			euler.y -= RotationRatchet;

		prevHatLeft = curHatLeft;

		bool curHatRight = OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.RightShoulder);

		if(curHatRight && !prevHatRight)
			euler.y += RotationRatchet;

		prevHatRight = curHatRight;

		//Use keys to ratchet rotation
		if (Input.GetKey(KeyCode.A))
			euler.y -= RotationAmount;

		if (Input.GetKey(KeyCode.E))
			euler.y += RotationAmount;

		// dubois_d OVR movement rotation
		if (ovrMovement)
			OvrRotationUpdate(ref euler);

		float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;

		if (!SkipMouseRotation)
			euler.y += Input.GetAxis("Mouse X") * rotateInfluence * 3.25f;

		moveInfluence = SimulationRate * Time.deltaTime * Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;

#if !UNITY_ANDROID // LeftTrigger not avail on Android game pad
		moveInfluence *= 1.0f + OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftTrigger);
#endif

		float leftAxisX = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftXAxis);
		float leftAxisY = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftYAxis);

		if(leftAxisY > 0.0f)
			MoveThrottle += ort * (leftAxisY * moveInfluence * Vector3.forward);

		if(leftAxisY < 0.0f)
			MoveThrottle += ort * (Mathf.Abs(leftAxisY) * moveInfluence * BackAndSideDampen * Vector3.back);

		if(leftAxisX < 0.0f)
			MoveThrottle += ort * (Mathf.Abs(leftAxisX) * moveInfluence * BackAndSideDampen * Vector3.left);

		if(leftAxisX > 0.0f)
			MoveThrottle += ort * (leftAxisX * moveInfluence * BackAndSideDampen * Vector3.right);

		float rightAxisX = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.RightXAxis);

		euler.y += rightAxisX * rotateInfluence;

		transform.rotation = Quaternion.Euler(euler);
	}

	/// <summary>
	/// Moves according to OVR rotation
	/// </summary>
	private void OvrRotationUpdate(ref Vector3 euler)
	{
		// rotation left / right
		float yAngle = CameraController.centerEyeAnchor.transform.localRotation.eulerAngles.y;
		if (yAngle > 180) // on the left side : 360->180
		{
			yAngle = -1 * (yAngle - 360); // recentering the angle on 0 - 90
			if (yAngle > ovrRotationMinimum.y)
				euler.y -= (yAngle - ovrRotationMinimum.y) * RotationAmount / (90 - ovrRotationMinimum.y);
		}
		else // on the right side : 0 -> 180
		{
			if (yAngle > ovrRotationMinimum.y)
				euler.y += (yAngle - ovrRotationMinimum.y) * RotationAmount / (90 - ovrRotationMinimum.y);
		}

		float xAngle = CameraController.centerEyeAnchor.transform.localRotation.eulerAngles.x;
		if (xAngle > 180) // on the top side : 360->180
		{
			xAngle = -1 * (xAngle - 360); // recentering the angle on 0 - 90
			if (xAngle > ovrRotationMinimum.x)
				crounched = false;
		}
		else // on the bottom side : 0 -> 180
		{
			if (xAngle > ovrRotationMinimum.x)
				crounched = true;
		}
	}

	/// <summary>
	/// Jump! Must be enabled manually.
	/// </summary>
	public bool Jump()
	{
		if (!Controller.isGrounded)
			return false;

		MoveThrottle += new Vector3(0, JumpForce, 0);

		return true;
	}

	/// <summary>
	/// Stop this instance.
	/// </summary>
	public void Stop()
	{
		Controller.Move(Vector3.zero);
		MoveThrottle = Vector3.zero;
		FallSpeed = 0.0f;
	}

	/// <summary>
	/// Gets the move scale multiplier.
	/// </summary>
	/// <param name="moveScaleMultiplier">Move scale multiplier.</param>
	public void GetMoveScaleMultiplier(ref float moveScaleMultiplier)
	{
		moveScaleMultiplier = MoveScaleMultiplier;
	}

	/// <summary>
	/// Sets the move scale multiplier.
	/// </summary>
	/// <param name="moveScaleMultiplier">Move scale multiplier.</param>
	public void SetMoveScaleMultiplier(float moveScaleMultiplier)
	{
		MoveScaleMultiplier = moveScaleMultiplier;
	}

	/// <summary>
	/// Gets the rotation scale multiplier.
	/// </summary>
	/// <param name="rotationScaleMultiplier">Rotation scale multiplier.</param>
	public void GetRotationScaleMultiplier(ref float rotationScaleMultiplier)
	{
		rotationScaleMultiplier = RotationScaleMultiplier;
	}

	/// <summary>
	/// Sets the rotation scale multiplier.
	/// </summary>
	/// <param name="rotationScaleMultiplier">Rotation scale multiplier.</param>
	public void SetRotationScaleMultiplier(float rotationScaleMultiplier)
	{
		RotationScaleMultiplier = rotationScaleMultiplier;
	}

	/// <summary>
	/// Gets the allow mouse rotation.
	/// </summary>
	/// <param name="skipMouseRotation">Allow mouse rotation.</param>
	public void GetSkipMouseRotation(ref bool skipMouseRotation)
	{
		skipMouseRotation = SkipMouseRotation;
	}

	/// <summary>
	/// Sets the allow mouse rotation.
	/// </summary>
	/// <param name="skipMouseRotation">If set to <c>true</c> allow mouse rotation.</param>
	public void SetSkipMouseRotation(bool skipMouseRotation)
	{
		SkipMouseRotation = skipMouseRotation;
	}

	/// <summary>
	/// Gets the halt update movement.
	/// </summary>
	/// <param name="haltUpdateMovement">Halt update movement.</param>
	public void GetHaltUpdateMovement(ref bool haltUpdateMovement)
	{
		haltUpdateMovement = HaltUpdateMovement;
	}

	/// <summary>
	/// Sets the halt update movement.
	/// </summary>
	/// <param name="haltUpdateMovement">If set to <c>true</c> halt update movement.</param>
	public void SetHaltUpdateMovement(bool haltUpdateMovement)
	{
		HaltUpdateMovement = haltUpdateMovement;
	}

	/// <summary>
	/// Resets the player look rotation when the device orientation is reset.
	/// </summary>
	public void ResetOrientation()
	{
		Vector3 euler = transform.rotation.eulerAngles;
		euler.y = YRotation;
		transform.rotation = Quaternion.Euler(euler);
	}
}

