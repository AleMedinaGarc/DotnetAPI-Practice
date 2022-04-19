using System;
using System.Collections.Generic;
using System.Text;

namespace APICarData.Domain.Interfaces
{
    public interface IPropertyCopier <TParent, TChild> where TParent : class
                                                     where TChild : class
    {
        void Copy(TParent parent, TChild child);
    }
}
