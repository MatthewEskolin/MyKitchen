namespace Utilities
{
    public static class CUtilities {

        public static bool IntToBool(int i)
        {
            if(i == 1) return true;
            if(i == 0) return false;

            //return default value;
            return false;
        }
    }
}