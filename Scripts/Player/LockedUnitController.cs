using UnityEngine;
using TMPro;

public class LockedUnitController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int price;
    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;
    private bool isPurchased;  //default deðeri false baþlatýr 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        priceText.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player")&& !isPurchased)
        {
            unlockUnit();
            //ürünü parasý yeterse aç
        }
    }
    private void unlockUnit()
    {
        if (CahManager.instance.TryBuyThisUnit(price))
        {

            unlock();
        }
        //parasý var mý kontrol et 
        //varsa ürünü aç
        

    }
    private void unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive (false);  //kiltili objeyi ekrandn kaldýrcaqk 
        unlockedUnit.SetActive (true); //tarlayý açýða çýkarcak 
    }
}
