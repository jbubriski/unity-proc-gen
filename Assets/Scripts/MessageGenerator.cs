using UnityEngine;
using System.Collections;

public class MessageGenerator : MonoBehaviour
{
    private string[] _prefix = new[] {
        "You should ",
        "What if you ",
        "Maybe you should ",
        "Possibly, you should ",
        "If only you could ",
        "If only there was a way to ",
        "How hard could it be to ",
        "How will you ever ",

        "Whatever you do, don't ",
        "You definitely don't want to ",
        "You better not ",
        "Don't ",
        "See what happens if you ",
    };

    private string[] _actions = new[] {
        "jump off the edge",
        "fall off the edge",
        "crawl in the hole",
        "move slowly",
        "move quickly",
        "be careful",
        "hurt the locals",
        "talk to the locals",
        "pet the locals",
        "look at the locals",
        "mingle with the locals",
        "dance around",
        "train more",
        "smash all the pots",
        "open all the chests",
        "stay away from the crypts",
        "find the exit",
        "stare into the darkness",
        "fall to the darkness",
        "make friends with the darkness",
        "give up hope",
        "stay strong",
        "ruin it",
        "finish it"
    };

    private string[] _endings = new[] {
        "!",
        "...",
        "."
    };

    void Start()
    {
    }

    void Update()
    {
    }

    public string GetMessage()
    {
        return _prefix.RandomElement() + _actions.RandomElement() + _endings.RandomElement();
    }
}
