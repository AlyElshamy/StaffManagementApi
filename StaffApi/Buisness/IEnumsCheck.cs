namespace StaffApi.Buisness
{
    public interface IEnumsCheck
    {
        bool NationalitiesCheck(string nationality);
        bool RelationsCheck(string Relation);
        public bool NationalitiesCheckByid(int nationality);
        public bool RelationsCheckByid(int nationality);
        }
}