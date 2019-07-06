using UnityEngine;
using Fluent;

/// <summary>
/// This example shows that you can nest branches, effectively creating a dialogue tree.
/// Remember to have a look at this Conversation component in the editor, you'll be able to see what the tree looks like.
/// </summary>
public class Conversation6 : MyFluentDialogue
{
    public GameObject redGuy;

    public override FluentNode Create()
    {        
        return
            Show() *            

            Options
            (
                Write(0, "Lets look at an example with multiple branches") *
                Option("That red guy...") *
                    Write(0, "What about him ?") *
                    Options
                    (
                        Option("Whats his name ?") *
                            Write(0, "He's just an extra in the show, pay no mind") *

                        Option("Do you think he's into pink ?") *
                            Hide() *
                            Yell(redGuy, "I heard that!") *
                            Show() *
                            Write(0, "... maybe we shouldnt talk about him anymore") *

                        Option("Back") *
                            Back() 
                    ) *

                Option("Bye") *
                    Hide() *
                    Yell("Bye bye!") *
                    End()
            );
    
    }
}
