using UnityEngine;

public class MyTree : MonoBehaviour
{
    public TreeSaveData GetData()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        return new TreeSaveData()
        {
            TreePosition = transform.localPosition,
            TreeRotation = transform.localRotation,
            objNum = int.Parse(transform.transform.Find("Number").GetComponent<TextMesh>().text)
        };
    }
    
    public void SetData(TreeSaveData data)
    {
        transform.localPosition = data.TreePosition;
        transform.localRotation = data.TreeRotation;
        transform.transform.Find("Number").GetComponent<TextMesh>().text = data.objNum.ToString();
    }
}
