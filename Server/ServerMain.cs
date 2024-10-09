using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using eg_core.Server.players;

namespace eg_core.Server
{
    public class ServerMain : BaseScript
    {
        private CreatePlayer _createPlayer;  // Instância da classe CreatePlayer 
        public ServerMain()
        {
            Debug.WriteLine("Hi from eg_core.Server!");
            _createPlayer = new CreatePlayer();
        }

        [Command("hello_server")]
        public void HelloServer()
        {
            Debug.WriteLine("Sure, hello.");
        }
    }
}