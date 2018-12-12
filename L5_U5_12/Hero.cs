namespace L5_U5_12
{
    class Hero : Player
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public string Power { get; set; }

        public Hero(string name, string role, int hitPoints, int mana, int damage, int defence, int strength, int agility, int intelligence, string power)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Power = power;
        }

        public Hero(string data)
        : base(data)
        {
            SetData(data);
        }

        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Strength = int.Parse(values[7]);
            Agility = int.Parse(values[8]);
            Intelligence = int.Parse(values[9]);
            Power = values[10];
        }

        public bool IsIntelligence(int intelligenceLimit)
        {
            if (Intelligence > intelligenceLimit)
                return true;
            return false;
        }

        public override string ToString()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Strength,-10};{Agility,-10};{Intelligence,-10};{Power,10}";
        }
    }
}