namespace CarShop.Data
{
    public class DataConstants
    {
        //Common constants
        public const int DefaultMaxLength = 20;
        public const int IdMaxLength = 40;

        // User constants
        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 20;
        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;
        public const string UserEmailRegularExpression = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string UserClientType = "Client";
        public const string UserMechanicType = "Mechanic";


        // Car constants
        public const int CarYearMinValue = 1800;
        public const int CarYearMaxValue = 2100;
        public const int CarModelMinLength = 5;
        public const int CarPlateMaxLength = 8;
        public const string CarPlateNumberRegularExpression = @"^([A-Z]{2}[0-9]{4}[A-Z]{2})$";


        // Issues constants
        public const int IssueDescriptionMinLength = 5;
    }
}
