using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private static SerialPort sp;
    private static string incomingMsg = "";
    private Thread ioThread;
    private bool isRunning = true;

    void Start()
    {
        ioThread = new Thread(DataThread);
        ioThread.Start();
    }

    private static void DataThread()
    {
        try
        {
            sp = new SerialPort("COM8", 115200); // Asegúrate de que el puerto COM y la velocidad de baudios son correctos
            sp.Open();
            Debug.Log("Puerto serie abierto correctamente.");

            while (true)
            {
                if (sp.IsOpen)
                {
                    try
                    {
                        incomingMsg = sp.ReadLine();
                        Debug.Log("Mensaje recibido: " + incomingMsg);
                    }
                    catch (TimeoutException) { }
                    catch (Exception e)
                    {
                        Debug.LogError("Error al leer el puerto serie: " + e.Message);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error al abrir el puerto serie: " + e.Message);
        }
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(incomingMsg))
        {
            SimulateKeyPress(incomingMsg.Trim());
            incomingMsg = ""; // Limpiar el mensaje después de procesarlo
        }
    }

    void SimulateKeyPress(string key)
    {
        Debug.Log("Simulando pulsación de tecla: " + key);
        switch (key)
        {
            case "A":
                SimulateKey(KeyCode.A);
                break;
            case "S":
                SimulateKey(KeyCode.S);
                break;
            case "D":
                SimulateKey(KeyCode.D);
                break;
            case "F":
                SimulateKey(KeyCode.F);
                break;
            default:
                Debug.LogWarning("Tecla no reconocida: " + key);
                break;
        }
    }

    void SimulateKey(KeyCode key)
    {
        NewBehaviourScript[] keyScripts = FindObjectsOfType<NewBehaviourScript>();

        foreach (NewBehaviourScript script in keyScripts)
        {
            if (script.keyToPress == key)
            {
                script.OnKeyPress();
            }
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        if (ioThread != null && ioThread.IsAlive)
        {
            ioThread.Abort();
        }

        if (sp != null && sp.IsOpen)
        {
            sp.Close();
        }
    }
}