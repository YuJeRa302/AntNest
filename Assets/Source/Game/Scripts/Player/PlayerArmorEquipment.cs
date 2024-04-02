using UnityEngine;

public class PlayerArmorEquipment : MonoBehaviour
{
    private const string SaveKey = "PlayerEquipment";

    [Header("[Player Entity]")]
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _armorObjectContainer;
    [Header("[Default Equipment Data]")]
    [SerializeField] private PlayerEquipmentState _defaultPlayerEquipmentState;

    private PlayerEquipmentState _playerEquipmentState;
    private EquipmentItemGameObject _armorObject;

    public EquipmentItemState CurrentArmor => _playerEquipmentState.EquippedArmor;

    private void Awake()
    {
        LoadPlayerEquipmentState();
    }

    public void BuyArmorItem(EquipmentItemState equipmentItemState)
    {
        equipmentItemState.IsBuyed = true;
    }

    public void EquipArmor(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null)
            return;

        if (_armorObject != null)
            Destroy(_armorObject.gameObject);

        if (CurrentArmor != null)
            CurrentArmor.IsEquipped = false;

        equipmentItemState.IsEquipped = true;
        _armorObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _armorObjectContainer);
        _player.PlayerView.UpdatePlayerStats();
        SavePlayerEquipmentState();
    }

    private void LoadPlayerEquipmentState()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _playerEquipmentState = JsonUtility.FromJson<PlayerEquipmentState>(PlayerPrefs.GetString(SaveKey));
        }
        else
        {
            _playerEquipmentState = _defaultPlayerEquipmentState;
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(_playerEquipmentState));
        }

        EquipArmor(_playerEquipmentState.EquippedArmor);
    }

    private void SavePlayerEquipmentState()
    {
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(_playerEquipmentState));
    }
}
