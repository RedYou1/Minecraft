using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Attributes;

namespace BiblioMinecraft.Items
{
    public class Back_Pack : BiblioMinecraft.Attributes.Other
    {
        public Inventaire inventaire = new Inventaire(3,2);

        public override string id()
        {
            return "Back_Pack";
        }

        public override void Right_Click(Player player)
        {
            // TODO: ouvrire inventaire
        }
    }
}
