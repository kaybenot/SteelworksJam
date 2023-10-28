using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CommandProcessor
{
    private static List<ICommandListener> listeners;
    
    public static void RegisterListener(ICommandListener listener)
    {
        if (listeners.Contains(listener))
        {
            Debug.LogWarning("Tried adding a listener to the CommandProcessor second time!");
            return;
        }
            
        listeners.Add(listener);
    }

    public static bool SendCommand(string command)
    {
        var split1 = command.Split('.');
        var listenerName = split1[0];
        var paramString = split1[1];
        var parameters = paramString.Split(' ').ToList();
        var cmd = parameters[0];
        parameters.RemoveRange(0, 1);

        foreach (var listener in listeners)
        {
            if (listener.ListenerName == listenerName)
            {
                listener.ProcessCommand(cmd, parameters);
                return true;
            }
        }
        
        return false;
    }
}
