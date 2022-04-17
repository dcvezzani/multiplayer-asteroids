using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class HelloWorldManager : NetworkManager
{
    public static TextMeshProUGUI status;
    public static GameObject buttons;
    public static GameObject gameActiveButtons;
    public static HelloWorld.HelloWorldPlayer localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<TextMeshProUGUI>();
        buttons = GameObject.Find("LayoutVerticalButtons");
        gameActiveButtons = GameObject.Find("LayoutVerticalGameActiveButtons");
        HideGameActiveButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            // StartButtons();
        }
        else
        {
            StatusLabels();

            SubmitNewPosition();
        }
    }

    private static void PlayerSetup(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
    {
        //Your logic here
        bool approve = true;
        bool createPlayerObject = true;

        // Position to spawn the player object at, set to null to use the default position
        Vector3? positionToSpawnAt = Vector3.zero;

        // Rotation to spawn the player object at, set to null to use the default rotation
        Quaternion rotationToSpawnWith = Quaternion.identity;

        //If approve is true, the connection gets added. If it's false. The client gets disconnected
        callback(createPlayerObject, null, approve, positionToSpawnAt, rotationToSpawnWith);
    }

    static void StartButtons()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        if (GUILayout.Button("Host", GUILayout.Width(200), GUILayout.Height(50)))
        {
            // NetworkManager.Singleton.ConnectionApprovalCallback += PlayerSetup;
            NetworkManager.Singleton.StartHost();
        }
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }
    

    public void HideGameActiveButtons()
    {
        gameActiveButtons.SetActive(false);
    }

    public void ShowGameActiveButtons()
    {
        gameActiveButtons.SetActive(true);
    }
    public void HideButtons()
    {
        buttons.SetActive(false);
    }

    public void ShowButtons()
    {
        buttons.SetActive(true);
    }

    public void MovePlayer()
    {

        var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        localPlayer = playerObject.GetComponent<HelloWorld.HelloWorldPlayer>();
        localPlayer.Move();

/*        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i=0; i<players.Length; i++)
        {
            NetworkObject player = players[i].GetComponent<NetworkObject>();
            if (player.IsLocalPlayer)
            {
                localPlayer = player;
                break;
            }
        }
        // localPlayer*/
    }

    public void StartHost()
    {
        HideButtons();
        ShowGameActiveButtons();
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        HideButtons();
        ShowGameActiveButtons();
        NetworkManager.Singleton.StartClient();
    }

    public void StartServer()
    {
        HideButtons();
        ShowGameActiveButtons();
        NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        status.text = $"Transport: {NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name}\nMode: {mode}";
        Debug.Log($"StatusLabels: {status.text}");
        // GUILayout.Label("Transport: " +
        //     NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        // GUILayout.Label("Mode: " + mode);
    }

    void SubmitNewPosition() { }

/*    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(50, 50, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            // StartButtons();
        }
        else
        {
            StatusLabels();

            SubmitNewPosition();
        }

        GUILayout.EndArea();
    }*/

}
