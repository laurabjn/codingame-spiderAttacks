using System;
using System.Collections.Generic;

class Player
{
    public const int TYPE_MONSTER = 0;
    public const int TYPE_MY_HERO = 1;
    public const int TYPE_OP_HERO = 2;

    public class Entity
    {
        public int Id;
        public int Type;
        public int X, Y;
        public int ShieldLife;
        public int IsControlled;
        public int Health;
        public int Vx, Vy;
        public int NearBase;
        public int ThreatFor;

        public Entity(int id, int type, int x, int y, int shieldLife, int isControlled, int health, int vx, int vy, int nearBase, int threatFor)
        {
            this.Id = id;
            this.Type = type;
            this.X = x;
            this.Y = y;
            this.ShieldLife = shieldLife;
            this.IsControlled = isControlled;
            this.Health = health;
            this.Vx = vx;
            this.Vy = vy;
            this.NearBase = nearBase;
            this.ThreatFor = threatFor;
        }

        public void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    static void Main(string[] args)
    {

        //Retourne la distance entre le héro et un monstre.
        static int compareTwoDistance(int positionMonster, int positionHero)
        {
            positionMonster = Math.Abs(positionMonster);
            positionHero = Math.Abs(positionHero);
            return positionMonster - positionHero;
        }

        //Permet de savoir quelle est le monstre le plus proche de la base pour le prendre pour cible
        static List<object> compareDistanceBetweenMonsterAndBase(Entity monster, int previousPosition, int position, int sideOfPlayer)
        {
            Entity target = null;

            //Si aucun ennemi pris pour cible
            if (previousPosition == 0)
            {
                previousPosition = monster.X + monster.Y;
                position = monster.X + monster.Y;
                target = monster;
            }

            //Si déja un ennemi en cible, on compare la précédente cible a la nouvelle
            else
            {
                position = monster.X + monster.Y;

                //Si le 2ème ennemi est plus proche du coeur de la base, on le prend pour cible
                if ((sideOfPlayer == 0 && position < previousPosition) || (sideOfPlayer == 1 && position > previousPosition))
                {
                    previousPosition = position;
                    target = monster;
                }
            }
            List<object> listResult = new List<object>();
            listResult.Add(target);
            listResult.Add(previousPosition);
            return listResult;
        }

        //Permet de savoir quelle est le monstre le plus proche du héros
        static List<object> compareDistanceBetweenMonsterAndHeros(Entity monster, Entity heroes, int previousPosition, int position, int sideOfPlayer)
        {
            Entity target = null;

            int positionHero = heroes.X + heroes.Y;
            int positionMonster = monster.X + monster.Y;

            //Si aucun ennemi pris pour cible
            if (previousPosition == 0)
            {
                previousPosition = compareTwoDistance(positionMonster, positionHero);
                position = previousPosition;
                target = monster;
            }

            //Si déja un ennemi en cible, on compare la précédente cible a la nouvelle
            else
            {
                positionBetweenMonsterAndHero = compareTwoDistance(positionMonster, positionHero);

                //Si le 2ème ennemi est plus proche du héros que le 1er, on le prend pour cible
                if ((sideOfPlayer == 0 && position < previousPosition) || (sideOfPlayer == 1 && position > previousPosition))
                {
                    previousPosition = position;
                    target = monster;
                }
            }
            List<object> listResult = new List<object>();
            listResult.Add(target);
            listResult.Add(previousPosition);
            return listResult;
        }

        string[] inputs;
        inputs = Console.ReadLine().Split(' ');

        // base_x,base_y: The corner of the map representing your base
        int baseX = int.Parse(inputs[0]);
        int baseY = int.Parse(inputs[1]);

        // heroesPerPlayer: Always 3
        int heroesPerPlayer = int.Parse(Console.ReadLine());

        // game loop
        while (true)
        {

            inputs = Console.ReadLine().Split(' ');
            int myHealth = int.Parse(inputs[0]); // Your base health
            int myMana = int.Parse(inputs[1]); // Ignore in the first league; Spend ten mana to cast a spell

            inputs = Console.ReadLine().Split(' ');
            int oppHealth = int.Parse(inputs[0]);
            int oppMana = int.Parse(inputs[1]);

            int entityCount = int.Parse(Console.ReadLine()); // Amount of heros and monsters you can see

            List<Entity> myHeroes = new List<Entity>(entityCount);
            List<Entity> oppHeroes = new List<Entity>(entityCount);
            List<Entity> monsters = new List<Entity>(entityCount);

            for (int i = 0; i < entityCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int id = int.Parse(inputs[0]); // Unique identifier
                int type = int.Parse(inputs[1]); // 0=monster, 1=your hero, 2=opponent hero
                int x = int.Parse(inputs[2]); // Position of this entity
                int y = int.Parse(inputs[3]);
                int shieldLife = int.Parse(inputs[4]); // Ignore for this league; Count down until shield spell fades
                int isControlled = int.Parse(inputs[5]); // Ignore for this league; Equals 1 when this entity is under a control spell
                int health = int.Parse(inputs[6]); // Remaining health of this monster
                int vx = int.Parse(inputs[7]); // Trajectory of this monster
                int vy = int.Parse(inputs[8]);
                int nearBase = int.Parse(inputs[9]); // 0=monster with no target yet, 1=monster targeting a base
                int threatFor = int.Parse(inputs[10]); // Given this monster's trajectory, is it a threat to 1=your base, 2=your opponent's base, 0=neither

                Entity entity = new Entity(
                    id, type, x, y, shieldLife, isControlled, health, vx, vy, nearBase, threatFor
                );

                switch (type)
                {
                    case TYPE_MONSTER:
                        monsters.Add(entity);
                        break;
                    case TYPE_MY_HERO:
                        myHeroes.Add(entity);
                        break;
                    case TYPE_OP_HERO:
                        oppHeroes.Add(entity);
                        break;
                }
            }
            for (int i = 0; i < heroesPerPlayer; i++)
            {
                Entity target = null;

                if (monsters.Count > 0)
                {
                    target = monsters[i % monsters.Count];
                }

                if (target != null)
                {
                    Console.WriteLine($"MOVE {target.X} {target.Y}");
                }
                else
                {
                    Console.WriteLine("WAIT");
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
        }
    }
}