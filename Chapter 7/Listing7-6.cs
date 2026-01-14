void TryToPlayCard(Player player, Card card) 

{ 

    if (player == null  

    || !player.CanPlayCard(card)) 

    { 

        return; 

    } 

    ICommand command = new PlayCardCommand(player, card); 

    CommandInvoker.ExecuteCommand(command); 

} 