using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WEBAPI.Auxiliar
{
    public static class CloneObject
    {
        public static void CopyLinqObject(object obj_source, object obj_dest)
        {
            Type t_source = obj_source.GetType();
            PropertyInfo[] p_source = t_source.GetProperties();

            Type t_dest = obj_dest.GetType();
            PropertyInfo[] p_dest = t_dest.GetProperties();

            foreach (PropertyInfo ps in p_source)
            {
                if (ps.Name.Equals("ValidationResult") || ps.Name.Equals("IsValid") || ps.Name.Equals("objID"))
                    continue;

                foreach (PropertyInfo pd in p_dest)
                {
                    if (ps.Name == pd.Name && !pd.Name.Equals("ValidationResult") && !pd.Name.Equals("IsValid"))
                    {
                        if (ps.PropertyType == pd.PropertyType)
                        {
                            if (ps.PropertyType.IsSerializable)
                            {
                                pd.SetValue(obj_dest, ps.GetValue(obj_source, null), null);
                            }
                        }
                        else
                        {
                            if (ps.PropertyType.BaseType == pd.PropertyType.BaseType)
                            {
                                if (ps.PropertyType.IsSerializable)
                                {
                                    if (ps.GetValue(obj_source, null) != null)
                                    {
                                        try
                                        {
                                            pd.SetValue(obj_dest, ps.GetValue(obj_source, null), null);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}