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

        public Equipment[] playerEquipment;
    }

    private GameObject exitTransition;
    public bool saveExist;
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
        playerData.playerEquipment = EquipmentManager.instance.equipmentList;
        // Debug.Log(playerData.itemList[0]);
        if(SceneManager.GetActiveScene().buildIndex == 0){
            playerData.activeSceneIndex = 1;
        } else {
            playerData.activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void setPlayerData() {
        PlayerPrefs.SetString("name", playerData.playerName);
        PlayerController.playerInstance.playerLocation = playerData.playerLoc;
        Inventory.invenInstance.gsItemList = playerData.itemList;
        EquipmentManager.instance.equipmentList = playerData.playerEquipment;
        InventoryUI.ivUIinstance.UpdateUI();
        PlayerController.playerInstance.playerMoney = playerData.playerMoney;
        HealthManager.instance.Health = playerData.playerHp;
        LifeBar.lifeBarInstance.updateHearts();
        PlayerController.playerInstance.startPoint = "";
    }

    public void startNewGame() {
        PlayerController.playerInstance.playerLocation = new Vector3(-19.7f, -5.33f, 0);
        Inventory.invenInstance.gsItemList = new List<Item>();
        InventoryUI.ivUIinstance.UpdateUI();
        PlayerController.playerInstance.playerMoney = 0;
        HealthManager.instance.Health = 5;
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
        if (inventoryData == "{}") {
            saveExist = false;
            MainMenu.menuInstance.setAlert(true);
        } else {
            saveExist = true;
            playerData = JsonUtility.FromJson<PlayerData>(inventoryData);
        }
        if (saveExist) {
            Invoke("LoadGame", 0.5f);
        }
    }

    public bool returnSaveExist() {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);
        if (inventoryData == "{}") {
            return false;
        } else {
            return true;
        }
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
