using UnityEngine;

public class CahManager : MonoBehaviour
{
    public static CahManager instance;
    private int coins;
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else 
            Destroy(instance);
    }
    public void ExchangeProduct(ProductData productData) 
    {
        AddCoin(productData.productPrice);
    }
    public void AddCoin(int price)
    {
        coins += price;
        DisplayCoins();
    }
    public void SpendCoin(int price)
    {
        coins -= price;
        DisplayCoins();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool TryBuyThisUnit(int price)
    {
        if (GetCoins() >= price)
        {
            //paran� harca
            SpendCoin(price);
            return true;
        }
        return false;
    }
    public int GetCoins() 
    {
        return coins;
    }
    private void DisplayCoins()
    {
        UIManager.instance.ShowCoinCountOnScreen(coins);
    }
}
