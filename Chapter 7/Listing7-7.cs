public class CommandInvoker 

{ 

    static Stack<ICommand> undoStack = new Stack<ICommand>(); 

    static Stack<ICommand> redoStack = new Stack<ICommand>(); 

  

    // executes a command and saves it in the undo stack 

    public static void ExecuteCommand(ICommand command) 

    { 

        command.Execute(); 

        undoStack.Push(command); 

  

        // clear out the redo stack if we make a new move 

        redoStack.Clear(); 

    } 

  

    public static void UndoCommand() 

    { 

        if (undoStack.Count > 0) 

        { 

            ICommand activeCommand = undoStack.Pop(); 

            redoStack.Push(activeCommand); 

            activeCommand.Undo(); 

        } 

    } 

  

    public static void RedoCommand() 

    { 

        if (redoStack.Count > 0) 

        { 

            ICommand activeCommand = redoStack.Pop(); 

            undoStack.Push(activeCommand); 

            activeCommand.Execute(); 

        } 

    } 

} 