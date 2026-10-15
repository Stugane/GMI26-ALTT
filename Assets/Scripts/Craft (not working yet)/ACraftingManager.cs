using UnityEngine;
using UnityEngine.UI;

public class ACraftingManager : MonoBehaviour
{
    private AItemCraft currentItem;
    public Image cursor;

    public void OnMouseDownItem(AItemCraft item)
    {
        if(currentItem == null)
        {
            currentItem = item;
            cursor.gameObject.SetActive(true);
            cursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}
