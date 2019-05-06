using GameSystem.GameCore.Debugger;
using GameSystem.GameCore.Physics;

namespace GameSystem.GameCore
{
    public class Game
    {
        public int GameID;

        public Scene mainScene;

        public float TargetFPS = 60f;

        public IDebugger Debugger;
        private IPhysicEngineFactory PhysicEngineFactory;

        public Game(IPhysicEngineFactory physicEngineFactory, IDebugger debugger)
        {
            Debugger = debugger;
            PhysicEngineFactory = physicEngineFactory;
            PhysicEngineProxy phyEngine = PhysicEngineFactory.Create(Debugger);
            mainScene = new Scene(phyEngine, Debugger);
        }

        public void Start()
        {
            mainScene.Run();
        }

        public void Stop()
        {
            mainScene.Stop();
        }

        public void Close()
        {
            mainScene.Stop();
        }

    }
}
