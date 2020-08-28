using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText,_coinText,_weaponText;

    [SerializeField]
    private GameObject _coin,_weapon;

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;             
    }

    public void CoinPickUp()
    {
        _coinText.text = "Press 'E' to pick up the coin.";
    }

    public void CollectedCoin()
    {
        _coin.SetActive(true);
    }

    public void RemoveCoin()
    {
        _coin.SetActive(false);
    }
    public void ResetCoinPickUp()
    {
        _coinText.text = "";
    }

    public void OfferWeapon()
    {
        _weaponText.text = "Press 'E' to buy a weapon.";
    }
    public void ResetOfferWeapon()
    {
        _weaponText.text = "";
    }


}
