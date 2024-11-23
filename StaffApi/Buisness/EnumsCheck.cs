namespace StaffApi.Buisness
{
    public class EnumsCheck : IEnumsCheck
    {
        public bool NationalitiesCheck(string nationality)
        {

            var natinalityList = Enum.GetNames(typeof(NationalityEnum));
            if (natinalityList.Contains(nationality)) return true;
            else return false;
        }
        public bool RelationsCheck(string Relation)
        {

            var natinalityList = Enum.GetNames(typeof(RelatioshipEnum));
            if (natinalityList.Contains(Relation)) return true;
            else return false;
        }
        public bool NationalitiesCheckByid(int nationality)
        {

            if (Enum.IsDefined(typeof(NationalityEnum),nationality))
            {
                return true;
            }
            else return false;

        }
        public bool RelationsCheckByid(int relation)
        {

            if (Enum.IsDefined(typeof(RelatioshipEnum),relation))
            {
                return true;
            }
            else return false;

        }
    }
}
