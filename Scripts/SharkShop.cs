using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField]
    private AudioClip _weaponBuy;

    private UIManager _uiManager;
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiManager.OfferWeapon();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    if (player.hasCoin)
                    {
                        player.hasCoin = false;
                        AudioSource.PlayClipAtPoint(_weaponBuy, transform.position, 1f);
                        _uiManager.RemoveCoin();
                        player.EnableWeapon();
                        _uiManager.ResetOfferWeapon();

                    }

                }


            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiManager.ResetOfferWeapon();
        }
    }

}
