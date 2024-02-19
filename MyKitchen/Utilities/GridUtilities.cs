namespace MyKitchen.Utilities
{
    public static class GridUtilities {

        public static string ToggleAscDesc(string sortOrder)
        {
            var rtn = string.Empty;

            if(sortOrder.EndsWith("_desc"))
            {
                //remove desc
                rtn = sortOrder.Substring(0,sortOrder.Length - "_desc".Length);
            }
            else
            {
                rtn = sortOrder += "_desc";
            }

            return rtn;
        }

        public static string Trim_desc(string str)
        {
            var rtn = str;

            if(str.EndsWith("_desc"))
            {
                //remove desc
                rtn = str.Substring(0,str.Length - "_desc".Length);
            }

            return rtn;

        }
    }
}