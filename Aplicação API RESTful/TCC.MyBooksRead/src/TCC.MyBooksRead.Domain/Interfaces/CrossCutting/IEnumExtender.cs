using System;

namespace TCC.MyBooksRead.Domain.Interfaces.CrossCutting
{
    public interface IEnumExtender
    {
        Guid GetEnumGuid(Enum enumerator);
    }
}


