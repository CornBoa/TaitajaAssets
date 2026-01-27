using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform shootFrom, visFrom;
    [SerializeField] LayerMask enemies;
    [SerializeField] float DMG, distance,duration,intensity;
    public int ammoCap, ammoMag, ammoOwned;
    [SerializeField] TrailRenderer trailRenderer;
    ScreenShake shaker;
    [SerializeField]TextMeshProUGUI boolets,ownedBoolets;
    bool reloading;
    [SerializeField]Slider reloadSlider;
    [SerializeField] AudioClip shoot, reload,mt;
    void Start()
    {
        shaker = GetComponentInChildren<ScreenShake>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(StartReloading());
        }
        if(reloading)reloadSlider.value += Time.deltaTime;
        boolets.text = ammoMag + "/" + ammoCap;
        ownedBoolets.text = ammoOwned.ToString();
    }
    void Shoot()
    {
        if (ammoMag > 0 && !reloading)
        {
            AudioManager.instance.PlaySound(shoot);
            Debug.DrawRay(shootFrom.position, shootFrom.up, Color.red, 10);
            RaycastHit2D hit = Physics2D.Raycast(shootFrom.position, shootFrom.up, distance, enemies);
            if (hit.collider != null)
            {
                IDamagable enemy = hit.collider.GetComponent<IDamagable>();
                if (enemy != null) enemy.TakeDMG(DMG);
                TrailRenderer trail = Instantiate(trailRenderer, visFrom.position, Quaternion.identity);
                StartCoroutine(TrailRoutine(trail, hit));
            }
            shaker.StartShake(duration, intensity);
            ammoMag -= 1;
            Debug.Log(hit.collider);
        }
        else AudioManager.instance.PlaySound(mt);
    }
    void Reload()
    {

        reloading = false;
        if (ammoCap < ammoOwned && ammoMag < ammoCap)
        {
            ammoOwned -= ammoCap - ammoMag;
            ammoMag = ammoCap;          
        }
        else if (ammoCap <= ammoOwned && ammoMag < ammoCap)
        {
            ammoMag = ammoOwned;
            ammoOwned = 0;
        }
        reloadSlider.gameObject.SetActive(false);
    }
    IEnumerator StartReloading()
    {
        AudioManager.instance.PlaySound(reload);
        reloadSlider.gameObject.SetActive(true);
        reloading = true;
        Debug.Log("Reloading");
        reloadSlider.value = 0;
        yield return new WaitForSeconds(1);
        Reload();
    }
    public IEnumerator TrailRoutine(TrailRenderer trail, RaycastHit2D Hit)
    {
        float time = 0;
        Vector3 StartPos = trail.transform.position;
        while (time < 1)
        {
            if (trail != null)
            {
                trail.transform.position = Vector3.Lerp(StartPos, Hit.point, time);
                time += Time.deltaTime / trail.time;
                yield return null;
            }

        }
        trail.transform.position = Hit.point;
        yield return new WaitForSeconds(1);
        Destroy(trail.gameObject);
    }
}
