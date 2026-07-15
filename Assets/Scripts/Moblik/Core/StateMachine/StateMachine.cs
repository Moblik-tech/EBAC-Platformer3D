using System.Collections.Generic;

namespace Moblik.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryState;
        private StateBase _currentState;

        public StateBase CurrentBase
        {
            get { return _currentState; }
        }

        //public StateMachine(T state)
        //{
        //    SwitchState(state);
        //}

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }

        public void SwitchState(T state)
        {
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = dictionaryState[state];
            _currentState.OnStateEnter();
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnStateStay();
            }
        }
    }
}