using System;
using System.ComponentModel;
using TCC.MyBooksRead.Domain.Enumerators;
using TCC.MyBooksRead.Domain.Interfaces.CrossCutting;

namespace TCC.MyBooksRead.Infra.CrossCutting.Helpers
{
    public class EnumExtensionMethods : IEnumExtender
    {
        /// <summary>
        /// Recupera o Guid de um enumerador através do Atributo "GuidEnum"
        /// </summary>
        /// <param name="enumerator">Enum</param>
        /// <returns>Guid</returns>
        public Guid GetEnumGuid(Enum enumerator)
        {
            var type = enumerator.GetType();

            var memInfo = type.GetMember(enumerator.ToString());

            if (memInfo.Length <= 0) throw new ArgumentException();

            var attrs = memInfo[0].GetCustomAttributes(false);

            if (attrs.Length > 0)
                return ((GuidEnum)attrs[0]).Guid;

            throw new ArgumentException();
        }
    }
}
