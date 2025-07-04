namespace TradingProject.Application.Shared;

public static class Permissions
{
    public static class Account
    {
        public const string AddRole = "Account.AddRole";
        public const string Create = "Account.Create";

        public static List<string> All = new()
        {
            AddRole,
            Create
        };

    }

    public static class Products
    {
        public const string Create = "Products.Create";
        public const string Update = "Products.Update";
        public const string Delete = "Products.Delete";
        public const string GetMy = "Products.GetMy";
        public const string DeleteProductImage = "Products.DeleteProductImage";

        public static List<string> All = new()
        {
            Create,
            Update,
            Delete,
            GetMy
        };
    }

    public static class Orders
    {
        public const string Create = "Orders.Create";
        public const string Delete = "Orders.Delete";
        public const string GetMy = "Orders.GetMy";
        public const string GetDetail = "Order.GetDetail";
        public const string GetMySales = "Orders.GetMySales";

        public static List<string> All = new()
        {
            Create,
            Delete,
            GetMy,
            GetDetail,
            GetMySales

        };
    }
    public static class User
    {
        public const string PasswordReset = "User.PasswordReset";
        public const string Create = "User.Create"; //Bu admine aiddir

        public static List<string> All = new()
        {
            Create,
            PasswordReset
        };
    }
    public static class Review //butun userlara aid
    {
        public const string Create = "Review.Create";
        public const string Delete = "Review.Delete";

        public static List<string> All = new()
        {
            Create,
            Delete
        };
    }

}
