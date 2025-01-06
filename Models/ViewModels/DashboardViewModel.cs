namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class DashboardViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
