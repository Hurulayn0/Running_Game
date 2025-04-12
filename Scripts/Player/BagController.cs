using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<ProductData> productDataList;
    private Vector3 productSize;
    [SerializeField] TextMeshPro maxText;
    private int maxBagCapacity = 5; // Maximum capacity of the bag
    private bool isRemovingProducts = false; // Flag to control removal process
    private bool isInShopPoint = false; // Flag to check if we are in ShopPoint
    [SerializeField] private Transform shopPointTarget; // ShopPoint target object

    void Start()
    {
        // Initialize max bag capacity
        maxBagCapacity = 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            isInShopPoint = true;

            if (!isRemovingProducts)
            {
                StartCoroutine(RemoveProductsOneByOne());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            isInShopPoint = false; // We are no longer in ShopPoint
        }
    }

    private void SellProductToShop(ProductData productData)
    {
        // Simulate selling the product to the shop
        CahManager.instance.ExchangeProduct(productData);
    }

    public void AddProductToBag(ProductData productData)
    {
        if (!IsEmptySpace())
        {
            return; // Return if no space is available
        }

        GameObject boxProduct = Instantiate(productData.productPrefab, Vector3.zero, Quaternion.identity);
        boxProduct.transform.SetParent(bag, true); // Set as child of bag

        // Calculate product size
        CalculateObjectSize(boxProduct);

        // Set new position for product
        float yPosition = CalculateNewPositionOfBox();
        boxProduct.transform.localPosition = new Vector3(0, yPosition, 0);
        boxProduct.transform.localRotation = Quaternion.identity;
        boxProduct.transform.localScale = Vector3.one;

        // Add product to the list
        productDataList.Add(productData);

        // Control bag capacity to show max text if necessary
        ControlBagCapacity();
    }

    private float CalculateNewPositionOfBox()
    {
        float totalHeight = productSize.y * productDataList.Count;
        return totalHeight - (productSize.y / 2); // Adjust position
    }

    private void CalculateObjectSize(GameObject gameObject)
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            productSize = renderer.bounds.size; // Get the size of the product
        }
    }

    private void ControlBagCapacity()
    {
        if (productDataList.Count >= maxBagCapacity)
        {
            SetMaxTextOn(); // Show max text if bag is full
        }
        else
        {
            SetMaxTextOff(); // Hide max text if there is space
        }
    }

    private void SetMaxTextOn()
    {
        if (!maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(true); // Show max text
        }
    }

    private void SetMaxTextOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false); // Hide max text
        }
    }

    public bool IsEmptySpace()
    {
        return productDataList.Count < maxBagCapacity; // Check if there is space
    }

    private IEnumerator RemoveProductsOneByOne()
    {
        isRemovingProducts = true; // Ürün çýkarma iþlemi baþladý
        for (int i = productDataList.Count - 1; i >= 0; i--)
        {
            if (!isInShopPoint) // Eðer ShopPoint'ten çýkýldýysa iþlemi durdur
            {
                break;
            }

            SellProductToShop(productDataList[i]);

            // Ürünü ShopPoint'e taþý ve pozisyon ayarla
            Transform productTransform = bag.transform.GetChild(i);
            productTransform.SetParent(shopPointTarget, true);
            float newYPosition = productSize.y * shopPointTarget.childCount;
            productTransform.localPosition = new Vector3(0, newYPosition, 0); // Ürünü y eksenine göre ayarla

            // Boyutunu koru
            productTransform.localScale = Vector3.one;
            productTransform.localRotation = Quaternion.identity; // Yönü koru

            productDataList.RemoveAt(i); // Listeyi güncelle
            yield return new WaitForSeconds(0.5f); // Bekleme süresi

            // Control bag capacity after each removal to hide "max" text if needed
            ControlBagCapacity();
        }
        isRemovingProducts = false; // Ýþlem tamamlandý
    }
}
