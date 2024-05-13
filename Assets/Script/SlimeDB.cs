using Unity;
using System;
using static UnityEditor.Progress;

class SlimeDB : MonsterData
{ 
    public SlimeDB(string slimename)
    {
        switch (slimename) // 드랍아이템 처리는 다른 애들과 같게
        {
            case "GreenSlime":
                hp=10;
                atk=5;
                def=5;
                spd=3;
                exp=10;
                items = 1; // 드랍 아이템 종류 = 1
                itemID = new int[items];
                itemID[0] = 50; // 초록슬라임부산물 (임시)
                dropnumber = new int[items];
                dropnumber[0] = 1; // 갯수
                return;
            case "BlueSlime":
                hp = 20;
                atk = 10;
                def = 10;
                spd = 3;
                exp = 20;
                items = 1; // 드랍 아이템 종류 = 1
                itemID = new int[items];
                itemID[0] = 50; // 초록슬라임부산물 (임시)
                dropnumber = new int[items];
                dropnumber[0] = 1; // 갯수
                return;
            case "RedSlime":
                hp = 30;
                atk = 15;
                def = 15;
                spd = 3;
                exp = 30;
                items = 1; // 드랍 아이템 종류 = 1
                itemID = new int[items];
                itemID[0] = 50; // 초록슬라임부산물 (임시)
                dropnumber = new int[items];
                dropnumber[0] = 1; // 갯수
                return;
            case "PurpleSlime":
                hp = 40;
                atk = 20;
                def = 20;
                spd = 3;
                exp = 40;
                items = 1; // 드랍 아이템 종류 = 1
                itemID = new int[items];
                itemID[0] = 50; // 초록슬라임부산물 (임시)
                dropnumber = new int[items];
                dropnumber[0] = 1; // 갯수
                return;
            case "DirtSlime":
                hp = 40;
                atk = 20;
                def = 20;
                spd = 3;
                exp = 40;
                items = 1; // 드랍 아이템 종류 = 1
                itemID = new int[items];
                itemID[0] = 50; // 초록슬라임부산물 (임시)
                dropnumber = new int[items];
                dropnumber[0] = 1; // 갯수
                return;
            case "StoneSlime":
                hp = 40;
                atk = 20;
                def = 20;
                spd = 3;
                exp = 40;
                items = 1; // 드랍 아이템 종류 = 1
                itemID = new int[items];
                itemID[0] = 50; // 초록슬라임부산물 (임시)
                dropnumber = new int[items];
                dropnumber[0] = 1; // 갯수
                return;
        }
    }
}
