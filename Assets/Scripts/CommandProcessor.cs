using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CommandProcessor
{
    private static Dictionary<string, ICommandListener> listeners = new ();
    
    public static void RegisterListener(ICommandListener listener)
    {
        if (listeners.ContainsKey(listener.ListenerName))
        {
            Debug.LogWarning("Tried adding a listener to the CommandProcessor second time!");
            Debug.LogWarning("Overwriting...");
            //return;
        }
            
        listeners[listener.ListenerName] =  listener;
    }

    public static bool SendCommand(string command)
    {
        var split1 = command.Split(' ');
        var listenerName = split1[0];
        var cmd = split1[1];
        //var parameters = paramString.Split(' ').ToList();
        //var cmd = parameters[0];
        var parameters = split1.Skip(2).ToList();
        //parameters.RemoveRange(0, 1);

        foreach (var listener in listeners)
        {
            if (listener.Key == listenerName)
            {
                listener.Value.ProcessCommand(cmd, parameters);
                return true;
            }
        }

        Debug.LogWarning($"{listenerName} is not a registered listener!");
        return false;
    }
}
