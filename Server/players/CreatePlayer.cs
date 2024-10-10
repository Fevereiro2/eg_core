using System;
using System.Collections.Generic;
using System.Data;
using CitizenFX.Core;
using Npgsql; // Biblioteca para PostgreSQL

namespace eg_core.Server.players
{
    public class CreatePlayer : BaseScript
    {
        private Dictionary<string, string> playerIdentiifies = new Dictionary<string, string>();

        // String de conexão com PostgreSQL
        private string connectionString = "Host=localhost;Username=fevereiro;Password=z1x2c3v4b5n6m7;Database=egsolutions"; // Substitua com suas credenciais

        public CreatePlayer()
        {
            EventHandlers["playerConnecting"] += new Action<string, CallbackDelegate, dynamic>(OnPlayerConnecting);
        }

        private void OnPlayerConnecting(string playerName, CallbackDelegate setKickReason, dynamic deferrals)
        {
            deferrals.defer(); // Deferrals podem ser usados para verificar a conexão

            string uniqueId = GetOrCreatePlayerId(playerName);

            Debug.WriteLine($"Player {playerName} connected with ID: {uniqueId}");

            deferrals.done();
        }

        // Função para verificar se o jogador já existe e criar um novo ID se necessário
        private string GetOrCreatePlayerId(string playerName)
        {
            string playerId = null;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Verificar se o jogador já está registrado
                string queryCheck = "SELECT player_id FROM players WHERE player_name = @name";
                using (var cmd = new NpgsqlCommand(queryCheck, connection))
                {
                    cmd.Parameters.AddWithValue("name", playerName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        playerId = result.ToString(); // O jogador já existe, retorna o ID
                    }
                }

                // Se o jogador não existe, cria um novo registro
                if (playerId == null)
                {
                    playerId = Guid.NewGuid().ToString();
                    string queryInsert = "INSERT INTO players (player_name, player_id) VALUES (@name, @id)";
                    using (var cmd = new NpgsqlCommand(queryInsert, connection))
                    {
                        cmd.Parameters.AddWithValue("name", playerName);
                        cmd.Parameters.AddWithValue("id", playerId);
                        cmd.ExecuteNonQuery(); // Executa a inserção
                    }
                }
            }

            return playerId;
        }
    }
}
