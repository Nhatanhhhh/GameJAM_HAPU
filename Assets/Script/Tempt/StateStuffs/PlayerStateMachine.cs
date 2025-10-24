using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public event Action<PlayerState, PlayerState> OnStateChanged;

    private PlayerState _currentState = PlayerState.Idle;

    public PlayerState CurrentState
    {
        get => _currentState;
        set
        {
            if (_currentState == value) return;

            PlayerState oldState = _currentState;
            _currentState = value;

            OnStateChanged?.Invoke(oldState, _currentState);
        }
    }
}
