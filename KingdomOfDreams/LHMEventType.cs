using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHMEventType
{
    public enum eEventType
    {
        NONE = -1,
        CREATED_MAGIC_TOOL,
        UPGRADE_MAGIC_TOOL,
        GET_DREAM_PIECE,
        EXCLAIM_ICON_BOOK_ITEM,
        GET_INGREDIENT,
        CREATE_MAGIC_CIRCLE,
        GET_BOOK_ITEM,
        REFRESH_UI_INVENTORY,
        REFRESH_UI_BOOK,
        CHECK_MAGICTOOL_LEVEL,
        REFRESH_UI_MAGICTOOL,
        SHOW_PRODUCTION_CHAIN,
        COMBINE_INGREDIENT,
        GIVE_OFFLINE_INGREDIENT,
        CLAIM_BOOK_ITEM, 
        SHOW_OFFLINE_GIFT_EXPLAIN, 

    }
}
