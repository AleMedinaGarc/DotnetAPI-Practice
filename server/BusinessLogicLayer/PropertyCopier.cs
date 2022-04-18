using System;
using System.Collections.Generic;
using System.Text;

namespace APICarData.Services
{
    public class PropertyCopier<TParent, TChild> where TParent : class
                                                          where TChild : class
    {
        public static void Copy(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name &&
                        parentProperty.PropertyType == childProperty.PropertyType &&
                        parentProperty.GetValue(parent) != null &&
                        parentProperty.Name != "lastLogin" &&
                        parentProperty.Name != "creationDate")
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
    }
}
