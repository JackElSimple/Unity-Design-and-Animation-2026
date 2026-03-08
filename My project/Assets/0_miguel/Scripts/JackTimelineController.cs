using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class JackTimelineController : MonoBehaviour
{
	private Animator _animator;
	private PlayerInput _playerInput;
	private CharacterController _characterController;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_playerInput = GetComponent<PlayerInput>();
	}

	// Esta función la puedes llamar desde el Timeline usando un Signal Emitter
	public void SetCinematicMode(bool isCinematic)
	{
		if (_playerInput != null) _playerInput.enabled = !isCinematic;
		if (_animator != null) _animator.enabled = !isCinematic;
		if (_characterController != null) _characterController.enabled = !isCinematic;

	}
}