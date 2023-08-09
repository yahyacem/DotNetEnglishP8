namespace CalifornianHealthMonolithic.WebApp.Models.ViewModels
{
    public class ConsultantViewModel
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Speciality { get; set; }
    }
    public class ConsultantListViewModel
    {
        public List<ConsultantViewModel> ConsultantViewModels { get; set; } = new List<ConsultantViewModel>();
    }
}