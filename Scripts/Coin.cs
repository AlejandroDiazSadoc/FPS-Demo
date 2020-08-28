using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPickUp;

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
            _uiManager.CoinPickUp();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.gameObject.GetComponent<Player>();
                if(player != null)
                {
                    player.hasCoin=true;
                    AudioSource.PlayClipAtPoint(_coinPickUp, transform.position, 1f);
                    _uiManager.CollectedCoin();
                    Destroy(this.gameObject);
                    _uiManager.ResetCoinPickUp();
                }

                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiManager.ResetCoinPickUp();
        }
    }
}
