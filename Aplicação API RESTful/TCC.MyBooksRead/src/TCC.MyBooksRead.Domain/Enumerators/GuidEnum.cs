using System;

namespace TCC.MyBooksRead.Domain.Enumerators
{
    public class GuidEnum : Attribute
    {
        public Guid Guid { get; set; }

        public GuidEnum(string valor)
        {
            Guid = new Guid(valor);
        }
    }
}
