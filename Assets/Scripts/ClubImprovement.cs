using UnityEngine.UI;

public class ClubImprovement 
{
    //The name of the improvement
    public string name;

    //The image associated to this improvement
    public Image image;

    //The price of this improvement
    public int price;

    //True if this improvement is built
    public bool isBuilt;

    public ClubImprovement(Image image, int price, bool isBuilt)
    {
        this.image = image;
        this.price = price;
        this.isBuilt = isBuilt;
    }
}
