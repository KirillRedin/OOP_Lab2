using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2
{
    class Emperor
    {
        public string Name;
        public bool alive = true;
    }

    class Lieutenant
    {
        public int SerialNumber;
    }

    class ImperialArmy
    {
        public static int amount;
        private bool battle = false;
        private Emperor CurrEmperor;

        public ImperialArmy() { }

        public ImperialArmy(Emperor SithLord, int Soldiers)
        {
            CurrEmperor = SithLord;
            amount = Soldiers;
        }

        public Emperor CurrentEmperor
        {
            get
            {
                if (CurrEmperor.alive == true)
                    return CurrEmperor;
                else
                    Console.WriteLine("Emperor is dead");
                return null;
            }
            internal set
            {
                if (CurrEmperor.alive == false)
                    CurrEmperor = value;
                else
                    Console.WriteLine("Can't choose new Emperor while current is alive.");
            }
        }

        protected virtual void ChangeClonesAmount(int SquadNum, bool clone)
        {
            if (clone)
                amount++;
            else amount--;
        }

        public virtual void CloneBorned(int SquadNum)
        {
            ChangeClonesAmount(SquadNum, true);
        }

        public virtual void CloneKilled(int SquadNum)
        {
            ChangeClonesAmount(SquadNum, false);
        }

        public bool BattleStatus()
        {
            return battle;
        }

        public void ChangeBattleStatus()
        {
            if (BattleStatus())
                battle = false;
            else battle = true;
        }
    }



    class Platoon : ImperialArmy
    {
        private static int[] Squads;
        private static int SquadCount;
        private Lieutenant CurrLieutenant;

        public Platoon() { amount = 128; }

        public Platoon(Lieutenant CloneSoldier)
        {

            CurrLieutenant = CloneSoldier;
        }

        static Platoon()
        {
            SquadCount = 4;
            for (int i = 0; i < SquadCount; i++)
                Squads[i] = 32;
        }

        public Lieutenant CloneLiuetenant
        {
            get { return CurrLieutenant; }
            internal set { CurrLieutenant = value; }
        }

        protected override void ChangeClonesAmount(int SquadNum, bool clone)
        {
            if (!clone)
                if (Squads[SquadNum] > 1)
                {
                    Squads[SquadNum]--;
                    amount--;
                }
                else
                {
                    Squads[SquadNum] = -1;
                    if (SquadCount > 0)
                    {
                        SquadCount--;
                        amount--;
                    }
                    else
                        Console.WriteLine("All Squads are Destroyed");
                }
            else
            {
                if (Squads[SquadNum] < 32)
                {
                    Squads[SquadNum]++;
                    amount++;
                }
                else
                    Console.WriteLine("Squad is Full");
            }
        }

        public override void CloneKilled(int SquadNum)
        {
            ChangeClonesAmount(SquadNum, false);
        }

        public override void CloneBorned(int SquadNum)
        {
            ChangeClonesAmount(SquadNum, true);
        }
    }

    class Squad : Platoon
    {
        internal static int CurrSquadID;
        protected static string[] weapon;

        internal int NextSquadID()
        {
            return CurrSquadID++;
        }

        public Squad() { amount = 32; }

        static Squad()
        {
            weapon[0] = "Laser Pistol";
            CurrSquadID = 0;
        }

        public static string GetWeapon()
        {
            return weapon[0];
        }

        public int SquadCounter()
        {
            return amount;
        }

        protected override void ChangeClonesAmount(int SquadNum, bool clone)
        {
            if (!clone)
            {
                if (amount > 1)
                    amount--;
                else
                    Console.WriteLine("Squad Destroyed");
            }
            else
            {
                if (amount < 32)
                    amount++;
                else
                    Console.WriteLine("Squad is Full");
            }
        }

        public override void CloneKilled(int SquadNum)
        {
            ChangeClonesAmount(SquadNum, false);
        }

        public override void CloneBorned(int SquadNum)
        {
            ChangeClonesAmount(SquadNum, true);
        }
    }

    class Assault : Squad
    {
        private string CurrMission;
        private static Assault instance;

        private Assault() { }

        public static Assault Instance
        {
          get
            {
                if (instance == null)
                {
                    instance = new Assault();
                }
                return instance;
            }
        }

        public string Mission
        {
            get { return CurrMission; }
            internal set { CurrMission = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
