namespace StaffApi.ViewModels
{
    public class EditFamilyMemberVM
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lasttName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int studentId { get; set; }
        public int Relatioship { get; set; }
        public int Nationality { get; set; }
    }
}
