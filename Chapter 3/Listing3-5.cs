using UnityEngine; 

using Mirror; 

//note how this is a MonoBehaviour, so it’s not networked 

class Scores : MonoBehaviour 

{ 

    public struct ScoreMessage : NetworkMessage 

    { 

        public int score; 

        public Vector3 scorePos; 

    } 

  

    void SendScore(int score, Vector3 scorePos) 

    { 

        var message = new ScoreMessage() 

        { 

            score = score, 

            scorePos = scorePos 

        }; 

        NetworkServer.SendToAll(message); 

    } 

  

    void SetupClient() 

    { 

        NetworkClient.RegisterHandler<ScoreMessage>(OnClientScoreReceived); 

        NetworkClient.Connect("localhost"); 

    } 

  

    void OnClientScoreReceived(ScoreMessage message) 

    { 

        Debug.Log($"Received score {message.score} from position {message.scorePos}"); 

    } 

} 