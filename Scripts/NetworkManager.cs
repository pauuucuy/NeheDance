using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Globalization;

public class NetworkManager : MonoBehaviour
{
    //-------------- REGISTER -------------
    public void CreateUser(string nombre, string apellido, string edad, string usuario, string correo, string contraseña, Action<Response> response)
    {
        StartCoroutine(CO_CreateUser(nombre, apellido, edad, usuario, correo, contraseña, response));
    }

    private IEnumerator CO_CreateUser(string nombre, string apellido, string edad, string usuario, string correo, string contraseña, Action<Response> response)
    {
        WWWForm form = new WWWForm();

        //variables unity
        form.AddField("nombre", nombre);
        form.AddField("apellido", apellido);
        form.AddField("edad", edad);
        form.AddField("usuario", usuario);
        form.AddField("correo", correo);
        form.AddField("contraseña", contraseña);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:80/conect/createUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                response(new Response { done = false, message = "Server error: " + www.error });
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                try
                {
                    Response jsonResponse = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                    response(jsonResponse);
                }
                catch (Exception e)
                {
                    Debug.LogError("JSON parse error: " + e.Message);
                    response(new Response { done = false, message = "Error parsing server response." });
                }
            }
        }
    }


    //-------------- LOGIN -------------

    public void CheckUser(string correo, string contraseña, Action<Response> response)
    {
        StartCoroutine(CO_CheckUser(correo, contraseña, response));
    }

    private IEnumerator CO_CheckUser(string correo, string contraseña, Action<Response> response)
    {
        WWWForm form = new WWWForm();

        form.AddField("correo", correo);
        form.AddField("contraseña", contraseña);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:80/conect/checkUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                response(new Response { done = false, message = "Server error: " + www.error });
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                try
                {
                    Response jsonResponse = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                    response(jsonResponse);
                }
                catch (Exception e)
                {
                    Debug.LogError("JSON parse error: " + e.Message);
                    response(new Response { done = false, message = "Error parsing server response." });
                }
            }
        }
    }




}

[Serializable]
public class Response
{
    public bool done = false;
    public string message = "";
}
