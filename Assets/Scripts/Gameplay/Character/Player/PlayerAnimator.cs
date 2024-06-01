using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public enum PlayerAnimationState
{
    idle,
    move,
    attack
}
public class PlayerAnimator : MonoBehaviour
{
    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";   
    private const string ATTACK_X = "attackX";
    private const string ATTACK_Y = "attackY";
    private const string ATTACK_ANIMSPEED = "attackAnimSpeed";
    private Animator _animator;
    private PlayerManager _playerManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        _playerManager.playerController.OnMoveInputChanged += OnInputChanged;
        _playerManager.playerAttack.OnAttackStart += OnAttackInput;
        _playerManager.playerAttack.OnAttackStop += OnAttackStop;
    }

    private void OnDisable()
    {
        if (_playerManager)
        {
            _playerManager.playerController.OnMoveInputChanged -= OnInputChanged;
            _playerManager.playerAttack.OnAttackStart -= OnAttackInput;
            _playerManager.playerAttack.OnAttackStop -= OnAttackStop;
        }
    }

    public void OnInputChanged(Vector2 input)
    {
        if(input.Equals(Vector2.zero))
        {
            _animator.SetBool(PlayerAnimationState.idle.ToString(),true);
            _animator.SetBool(PlayerAnimationState.move.ToString(), false);
        }
        else
        {
            _animator.SetBool(PlayerAnimationState.move.ToString(), true);
            _animator.SetBool(PlayerAnimationState.idle.ToString(), false);
        }

        _animator.SetFloat(MOVE_X, input.x);
        _animator.SetFloat(MOVE_Y, input.y);

     
    }

    public void OnAttackInput(Vector2 input, float attackInterval)
    {
        _animator.SetBool(PlayerAnimationState.attack.ToString(), true);

        _animator.SetFloat(ATTACK_ANIMSPEED, attackInterval);
        _animator.SetFloat(ATTACK_X, input.x);
        _animator.SetFloat(ATTACK_Y, input.y);
    }
    public void OnAttackStop()
    {
        _animator.SetBool(PlayerAnimationState.attack.ToString(), false);
    }
}
