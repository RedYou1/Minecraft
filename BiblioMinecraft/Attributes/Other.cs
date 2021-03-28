using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Damages;

namespace BiblioMinecraft.Attributes
{
    public abstract class Other : Attribute
    {
        public Other(int quantity, int maxQuantity) : base(quantity, maxQuantity)
        {

        }

        public override String Attribute_id() { return "Other"; }
        public override bool HaveAttribute(Attribute attribute)
        {
            return (attribute.Attribute_id() == Attribute_id());
        }
    }
}
