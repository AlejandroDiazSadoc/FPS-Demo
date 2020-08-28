using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    [SerializeField]
    private AudioSource _weaponAudio;

    [SerializeField]
    private int _currentAmmo;
    private int _maxAmmo = 50;

    private bool _isReloading = false;

    private UIManager _uiManager;

    public bool hasCoin = false;

    [SerializeField]
    private GameObject _weapon;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentAmmo = _maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateAmmo(_currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0 && !_isReloading && _weapon.activeSelf)
        {
            Shoot();
        }
        else
        {
            _weaponAudio.Stop();
            _muzzleFlash.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }
        CalculateMovement();

    }

    void Shoot()
    {
       

            _muzzleFlash.SetActive(true);
            _currentAmmo--;
            _uiManager.UpdateAmmo(_currentAmmo);
            if (!_weaponAudio.isPlaying)
            {
                _weaponAudio.Play();
            }

            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
                GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                Destroy(hitMarker, 1f);

                Destructable crate = hitInfo.transform.GetComponent<Destructable>();
                if(crate != null)
                {
                    crate.DestroyCrate();
                }
                

        }
        

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity = transform.transform.TransformDirection(velocity);
        velocity.y -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = 50;
        _uiManager.UpdateAmmo(_currentAmmo);
        _isReloading = false;
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
