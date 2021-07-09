using System;
using System.Collections.Generic;
using System.Text;

namespace MurphyJustin_BS
{
    class LoadData
    {
        enum PlayerPrams
        {
            Name,
            Hp,
            Strength
        };

        struct PlayerStruct
        {
            public string name;
            public float hp;
            public float strength;
        }


        private string[] rawData;
        private PlayerPrams playerPrams = PlayerPrams.Name;
        public bool LoadDataIn(string filePath)
        {
            try
            {
                rawData = System.IO.File.ReadAllLines(filePath);
                return true;
            }
            catch
            {
                Console.WriteLine("Can't read file. Please try again");
                Console.ReadKey();
                return false;
            }
        }

        public void LoadDataInGame(ref Player[] playersList)
        {
            if(rawData == null) { return; }

            PlayerStruct struct_Player;
            #region Inital Default Struct Values

            // I did this to keep the compiler from freaking out.
            struct_Player.name = string.Empty;
            struct_Player.hp = 8;
            struct_Player.strength = 5;
            #endregion

            bool hasFilterJunk = false;
            bool isCompleate = false;
            int index = 0;
            for(int i = 0; i < rawData.Length; i++)
            {
                string[] parseData = rawData[i].Split(',');
                foreach (var item in parseData)
                {
                    if(!hasFilterJunk)
                    {
                        if(parseData[0].ToLower() != "name")
                        {
                            hasFilterJunk = true;
                        }
                        if(item.ToLower() == "strength")
                        {
                            hasFilterJunk = true;
                            break;
                        }
                    }
                    else if(hasFilterJunk)
                    {
                        if (!isCompleate)
                        {
                            switch (playerPrams)
                            {
                                case PlayerPrams.Name:
                                    struct_Player.name = item;
                                    playerPrams = PlayerPrams.Hp;
                                    break;
                                case PlayerPrams.Hp:
                                    float.TryParse(item, out struct_Player.hp);
                                    playerPrams = PlayerPrams.Strength;
                                    break;
                                case PlayerPrams.Strength:
                                    float.TryParse(item, out struct_Player.strength);
                                    isCompleate = true;
                                    playerPrams = PlayerPrams.Name;
                                    break;
                            }
                        }
                        if(isCompleate)
                        {
                            Player player = new Player(struct_Player.name, struct_Player.hp, struct_Player.strength);
                            playersList[index] = player;
                            index++;
                            isCompleate = false;
                        }

                    }
                }
            }


        }
    }
}
