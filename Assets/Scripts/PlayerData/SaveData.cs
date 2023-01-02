using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public class PlayerData {
        public string playerName;
        public int playerHp;

        public List<Item> itemList;
    }

    private PlayerData playerData = new PlayerData();

    private void setPlayerData() {
        playerData.playerName = PlayerPrefs.GetString("name");
        playerData.playerHp = HealthManager.instance.Health;
        playerData.itemList = Inventory.invenInstance.itemList;
    }

    public void save2Json() {
        setPlayerData();
        string inventoryData = JsonUtility.ToJson(playerData);
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        System.IO.File.WriteAllText(filePath, inventoryData);
    }

    public void LoadFromJson() {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);
        // playerData = JsonUtility.FromJson<PlayerData>(inventoryData);
    }
}
