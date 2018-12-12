namespace L5_U5_12
{
    class NPC : Player
    {
        public string Guild { get; set; }

        public NPC(string name, string role, int hitPoints, int mana, int damage, int defence, string guild)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Guild = guild;
        }

        public NPC(string data)
        : base(data)
        {
            SetData(data);
        }

        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Guild = values[7];
        }

        public bool IsNotDamaged(int damagePoint)
        {
            if (Damage <= damagePoint)
                return true;
            return false;
        }

        public override string ToString()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Guild,-10}";
        }
    }
}
