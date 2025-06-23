using ResourcesSystem;
using VContainer;
using VContainer.Unity;
using World;
using World.States;

namespace Core
{
    public class MainInstaller : LifetimeScope
    {
        private InputSystem_Actions _inputActions;
        private WorldTransitionStateMachine _worldTransitionStateMachine;

        protected override void Configure(IContainerBuilder builder)
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Enable();
            builder.RegisterInstance(_inputActions).AsSelf();

            #region ResourcesSystem

            builder.RegisterComponentInHierarchy<Clicker>();
            builder.Register<Wallet>(Lifetime.Singleton)
                .As<ITickable>()
                .AsSelf();

            #endregion

            #region World

            _worldTransitionStateMachine = new WorldTransitionStateMachine();
            builder.RegisterInstance(_worldTransitionStateMachine);
            builder.RegisterComponentInHierarchy<CameraScroll>();
            builder.RegisterComponentInHierarchy<NavigationPanel>();
            builder.RegisterComponentInHierarchy<UpperLayerNavigation>();

            #endregion
        }

        protected override void Awake()
        {
            base.Awake();

            var lowerState = new LowerLayerState();
            Container.Inject(lowerState);
            var upperState = new UpperLayerState();
            Container.Inject(upperState);
            var transitionState = new TransitionState();
            Container.Inject(transitionState);
            _worldTransitionStateMachine.RegisterState(lowerState);
            _worldTransitionStateMachine.RegisterState(upperState);
            _worldTransitionStateMachine.RegisterState(transitionState);
            _worldTransitionStateMachine.Switch<LowerLayerState>();
        }
    }
}