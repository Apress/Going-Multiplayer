//Command object 

public class PlayCardCommand : ICommand 

{ 

    Player player; 

    Card card; 

    public PlayCardCommand(Player player, Card card) 

    { 

        this.player = player; 

        this.card = card; 

    } 

    public void Execute() 

    { 

        player.PlayCard(card); 

    } 

    public void Undo() 

    { 

        player.UndoPlayCard(card); 

    } 

} 

 

//Gameplay action 

public class Player : MonoBehaviour 

{ 

    List<Card> cardsInHand; 

    List<Card> cardsOnField; 

  

    public void PlayCard(Card card) 

    { 

        cardsInHand.Remove(card); 

        cardsOnField.Add(card); 

    } 

 

    public void UndoPlayCard(Card card) 

    { 

        cardsOnField.Remove(card); 

        cardsInHand.Add(card); 

    } 

 

  

    public bool CanPlayCard(Card card) 

    { 

        return cardsInHand.Contains(card); 

    } 

} 