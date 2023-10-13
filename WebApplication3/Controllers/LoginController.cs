using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class LoginController : Controller
    {
        private string connectionString = "Server=DESKTOP-3D9K9A4\\SQLEXPRESS;Database=DemoProject;Integrated Security=True;";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string username, string password)
        {
            bool isAuthorized = AuthorizationUser(username, password);

            if (isAuthorized)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        private bool AuthorizationUser(string username, string password)
        {
            string sql = "SELECT COUNT(*) FROM Tbl_login WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
    }
}