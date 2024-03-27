using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EquipmentPanel : Shop
{
    [FormerlySerializedAs("_weaponView")]
    [Header("[Views]")]
    [SerializeField] private EquipmentPanelItemView _weaponPanelItemView;
    [SerializeField] private EquipmentPanelItemView _armorPanelItemView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private GameObject _armorContainer;

    private List<EquipmentItemGameObject> _weapons;
    private List<EquipmentItemGameObject> _armors;
    //сделать отдельный скрипт под оружие отдельный под армор

    public void OpenEquipmentPanel(Panels panel)
    {
        //CloseAllPanels();
        gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
    }

    //public void AddEquipment(Equipment equipment, EquipmentView equipmentView, GameObject container)
    //{
    //    var view = Instantiate(equipmentView, container.transform);
    //    view.BuyButtonClick += OnBuyEquipment;
    //    view.ChangeEquipmentButtonClick += OnChangeEquipment;
    //    view.Render(equipment);
    //}

    //public void OnBuyEquipment(Equipment equipment, EquipmentView equipmentView)
    //{
    //    TryBuyEquipment(equipment, equipmentView);
    //}

    //public void TryBuyEquipment(Equipment equipment, EquipmentView equipmentView)
    //{
    //    if (equipment.Price <= Player.Coins)
    //    {
    //        Player.PlayerEquipment.BuyEquipment(equipment);
    //        equipment.Buy();
    //        equipmentView.BuyButtonClick -= OnBuyEquipment;
    //        UpdatePlayerStats();
    //    }
    //    else DialogPanel.ShowPanel();
    //}

    //public void OnChangeEquipment(Equipment equipment)
    //{
    //    Player.PlayerEquipment.ChangeCurrentEquipment(equipment);
    //}

    //protected override void Filling(Player player)
    //{
    //    Player = player;

    //    if (_weapons == null)
    //    {
    //        _weapons = Player.PlayerEquipment.GetListWeapon();
    //        _armors = Player.PlayerEquipment.GetListArmor();

    //        AddEquipmentToList(_weapons, _weaponView, _weaponContainer);
    //        AddEquipmentToList(_armors, _armorView, _armorContainer);
    //    }

    //    TryUnlockEquipment(_weaponContainer);
    //    TryUnlockEquipment(_armorContainer);
    //}

    private void AddEquipmentToList(List<EquipmentItemGameObject> equipment, EquipmentPanelItemView equipmentPanelItemView, GameObject container)
    {
        for (int i = 1; i < equipment.Count; i++)
        {
            //AddEquipment(equipment[i], equipmentView, container);
        }
    }

    private void TryUnlockEquipment(GameObject container)
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
           // container.transform.GetChild(i).GetComponent<EquipmentView>().TryUnlockBuyButton(Player);
        }
    }
}