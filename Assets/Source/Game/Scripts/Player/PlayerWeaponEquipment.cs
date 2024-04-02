using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    private const string SaveKey = "PlayerEquipment";

    [Header("[Player Entity]")]
    [SerializeField] private Player _player;
    [Header("[Containers]")]
    [SerializeField] private Transform _weaponObjectContainer;
    [Header("[Default Equipment Data]")]
    [SerializeField] private PlayerEquipmentState _defaultPlayerEquipmentState;

    private PlayerEquipmentState _playerEquipmentState;
    private EquipmentItemGameObject _weaponObject;

    public EquipmentItemState CurrentWeapon => _playerEquipmentState.EquippedWeapon;

    private void Awake()
    {
        LoadPlayerEquipmentState();
    }

    public void BuyWeaponItem(EquipmentItemState equipmentItemState)
    {
        equipmentItemState.IsBuyed = true;
    }

    public void EquipWeapon(EquipmentItemState equipmentItemState)
    {
        if (equipmentItemState == null)
            return;

        if (_weaponObject != null)
            Destroy(_weaponObject.gameObject);

        if (CurrentWeapon != null)
            CurrentWeapon.IsEquipped = false;

        equipmentItemState.IsEquipped = true;
        _weaponObject = Instantiate(equipmentItemState.ItemData.ItemGameObject as EquipmentItemGameObject, _weaponObjectContainer);
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

        EquipWeapon(_playerEquipmentState.EquippedWeapon);
    }

    private void SavePlayerEquipmentState()
    {
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(_playerEquipmentState));
    }
}
