using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public class PlayerData {
        public string playerName;
        public int playerHp;
        public int playerMoney;

        public List<Item> itemList;

        public Vector3 playerLoc;

        public int activeSceneIndex;
    }

    private GameObject exitTransition;
    public static SaveData dataInstance;

    void Awake() {
        if (dataInstance == null) {
            dataInstance = this;
        }
    }

    private PlayerData playerData = new PlayerData();
    // public PlayerData getSavedData {
    //     get { return playerData; }
    // }

    private void getPlayerData() {
        playerData.playerName = PlayerPrefs.GetString("name");
        playerData.playerLoc = PlayerController.playerInstance.playerLocation;
        playerData.playerMoney = PlayerController.playerInstance.playerMoney;
        playerData.playerHp = HealthManager.instance.Health;
        playerData.itemList = Inventory.invenInstance.gsItemList;
        // Debug.Log(playerData.itemList[0]);
        playerData.activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void setPlayerData() {
        PlayerPrefs.SetString("name", playerData.playerName);
        PlayerController.playerInstance.playerLocation = playerData.playerLoc;
        Inventory.invenInstance.gsItemList = playerData.itemList;
        InventoryUI.ivUIinstance.UpdateUI();
        PlayerController.playerInstance.playerMoney = playerData.playerMoney;
        HealthManager.instance.Health = playerData.playerHp;
        LifeBar.lifeBarInstance.updateHearts();
        PlayerController.playerInstance.startPoint = "";
    }

    public void save2Json() {
        getPlayerData();
        string inventoryData = JsonUtility.ToJson(playerData);
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        System.IO.File.WriteAllText(filePath, inventoryData);
    }

    public void LoadFromJson() {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(inventoryData);
        Invoke("LoadGame", 0.5f);
    }

    void LoadGame() {
        // if (SceneManager.GetActiveScene().buildIndex == playerData.activeSceneIndex) {
        //     setPlayerData();
        // } else {
            
        // }
        SceneManager.LoadScene(playerData.activeSceneIndex);
        setPlayerData();
    }
}
