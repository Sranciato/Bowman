using UnityEngine;
using UnityEngine.Networking;

// Class for setting up player
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    GameObject sceneCamera;
    [SerializeField]
    GameObject playerUIPrefab;
    [SerializeField]
    string dontDrawLayerName = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;
    [HideInInspector]
    public GameObject playerUIInstance;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            AssignRemoteLayer();
        }
        else
        {
            //Disable player graphics for localplayer
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
            sceneCamera = GameObject.Find("Scene Camera");
            if (sceneCamera != null)
            {
                sceneCamera.SetActive(false);
            }
            //Create PlayerUI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
            //Configure PlayerUI
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
                Debug.LogError("No playerui component on playerui prefab");
            ui.SetPlayer(GetComponent<Player>());
            GetComponent<Player>().SetupPlayer();
        }
    }
    // Sets player layer recursively
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    // Sets remote players layer to remote
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }
    // Sets player id and name on start
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }
    // Destroys remote player on leaving
    void OnDisable()
    {
        Destroy(playerUIInstance);
        if (isLocalPlayer)
            GameManager.instance.SetSceneCameraActive(true);
        GameManager.UnRegisterPlayer(transform.name);
    }
}
