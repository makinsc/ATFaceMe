namespace ApiBackend.Transversal.DTOs.PLC
{
    public static class Endpoints
    {

        public const string urlBase = "http://atfaceme.azurewebsites.net/api";

        public const string GetMe = "/user";
        public const string GetUserById = "/user/GetById";
        public const string GetAllUser = "/user/GetAll"; //Get All paginated users
        public const string GetAllByFilter = "/user/GetAllByFilter";
        public const string Identify = "/Face/Identify";
        public const string Train = "/Face/Train";

        public const string HeaderSkipToken = "skiptoken";
    }
}
