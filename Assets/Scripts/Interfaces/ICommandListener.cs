using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandListener
{
    string ListenerName { get; set; }
    
    void ProcessCommand(string command, List<string> parameters);
}
