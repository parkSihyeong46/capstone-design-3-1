using System.Collections;
using System.Collections.Generic;

public class Item
{
    private ItmeValue itemValue = ItmeValue.None;
    public ItmeValue Items
    {
        set { itemValue = value; }
        get { return itemValue; }
    }

    // 도구, 열매, 씨앗, 음식 순
    public enum ItmeValue
    {
        None = -1,
        Axe,
        Pick,
        Hoe,
        WateringCans,
        Parsnip,
        Cauliflower,
        Strawberry,
        Seeds,
        ParsnipSeed,
        CauliflowerSeed,
        StrawberrySeed,
        Icecream,
        SurvivalHambuger,
        FriedEgg,
        CheeseCauliflower
    }
}
