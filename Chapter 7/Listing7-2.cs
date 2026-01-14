public class AIBrain : MonoBehaviour
{
	System.Action behave;
    public AILevel Level;

    public enum AILevel : byte
	{
	   Test,
	   Easy,
	   Medium,
	   Hard
	}

    //Called from other classes at setup time
	public void SetLevel(AILevel level)
	{
	    Level = level;
	    switch (level)
	    {
	        case AILevel.Test:
	            behave = DoTestAI;
	            break;
	        case AILevel.Easy:
	        	behave = DoEasyAI;
	            break;
	        case AILevel.Medium:
	        	behave = DoMediumAI;
	            break;
	        case AILevel.Hard:
	            behave = DoHardAI;
	            break;
	        default:
	            behave = DoTestAI;
	            break;
	    }
	}

	void OnServerBehave()
	{
	    behave?.Invoke();
	}

	void DoTestAI() { /* Do nothing */ }
	void DoEasyAI() { /* Play as a noob */}
	void DoMediumAI() { /* Play as a normal player */ }
	void DoHardAI() { /* Play as a professional */}
}