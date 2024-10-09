using CitizenFX.Core;
using System.Collections.Generic;

namespace eg_core.Server.players
{
    public class CreatePlayer : BaseScript
    {
        private Dictionary<Player, string> playerIdentiifies = new Dictionary<Player, string>();

        public CreatePlayer()
        {
            EventHandlers["playerConnecting"] += new System.Action<Player>(OnPlayerConnecting);
        }
        
        private void OnPlayerConnecting(Player player)
        {
            string playerId = System.Guid.NewGuid().ToString();
            playerIdentiifies[player] = playerId;
            Debug.WriteLine($"Player {player.Name} connected with ID: {playerId}");
        }
    }
}