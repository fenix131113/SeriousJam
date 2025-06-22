using System;
using System.Collections.Generic;
using Utils.StateMachineSystem.Data;

namespace Utils.StateMachineSystem
{
    public abstract class StateMachine
    {
        protected readonly Dictionary<Type, AState> _states = new();
        protected AState _currentState;

        public void Switch<T>() where T : AState
        {
            if(!_states.ContainsKey(typeof(T)))
                return;
            
            _currentState?.OnExit();
            _currentState = _states[typeof(T)];
            _currentState?.OnEnter();
        }

        public void RegisterState(AState state)
        {
            if(_states.ContainsKey(state.GetType()))
                return;
            
            _states.Add(state.GetType(), state);
        }
    }
}